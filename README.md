# 📘 BlogMaster

BlogMaster is a full-stack blog application built using **ASP.NET Core Web API** and **Angular**. It provides a simple yet powerful platform for managing blog content with **role-based access control**, **JWT authentication**, and a clean user interface built with Angular Material.

## 🔐 Roles and Access

The application supports two types of users:

- **Admin**:
  - Can create, edit, and delete blog categories.
  - Can create, publish, and manage blog posts.
- **Normal User**:
  - Can view all blog posts created by admins.
  - Cannot create or modify any content.

## 🚀 Features

- 🛡️ **JWT Authentication**: Secure login and protected routes using token-based authentication.
- 🔑 **Role-Based Access Control (RBAC)**: Feature access is restricted based on user roles.
- 🧭 **Angular Route Guards**: Prevents unauthorized access to protected routes.
- 🌐 **HTTP Interceptors**: Automatically attaches JWT token to outgoing requests and handles error responses.
- 📚 **Category & Post Management** (for admins only).
- 🔍 **Filtering, Sorting, and Pagination** for blog post listings.
- 🧼 Clean UI built with **Angular Material** components.

## 🛠️ Tech Stack

### Backend
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server**
- **JWT for Authentication**

### Frontend
- **Angular**
- **Angular Material**
- **RxJS, Forms, HTTPClient**

### Dev Tools
- Visual Studio
- Visual Studio Code
- Postman (for testing APIs)
