# 🛍️ ASP.NET Web API – E-commerce Backend with Clean Architecture & Real-Time Features

🚀 This is a complete E-commerce backend solution built using **ASP.NET Web API** with a focus on **clean, scalable, and real-world architecture**. Designed using the **N-Layer Architecture** pattern to ensure separation of concerns, maintainability, and flexibility.

---

## 📌 Features

### 👨‍💻 Customer Side:
- 👤 **Login & Register** – Secure authentication with **JWT** and **Identity Roles**
- 🛒 **Browse Products** – Get product listings with filter and sort options
- 💖 **Wishlist** – Save favorite products for later
- 🛍️ **Cart** – Add and manage cart items before checkout
- 📦 **Place Orders** – Confirm orders with secure flow
- 📧 **Mail Service** – Receive order confirmation emails

### 🧑‍💼 Seller Side:
- 📋 **Product Management** – Create, update, and delete product listings
- 📉 **Inventory Alerts** – Automatic email alert when product quantity drops below 10
- 📲 **Real-Time Notifications** – Sellers get instant alerts when a customer places an order (SignalR or Notification Service)

---

## 🏗️ Tech Stack & Architecture

| Layer        | Responsibility                       |
|--------------|--------------------------------------|
| `Controllers`| Handle incoming API requests         |
| `Services`   | Business logic implementation        |
| `Repositories`| Data access using EF Core           |
| `Models`     | DTOs and Entity definitions          |

### 🔧 Technologies Used:
- ✅ **ASP.NET Web API (C#)**
- ✅ **Entity Framework Core**
- ✅ **JWT + Identity for Auth**
- ✅ **SignalR** / Notification Service
- ✅ **Mail Services (Gmail SMTP / Mailjet)**
- ✅ **SQL Server**
- ✅ **LINQ / AutoMapper**
- ✅ **N-Layer Architecture**

---

## 💡 Learning Goals

This project helped me:
- Understand **end-to-end flow** of an E-commerce system
- Implement **manual and token-based authentication**
- Apply **clean code and architecture principles**
- Use **real-time communication** with SignalR
- Work with **external services** like email integrations

---

## 📬 Mail Integration

- Sends inventory low-stock alerts every 2 days using **Gmail SMTP / Mailjet**
- Uses **`async/await`** to ensure the email process is non-blocking and efficient

---

## ⚙️ Installation & Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/aspnetcore-ecommerce-api.git
   cd aspnetcore-ecommerce-api
