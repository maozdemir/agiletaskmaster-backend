# AgileTaskMaster

AgileTaskMaster is a project management tool that allows users to create and manage tasks, projects, teams, and permissions. It provides a user-friendly interface for organizing and tracking tasks, assigning tasks to team members, and managing project progress.

## The frontend of AgileTaskMaster is currently under development, and will be written in Flutter for mobile. The planned desktop and web clients will be written in React
This backend is not tested extensively and is not ready for production use.

## Features

- User authentication and authorization using JWT
- CRUD operations for tasks, projects, teams, and users
- Task dependencies to manage task relationships
- Task priorities to prioritize tasks
- Real-time notifications using SignalR
- Swagger documentation for API endpoints

## Technologies Used

- C# (.NET Core)
- MongoDB
- AutoMapper
- SignalR
- JWT Authentication
- Swagger

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community)

## Getting Started

1. Clone the repository:

   ```
   git clone https://github.com/maozdemir/agiletaskmaster-backend
   ```

2. Change to the project directory:

   ```
   cd agiletaskmaster-backend
   ```

3. Update the MongoDB connection string in `appsettings.json`:

   ```json
   "MongoDBSettings": {
       "ConnectionString": "mongodb://localhost:27017",
       "DatabaseName": "agiletaskmaster"
   }
   ```

4. Build and run the backend server:

   ```
   dotnet run
   ```

## API Endpoints

- **AuthController**
  - POST /api/auth/login - User login

- **DependencyController**
  - GET /api/dependency/{taskId} - Get dependencies by task ID
  - POST /api/dependency - Create a new dependency
  - DELETE /api/dependency/{dependencyId} - Delete a dependency

- **ProjectController**
  - GET /api/project - Get all projects
  - GET /api/project/{projectId} - Get a project by ID
  - POST /api/project - Create a new project
  - PUT /api/project/{projectId} - Update a project
  - DELETE /api/project/{projectId} - Delete a project
  - GET /api/project/{projectId}/tasks - Get tasks by project ID
  - POST /api/project/{projectId}/tasks/{taskId} - Add a task to a project
  - DELETE /api/project/{projectId}/tasks/{taskId} - Remove a task from a project
  - GET /api/project/{projectId}/teams - Get teams by project ID
  - POST /api/project/{projectId}/teams/{teamId} - Add a team to a project
  - DELETE /api/project/{projectId}/teams/{teamId} - Remove a team from a project

- **TaskController**
  - GET /api/task - Get all tasks
  - GET /api/task/{taskId} - Get a task by ID
  - POST /api/task - Create a new task
  - PUT /api/task/{taskId} - Update a task
  - DELETE /api/task/{taskId} - Delete a task
  - PUT /api/task/{taskId}/priority/{priority} - Update task priority

- **TeamController**
  - GET /api/team - Get all teams
  - GET /api/team/{teamId} - Get a team by ID
  - POST /api/team - Create a new team
  - PUT /api/team/{teamId} - Update a team
  - DELETE /api/team/{teamId} - Delete a team
  - POST /api/team/{teamId}/users/{userId} - Add a user to a team
  - DELETE /api/team/{teamId}/users/{userId} - Remove a user from a team

- **UserController**
  - GET /api/user - Get all users
  - GET /api/user/{userId} - Get a user by ID
  - POST /api/user - Create a new user
  - PUT /api/user/{userId} - Update a user
  - DELETE /api/user/{userId} - Delete a user

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
