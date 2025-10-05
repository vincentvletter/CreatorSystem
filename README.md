# 🧠 CreatorSystem  
> AI-driven content planning & caption SaaS built with .NET 9 and CQRS architecture.

CreatorSystem is a **modular SaaS application** designed for content creators and marketing teams.  
It combines **AI-powered caption generation**, **content scheduling**, **analytics**, and **subscription management** — built entirely with a **Clean Architecture + CQRS** approach in .NET 9.  

---

## 🚀 Vision
CreatorSystem empowers creators to combine **discipline, creativity, and consistency**.  
The focus is on:
- Authentic AI output (trainable brand voice)  
- Minimalistic and intuitive UX  
- Scalable, clean backend architecture  
- Monetization through subscriptions  

---

## 🧩 Architecture
CreatorSystem follows a **Clean Modular Monolith** with CQRS, where each domain (Orders, Posts, Users, Billing, etc.) owns its logic and data.

**Structure:**

**Core principles:**
- CQRS (Command Query Responsibility Segregation)
- MediatR for command/query dispatching
- Dependency Injection for loose coupling
- Modular design per bounded context
- Event-driven (domain events + notifications)

---

## 🛠️ Technologies
| Layer | Technology |
|-------|-------------|
| **API** | ASP.NET Core 9 Web API |
| **CQRS** | MediatR |
| **Database** | PostgreSQL + EF Core |
| **Background jobs** | Hangfire / Quartz (later) |
| **Cache / Messaging** | Redis (later) |
| **Payments** | Stripe |
| **Frontend** | Blazor Server (Admin + Web) |
| **CI/CD** | GitHub Actions + Docker |
| **Auth** | JWT + Role-based Authorization |

---

## 🧱 Project Roadmap

### **Phase 1 – Core MVP**
> 🎯 Goal: first working SaaS with AI caption generator and planner.
- [ ] User registration & JWT authentication  
- [ ] AI Caption Generator (OpenAI API)  
- [ ] Basic content planner (draft saving, scheduling)  
- [ ] CQRS setup with MediatR  
- [ ] Stripe integration for paid plans  
- [ ] Basic admin dashboard (Blazor)  
- [ ] Docker & GitHub Actions CI/CD  
- [ ] Unit tests for Commands & Queries  

### **Phase 2 – Pro Platform**
> 🎯 Goal: expand into a full content management tool.
- [ ] Social account integrations (Instagram, TikTok, LinkedIn APIs)  
- [ ] Advanced scheduler (auto-posting)  
- [ ] Analytics dashboard (views, engagement rates)  
- [ ] Brand voice training (AI learns user tone)  
- [ ] Email notifications  
- [ ] Multi-user workspaces (teams)  

### **Phase 3 – Ecosystem**
> 🎯 Goal: build a scalable, multi-module ecosystem.
- [ ] Template store (caption packs, prompt kits)  
- [ ] Public API for developers  
- [ ] Plugin marketplace  
- [ ] Performance monitoring & telemetry  
- [ ] Localization (EN/NL/DE)  
- [ ] Multi-tenancy support  

---

## 🧠 Domains
| Domain | Description |
|---------|--------------|
| **Posts** | Captions, templates, content drafts |
| **Users** | User accounts, roles, authentication |
| **Billing** | Subscriptions, payments, invoices |
| **Analytics** | Performance metrics, feedback loop |
| **Scheduling** | Planning, auto-posting, reminders |

---

## 🧰 Setup (Development)
### 1️⃣ Clone repository
```bash
git clone https://github.com/<your-username>/CreatorSystem.git
cd CreatorSystem
dotnet new sln -n CreatorSystem
mkdir src
cd src
dotnet new webapi -n CreatorSystem.Api
dotnet new classlib -n CreatorSystem.Application
dotnet new classlib -n CreatorSystem.Domain
dotnet new classlib -n CreatorSystem.Infrastructure
cd ..
dotnet sln add src/CreatorSystem.*/*.csproj
dotnet add src/CreatorSystem.Application reference src/CreatorSystem.Domain
dotnet add src/CreatorSystem.Infrastructure reference src/CreatorSystem.Domain
dotnet add src/CreatorSystem.Infrastructure reference src/CreatorSystem.Application
dotnet add src/CreatorSystem.Api reference src/CreatorSystem.Application
dotnet add src/CreatorSystem.Api reference src/CreatorSystem.Infrastructure
dotnet add src/CreatorSystem.Application package MediatR
dotnet add src/CreatorSystem.Api package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add src/CreatorSystem.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add src/CreatorSystem.Infrastructure package Microsoft.EntityFrameworkCore.Design
dotnet add src/CreatorSystem.Infrastructure package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet new xunit -n CreatorSystem.Tests
dotnet sln add CreatorSystem.Tests/CreatorSystem.Tests.csproj
dotnet add CreatorSystem.Tests reference src/CreatorSystem.Application
dotnet test
cd src/CreatorSystem.Api
dotnet run
docker build -t creatorsystem-api .
docker run -p 8080:80 creatorsystem-api
