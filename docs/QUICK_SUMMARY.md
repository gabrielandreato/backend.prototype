## Quick Summary

---

### Architecture

---

This project is a dotnet backend, developed applying architecture concepts:
* Domain Driven Design;
* Microservices;

### Project design

---

The application is developed using a organizing pattern where for each domain it will contains an API with;
* API - Containing the startup project, the main configurations and the business logic in the services;
* DataLibrary - Containing all DB logic, with repositories, data context, DB migrations and mapping object 
configurations
* ModelLibrary - Containing all models for the project; 
* Testing layer - Unit test layer;

---

[Return to Readme](..%2F..%2FREADME.md)