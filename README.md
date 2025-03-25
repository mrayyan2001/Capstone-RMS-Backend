# Capstone Restaurant Management System (Backend)

![.NET Core](https://img.shields.io/badge/.NET_Core-8.0-blue) ![SQL Server](https://img.shields.io/badge/SQL_Server-2022-green)

## 📖 Overview

This repository contains the **backend API** for a restaurant management system, built with **ASP.NET Core 8** and **SQL Server**. It supports a multi-role system (Admin, Client, Delivery Captain) and integrates with a Flutter-based mobile/frontend application (separate repository).

The API handles:

- Authentication & Authorization (JWT)
- Role-based access control (Super Admin, Admin, Client, Driver)
- Order lifecycle management (placement, tracking, delivery)
- Menu & category management
- Discount/offers system
- Real-time notifications
- Reporting and analytics (future stages)

---

## 🚀 Features

✅ **Authentication**

- JWT token generation with email verification.
- Password reset via OTP.
- Role-based permissions (Super Admin, Admin, Client, Driver).

✅ **Order Management**

- Order placement, assignment to drivers, and status updates.
- Real-time tracking integration.

✅ **Menu & Inventory**

- CRUD operations for menu items, categories, and options.
- Discount/offers management (time-bound, percentage-based).

✅ **Delivery System**

- Driver assignment and delivery tracking.
- Integration with maps for real-time location updates.

✅ **Admin Panel**

- User management (clients, drivers, admins).
- Analytics (sales, orders, ratings).

---

## 🛠 Technologies

- **Framework**: ASP.NET Core 8 (Web API)
- **Database**: SQL Server 2022
- **Authentication**: JWT, SHA-512 hashing
- **API Docs**: Swagger/OpenAPI
