# Moula Payment API

This is a sample application built using ASP.NET Core and Entity Framework Core using CQRS pattern.

## Design Pattern
### CQRS
The Command and Query Responsibility Segregation (CQRS) pattern separates read and update operations for a data store. Implementing CQRS in your application can maximize its performance, scalability, and security. The flexibility created by migrating to CQRS allows a system to better evolve over time and prevents update commands from causing merge conflicts at the domain level.
In simple terms 
* Queries = GET Methods
* Commands = POST/DELTE/PUT Methods

CQRS separates reads and writes into different models, using commands to update data, and queries to read data.
* Commands should be task based, rather than data centric. e.g Cancel Payment, Create Payment
* Commands may be placed on a queue for asynchronous processing, rather than being processed synchronously.
* Queries never modify the database. A query returns a DTO that does not encapsulate any domain knowledge.

### Benefits of CQRS 
* Independent scaling: CQRS allows the read and write workloads to scale independently
* Optimized data schemas: The read side can use a schema that is optimized for queries, while the write side uses a schema that is optimized for updates.
* Security: It's easier to ensure that only the right domain entities are performing writes on the data.
* Separation of concerns: Segregating the read and write sides can result in models that are more maintainable and flexible.
* Simpler queries: By storing a materialized view in the read database, the application can avoid complex joins when querying

### CQRS + MediatR
* Define commands and queries as requests
* Application layer is just a series of request/response objects
* Ability to attach additional behaviour pre and post request. e.g logging, validation, caching, authorisation and so on
* Easy to implement Event Sourcing

### Why not Repository pattern or Unit of Work?
In traditional architectures, the same data model is used to query and update a database. That's simple and works well for basic CRUD operations. In more complex applications, however, this approach can become unwieldy. For example, on the read side, the application may perform many different queries, returning data transfer objects (DTOs) with different shapes. Object mapping can become complicated. On the write side, the model may implement complex validation and business logic. As a result, you can end up with an overly complex model that does too much.
Repository and UoW pattern is already included in EF core so no need to add extra layer.
* EF Core insuates your code from database changes
* DbContext acts as unit of work
* DbSet acts as repository
* EF Core has features for unit testing without repositories

## Product Structure
Solution has 4 main projects and 2 test projects
##### Moula.Web 
Presentation and infrastructure depend only on application. 
* Controllers without any application logic
* Consume view models

##### Moula.Domain 
Domain contains enterprise-wide logic and types.
* Entities
* Logic
* Exceptions
* Enumerations

##### Moula.Persistence
This layer is independent of the database. Fluent API configuration is used over Data Annotations. Persistence contain
* DbContext
* Migrations (if not using in-memory DB)
* Configurations
* Abstractions

##### Moula.Application
Contains business-logic and types
* Interfaces
* Models
* Logic
* Commands/Queries
* Validators
* Exceptions
* Moula.Web.IntegrationTests
* Moula.Web.UnitTests


## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet/current)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
      ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Next, run unit and integration tests by running
      ```
     dotnet test
     ```
  5. within the `\Moula.Web` directory, launch the APIs by running:
     ```
	 dotnet run
	 ```
  6. Launch [https://localhost:5001/payment/get](http://localhost:5001/payment/get) in your browser to view the API

### API Endpoints
##### Get Payments

``` 
/payment/get 
```
This method will return the payments and balance for the current user(mocked in CurrentUserService).
Response: 
```
{
   "name":"Rajinder Singh",   
   "balance":10000,
   "payments":[
      {
         "id":"154f10e0-85be-4da7-9499-b05dbcc40b92",
         "amount":100,
         "date":"2020-10-06T15:44:54.7292054+11:00",
         "comment":"",
         "status":"Processed"
      },
      ...
      ...
   ]
}
```

##### Create Payment
```
/payment/create
```
Method: Post

Request: 
| Parameter | Type | Format |
| ------ | ------ | ------|
| amount | decimal | |
| date | DateTime| 'yyyy-mm-dd' |

Response: 200 OK | 400 Bad Request

##### Process Payment
```
/payment/process
```
Method: Post

Parameter 
| Parameter | Type | e.g. |
| ------ | ------ | ------|
| id | guid | b162e88d-a3a6-4341-87da-725658d743f3|

Response: 202 Accepted | 400 Bad Request

##### Cancel Payment
```
/payment/cancel
```
Method: Post

Parameter 
| Parameter | Type | e.g. |
| ------ | ------ | ------|
| id | guid | b162e88d-a3a6-4341-87da-725658d743f3|
| reason | string(optional) | |

Response: 202 Accepted | 400 Bad Request

## Frameworks and Libraries
* .NET Core 3.1
* MediatR (mediator pattern implementation for .NET)
* Entity Framework In-Memory Provider (for data access)
* Nunit
* FluentValidation
* Serilog

## CI Pipeline
```azure-pipelines.yml``` file has been added to setup CI pipeline in Azure devops. This file can be imported in Azure Devops to create artifacts. This pipeline has 5 steps
* Resstore nuget packages
* Build project
* Run unit tests
* Run Integration tests
* Publish web project
* Publish artifacts

## Environment settings
Web project has 3 ```appsettings.Environment.json``` files which will be selected based on ```ASPNETCORE_ENVIRONMENT``` variable on server. 
So different environments can have separate settings configured in these files.


### What's next?
I would like to implement Event Store. Events can be used for various reasons, like tracing the activity on the platform or rebuilding the state of the domain models at any specific point in time. In case something bad occurs, we have an almost immediate way to understand what went wrong and possibly how to fix the issue.

