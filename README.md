# GemQuest (Ecommercegq)

An innovative, purpose-driven e-commerce web application designed to empower rural communities by connecting local artisans, farmers, and small business owners with a broader national and global marketplace.

## Table of Contents

- [Overview](#overview)
- [Objectives](#objectives)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Implementation Plan](#implementation-plan)
- [User Interface (UI) Design](#user-interface-ui-design)
- [Testing Plan](#testing-plan)
- [Deployment](#deployment)
- [Expected Outcome](#expected-outcome)
- [Getting Started](#getting-started)
- [Contributing](#contributing)
- [License](#license)
- [References](#references)
- [Appendix](#appendix)
- [Contact](#contact)

---

## Overview

**GemQuest (Ecommercegq)** is a dynamic and inclusive e-commerce platform that replicates the energy of a real-world marketplace while providing seamless digital access. With secure authentication, intuitive product browsing, interactive shopping cart, and robust order processing, the platform is built for both usability and scalability. It especially empowers rural communities to participate in the digital economy by making it easy for local producers to reach wider audiences.

## Objectives

- Bridge the digital gap for rural artisans, farmers, and small businesses.
- Provide a scalable, easy-to-use platform for showcasing and selling products.
- Foster economic growth in underserved regions by connecting local products with national and global customers.
- Support both vendors and customers with secure, role-based access.

## Features

- **User Registration & Authentication:** Secure signup and login for customers and vendors, with role-based access control.
- **Product Catalog:** Browse by category, view images, read descriptions, and filter products.
- **Shopping Cart & Checkout:** Add/remove products, manage quantities, and place orders with simulated payment processing.
- **Admin Dashboard:** Manage inventory, orders, user accounts, and monitor system activity.
- **Wishlist & Reviews:** Save favorite products and leave feedback to build trust.
- **Order Tracking:** Real-time status updates for user orders.
- **Personalized Options:** Select product variants and preferences.
- **Customer Support:** Contact form for inquiries and assistance.

## Technology Stack

- **Frontend:** HTML5, CSS/SCSS, Bootstrap, JavaScript
- **Backend:** ASP.NET Core (C#), Dapper or Entity Framework Core
- **Database:** MySQL, MySQL Workbench (design, stored procedures)
- **Dev Tools:** Visual Studio 2022, Git, GitHub
- **Cloud Hosting:** Microsoft Azure App Service or Amazon EC2

## Implementation Plan

1. **Setup:** Initialize solution structure in Visual Studio; design MySQL schema with Workbench.
2. **Backend:** Build core logic in C#/ASP.NET Core; integrate MySQL via Dapper/EF Core.
3. **Frontend:** Develop responsive UI using HTML, CSS, Bootstrap, and JavaScript.
4. **Admin Features:** Implement role-based access controls and admin workflows.
5. **Testing:** Rigorous manual and automated testing of user flows and backend logic.
6. **Deployment:** Prepare for cloud deployment, SSL certificates, and scaling strategies.

## User Interface (UI) Design

- **Mobile-first, responsive, and accessible**
- **Core Screens:** Home, Product Listing, Product Details, Cart, Checkout, Login/Register, Admin Dashboard
- **Principles:** Consistency, clarity, minimalism, and accessibility (high-contrast themes, large touch targets)
- **Experience:** Fast load times, clear feedback, robust error handling, and cross-device usability

## Testing Plan

- **Unit Testing:** xUnit for C# logic (orders, filtering, authentication)
- **Integration Testing:** Database/API/frontend workflows (checkout, registration)
- **Manual Testing:** UI, device, and real-world scenario coverage
- **Database Testing:** MySQL stored procedures for data integrity and performance

## Deployment

- **Cloud Hosting:** Azure App Service or AWS EC2
- **Security:** SSL certificates via Let’s Encrypt/Cloudflare
- **Scalability:** Horizontal scaling, load balancing
- **Monitoring:** Performance tracking, uptime, and usage analytics

## Expected Outcome

- Fully functional, user-friendly e-commerce platform
- Responsive frontend, secure authentication, comprehensive admin dashboard
- MySQL-backed data handling with stored procedures
- Features: product browsing, shopping cart, order management, reviews, and support
- Deployed on scalable cloud infrastructure, ready for real vendors and customers

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Fudgiethewhal/GemQuest.git
   ```
2. **Set up the database:**
   - Import the provided SQL schema into MySQL.
   - Configure your connection string in the backend project.
3. **Run the backend:**
   - Open in Visual Studio 2022 and start the ASP.NET Core project.
4. **Run the frontend:**
   - Open `index.html` or start the project via your preferred static server.
5. **Access the app:**
   - Visit `http://localhost:<port>` in your browser.

*For detailed instructions, refer to the `/docs` folder (if available) or project wiki.*

## Contributing

Contributions are welcome! Please open an issue or submit a pull request. For major changes, discuss them first via an issue.

- Fork the repo and create your branch from `main`.
- Follow code style guidelines and add relevant tests.
- See `CONTRIBUTING.md` for more details (if available).

## License

This project is licensed under the [MIT License](LICENSE).

## References

- Microsoft ASP.NET Documentation
- TrueCoders Resources
- MySQL Workbench User Guide
- W3Schools, Bootstrap Docs
- Stack Overflow, GitHub Repositories
- "Clean Code" by Robert C. Martin

## Appendix

- **Database Schema:** 13+ tables (Users, Product, Category, Orders, Cart, Payment, etc.) with foreign keys and stored procedures
- **Stored Procedures:** Efficient product retrieval, order processing, etc.
- **UI Mockups:** Visual wireframes for homepage, product listings, checkout, admin dashboard
- **Source Code:** Structured for maintainability and scalability; see repository

## Contact

Project by **Bryseida Hernandez**  
TrueCoders Student  
For inquiries, contact via [GitHub](https://github.com/Fudgiethewhal) or open an issue in this repository.

---

> Ecommercegq is more than a website—it's a tool for growth, connection, and empowerment in the digital age.
