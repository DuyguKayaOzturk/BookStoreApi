# BookStoreApi

## Claim:
1.Model: Create a class called 'Book' with the following properties:

-Id (int): A unique identifier for the book.
-Title (string): The book's title.
-Author (string): The author of the book.
-PublicationYear (int): The year the book was published.
-ISBN (string): The international standard book number for the book.
-InStock (int): The amount of this book available in the bookstore.

2.Database:

-Create a simple database (for example using SQL Server) with a table representing Book.
-Use ADO.net to connect and perform CRUD operations against the database.

3.API Endpoints:

-GET /books
•Returns a list of all books.
-GET /books/{id}
•Returns a book based on ID.
-POST /books
•Adding a new book.
-PUT /books/{id}
•Updates an existing book based on ID.
-DELETE /books/{id}
•Deletes a book based on ID.

4. Filtering: Implement the option to filter the book list based on ▪Year of publication and Author through query parameters.
   
-For example:
-GET /books?publicationYear=2022(should return books published in 2022)
-GET /books?author=Ola(should return books written by an author named "Ola")
-GET /books?title=[title]Returns books based on title.

## Instructions:

1.Create a new ASP.NET Core minimal API application.

2.Design the database and create it using the MySQL script

3.Use ADO.net to perform CRUD operations.

4.Implemented the necessary endpoints in your minimal API.

5.Test your API with an API client such as Postman, Swagger, IDE tool for http request or using a web browser
