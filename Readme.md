#  API

Example API that shows how to implement CRUD operations with ASP.NET Core 2.1.


### How to test

You will need a way to test the API endpoints. I recommend you to use [Postman](https://www.getpostman.com/).

First of all, clone this repository and open it in terminal. Then restore all dependencies and run the project. Since it is configured to use [Entity Framework InMemory](https://docs.microsoft.com/en-us/ef/core/providers/in-memory/) provider, the project will run without any issues.

```sh
$ git clone https://github.com/Qbrayan/Safaricomhack.git
$ cd SafHackathon/SafHackathon
$ dotnet restore
$ dotnet run
```
OR you can create a docker image and run the containerized app

