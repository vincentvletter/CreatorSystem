using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Infrastructure.Data;
using CreatorSystem.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;
using FluentValidation;
using CreatorSystem.Application.Common.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------
// 🔹 Logging (Serilog)
// ---------------------------
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .WriteTo.File("Logs/creatorsystem.log", rollingInterval: RollingInterval.Day)
      .ReadFrom.Configuration(ctx.Configuration));

// ---------------------------
// 🔹 Services
// ---------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CreatorSystem API",
        Version = "v1",
        Description = "AI-driven content planning & caption SaaS built with .NET 9 and CQRS architecture."
    });

    // 🔐 JWT support in Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and your valid token.\nExample: 'Bearer eyJhbGciOiJIUzI1...'"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ---------------------------
// 🔹 Database (SQL Server)
// ---------------------------
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ---------------------------
// 🔹 CQRS (MediatR)
// ---------------------------
// Scan de hele Application assembly
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetAssembly(typeof(CreatorSystem.Application.AssemblyMarker))!
    );
});

// 🔥 Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreatorSystem.Application.AssemblyMarker>();

// MediatR pipeline behavior voor validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ---------------------------
// 🔹 JWT Authentication
// ---------------------------
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
        };
    });

// ---------------------------
// 🔹 Dependency Injection
// ---------------------------

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHealthChecks();

var app = builder.Build();

// ---------------------------
// 🔹 Middleware Pipeline
// ---------------------------
if (app.Environment.IsDevelopment() ||
    app.Environment.EnvironmentName.Equals("DEV", StringComparison.OrdinalIgnoreCase))
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreatorSystem API V1");
        c.RoutePrefix = "swagger"; // of "" om direct op / te openen
    });
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

// ⚙️ Custom Middleware (global exception handling)
app.UseMiddleware<CreatorSystem.Api.Middleware.ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
