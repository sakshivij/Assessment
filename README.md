# Local Setup

Run the following command to setup the local environment

```console
foo@bar:~$ docker compose up
```

This would require Docker desktop to be installed on the system.

The docker compose will do the following:

- install and setup MYSql on one's machine
- migrate and create basic tables using flyway.
- Also insert/migrate data from csv file present in location data/scripts folder

Once the local setup is ready, the project can be furter tested on Visual Studio. This can be done by open the solution under path CountryGwp on Visual Studio.

# Tech Stack for the project

The API is built on .NET core console application. Along with that I have used the following:

- SQL Kata for query building
- Flyway for migration
- Swagger for documentation

The projects follows the separation of database from business logic and also follows SOLID principles.
