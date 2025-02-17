# Welcome to Flight Server

This project is developed using **.net 9** with **Clean Architecture** folder structure and **CQRS**.
Patterns used in the current project :

 - Unit Of Work
 - Repository
 - Encapsulation across all the layers
 - DDD
 - EDD
 - Provider
 - CQRS
 - Mapping Profiling
 - Abstract Valiation
 - DI
 - ...


There are 2 hostable class libraries on the solution

 1. Idp
 2. Api

**Idp**: Is the identity server v7 which we use the interactional exchange flow for authenticating user
**Api** : Is the application rest api services which follows the REST full guidelines.
You can either run the projects seperately or together
In docker environment you can run them by entering the following command at the root of project `docker-compose up -d`
