using CreatorSystem.Application.Common.Interfaces;
using CreatorSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔥 Configure EF Core to use SQL Server
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR setup
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatorSystem.Application.Posts.Commands.CreatePostCommand).Assembly));

builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console()
      .WriteTo.File("Logs/creatorsystem.log", rollingInterval: RollingInterval.Day)
      .ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddHealthChecks();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    await CreatorSystem.Infrastructure.Data.DbInitializer.SeedAsync(db);
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseMiddleware<CreatorSystem.Api.Middleware.ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();