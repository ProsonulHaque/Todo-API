# Todo-API

This is a .NET Core web API. It performs the following actions:

I.  POST todo  => id, task title and datetime 
ii. GET todo/all => Get all tasks
iii. PUT todo/{id} => Update(task title, datetime) a task by id
iv. PATCH todo/{id}  => Update datetime of a task by id
v. DELETE todo/{id} => Delete record by id
vi. GET todo/datetime=searchValue => Get all tasks filtered by datetime and sorted by taskname. [Only this API is served from Cassandra DB]

## Tech Stack:

.net core 3.1
Entity Framework Core
MS SQL Server
Cassandra DB 
Postman to check
