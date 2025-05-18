# Task Management System

## Description  
A **backend system** designed to efficiently manage daily tasks. Built using ASP.NET Core RESTful API, SQL Server, and T-SQL.

نظام خلفي (Backend) لإدارة المهام اليومية بكفاءة، مبني باستخدام ASP.NET Core RESTful API وقاعدة بيانات SQL Server.

---

## Key Features  
- **User Authentication**  
  - Secure login and signup using JWT  
  - Password encryption with bcrypt  
- **CRUD Operations**  
  - Create, Read, Update, and Delete tasks and users  
- **Role-Based Access Control**  
  - **User**: Manage own tasks  
  - **Administrator**: Manage all users and tasks  

---

## Technologies Used  
- **Backend:** ASP.NET Core RESTful API  
- **Database:** SQL Server with T-SQL stored procedures  
- **Authentication:** JWT (JSON Web Tokens), bcrypt  

---

##  API Endpoints  
| Endpoint              | Description                     | Access Level     |
|-----------------------|---------------------------------|------------------|
| `POST /auth/login`    | User authentication            | Public           |
| `POST /tasks`         | Create a new task              | Authenticated    |
| `GET /tasks/all`      | Get all tasks (Admin only)     | Admin            |
| `PUT /tasks/{id}`     | Update a task                  | Owner/Admin      |
| `DELETE /tasks/{id}`  | Delete a task                  | Owner/Admin      |


## API Documentation (Swagger UI)

You can explore and test the API interactively using Swagger UI.

### Swagger UI Screenshots

![Swagger Home](https://github.com/abdo7806/Task_management_project/blob/master/Swagger_Home.png?raw=true)  


## Installation & Setup  

1. Clone the repository:  
   ```bash
   https://github.com/abdo7806/Task_management_project.git

2. Configure SQL Server database using the provided scripts.

3. Build and run the ASP.NET Core API project in Visual Studio or your preferred IDE.

4. Use Postman or your frontend client to test the API endpoints.

---

## 👨‍💻 Developer | المطور

### 🙋‍♂️ About the Developer

I'm **Abdulsalam Dhahabi**, a passionate software developer with strong experience in desktop and web applications.

* C# WinForms & ASP.NET Core
* SQL Server & T-SQL
* Clean Architecture & Design Patterns
* Git & GitHub (2000+ problems solved)

🔗 GitHub: [github.com/abdo7806](https://github.com/abdo7806)
📧 Email: [balzhaby26@gmail.com](mailto:balzhaby26@gmail.com)
🌍 LinkedIn: [linkedin.com/in/abdulsalam-al-dhahabi-218887312](https://linkedin.com/in/abdulsalam-al-dhahabi-218887312)

---

## 🤝 Contributing | المساهمة

Contributions and feedback are welcome!
Feel free to open issues or submit pull requests.

---

## 📃 License | الترخيص

This project is open-source for learning and personal use only.  
هذا المشروع مفتوح المصدر لأغراض التعلم والاستخدام الشخصي فقط.
---

