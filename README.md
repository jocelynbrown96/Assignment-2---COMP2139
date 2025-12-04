# Assignment-2 — COMP2139

## TicketMe - Virtual Event Ticketing System

Welcome to **TicketMe**, a virtual event ticketing system built with **ASP.NET Core MVC** and **Entity Framework Core** using **PostgreSQL**. This application allows users to browse events, purchase tickets, and manage event listings.

---

## 📑 Table of Contents
- [Project Overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Database Configuration](#database-configuration)

---

## Project Overview

TicketMe provides the following features:

- Browse events by category and price
- Purchase tickets
- Add, edit, or delete events
- View purchase history and event details
- Clean, responsive UI with banners, filters, and alerts for low ticket stock

---

## Prerequisites

Before running the application, make sure you have installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [pgAdmin 4](https://www.pgadmin.org/download/)
- [JetBrains Rider](https://www.jetbrains.com/rider/)

---

## Database Configuration

TicketMe uses **PostgreSQL** for its database. The connection string is stored in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=TicketMeDB;Username=YOUR_DB_USERNAME;Password=YOUR_DB_PASSWORD"
}




