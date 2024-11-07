# WiseReminder

**WiseReminder** is a Web API designed to generate philosophical quotes stored in a database. Users can browse for quotes, their categories, and authors. There is also an additional part developed from the administrator's point of view, which can add, update or delete all entities

## Structure of the subject area

The subject area includes three classes that are used in the form of Entity:

1. Category is a certain abstraction for categorizing quotes by a certain topic

![image](https://github.com/user-attachments/assets/b8a88047-627d-4938-a907-2725384b0d43)

2. Author is a person who wrote certain quotes, i.e. is their author

![image](https://github.com/user-attachments/assets/fb03bda3-f578-4ba8-b7bf-e29101626c0e)

3. Quote is a quote that contains a category and an author

![image](https://github.com/user-attachments/assets/c714cb30-61f8-4417-9121-2b74547776ba)

## Subject area diagram

![image](https://github.com/user-attachments/assets/e1946acd-b901-4590-8f66-7d1ef6a1efdf)

## API methods

![image](https://github.com/user-attachments/assets/1003ea57-f3ac-4a40-80a1-a96ddb2a4918)

### Admin

- **POST** `/api/admin/login` - login as admin
- **POST** `/api/admin/logout` - logout as admin

### Authors

- **POST** `/api/authors/create` - create new author
- **GET** `/api/authors/{id}` - get author by his id
- **GET** `/api/authors/all` - get all authors
- **PUT** `/api/authors/update` - update author
- **DELETE** `/api/authors/delete` - delete author

### Categories

- **POST** `/api/categories/create` - create new category
- **GET** `/api/categories/{id}` - get category by its id
- **GET** `/api/categories/all` - get all categories
- **PUT** `/api/categories/update` - update category
- **DELETE** `/api/categories/delete` - delete category

### Quotes

- **POST** `/api/quotes/create` - create new quote
- **GET** `/api/quotes/{id}` - get quote by its id
- **GET** `/api/quotes/by-author/{authorId}` - get all quotes by author id
- **GET** `/api/quotes/by-category/{categoryId}` - get all quotes by category id
- **GET** `/api/quotes/random` - get random quote
- **PUT** `/api/quotes/update` - update quote
- **DELETE** `/api/quotes/delete` - delete quote

## Authorization

The `POST`, `PUT`, `DELETE` methods require authorization through a JWT token. Authorization is carried out through the **login** method. After successful login, the user receives a JWT token that must be added to requests to perform secure operations. But it should be noted that the system was implemented from the point of view that we can have only one admin. Therefore, no separate entities were created in the database for this purpose. Other methods, such as **getting lists of quotes, categories, authors**, as well as **getting a random quote** are available without authorization for all users.

## Technologies

The project uses the following technologies and architectural approaches:
- Clean Architecture
- Domain-Driven Design (DDD)
- MediatR
- AutoMapper
- JWT Bearer
- Entity Framework Core
- Microsoft SQL Server
