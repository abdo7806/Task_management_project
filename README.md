# Task Management System

##  Description  
A **backend system** for managing daily tasks efficiently. Built with ASP.NET Core RESTful API, SQL Server, and T-SQL.  

##  Key Features  
- ** User Authentication**  
  - Secure login/signup with JWT  
  - Password encryption using bcrypt  
- ** CRUD Operations**  
  - Create, Read, Update, and Delete tasks and users  
- ** Role-Based Access Control**  
  - **User**: Manage own tasks  
  - **Administrator**: Manage all users and tasks  

##  Technologies Used  
- **Backend**: ASP.NET Core RESTful API  
- **Database**: SQL Server with T-SQL  
- **Authentication**: JWT, bcrypt  

##  API Endpoints  
| Endpoint              | Description                     | Access Level     |
|-----------------------|---------------------------------|------------------|
| `POST /auth/login`    | User authentication            | Public           |
| `POST /tasks`         | Create a new task              | Authenticated    |
| `GET /tasks/all`      | Get all tasks (Admin only)     | Admin            |
| `PUT /tasks/{id}`     | Update a task                  | Owner/Admin      |
| `DELETE /tasks/{id}`  | Delete a task                  | Owner/Admin      |

##  Installation  
1. Clone the repository:  
   ```bash
   https://github.com/abdo7806/Task_management_project.git
