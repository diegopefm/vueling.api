# vueling.api
Vueling candidate test api part.

This test includes three projects, two DotNet Core C# Apis (backend part) and an Angular 8 frontend. This is the first api backend part.

All projects are stored individually on three separate GitHub repositories for "separation of concerns".

Backend is developed using DotNet Core Api 2.1 and Visual Studio 2017.

Database is SQL server and uses Entity Framework as ORM.

Cors is enabled in api so to allow a cross domain request from client (only http://localhost:4200 is allowed for requests).

INSTALL AND RUN THE API:

1) Create a folder to store the app anywhere locally.

2) Install Source code management: Git (https://git-scm.com/downloads)

3) Open console and move to folder in step 1)

4) Get source code from console: git clone https://github.com/diegopefm/vueling.api.git

6) Launch Visual Studio, open solution file (Vueling.Api.sln) and launch with F5.

7) La api está configurada para correr en el puerto 55909. Si se ha de modificar dicho puerto se habrá de modificar también el appsettings.json y el environment.ts del front.

CONFIGURE THE DATABASE

Engine: SQL.
Instance Name: SQLEXPRESS
Windows Authentication: Off.
Database name: Vueling
User: vueling
Pass: vueling@Api2019

If SQL setup is different connection string can be modified in appsettings.json file.

IMPORTANT: Password is set in connection string in code in an encripted way for security reasons, so even if you need to change connection string DON'T MODIFY PASSWORD or connection to SQL will fail.

Notice: A complete backup of the database can be found in bbdd folder with name Vueling.SQL.bak 