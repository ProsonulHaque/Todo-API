# Todo-API

This is a .NET Core web API. It performs the following actions:

* POST todo  => id, task title and datetime 
* GET todo/all => Get all tasks
* PUT todo/{id} => Update(task title, datetime) a task by id
* PATCH todo/{id}  => Update datetime of a task by id
* DELETE todo/{id} => Delete record by id
* GET todo/datetime=searchValue => Get all tasks filtered by datetime and sorted by taskname. [Only this API is served from Cassandra DB]

## Tech Stack:

* .NET core 3.1 
* Entity Framework Core
* MS SQL Server
* Cassandra DB 
* Postman to check
