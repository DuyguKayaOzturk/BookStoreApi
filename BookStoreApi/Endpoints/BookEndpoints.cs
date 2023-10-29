using Microsoft.AspNetCore.Builder;
using BookStoreApi.Model;
using BookStoreApi.Repository;

namespace BookStoreApi.Endpoints;

public static class BookEndpoints
{

    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("/books", GetBooks).WithName("GetBooks").WithOpenApi();
        app.MapGet("/books/{id}", GetBookById);
        app.MapPost("/books", AddBook).WithName("AddBook").WithOpenApi();
        app.MapDelete("/books/{id}", DeleteBook).WithName("DeleteBook").WithOpenApi();
        app.MapPut("/books/{id}", UpdateBook).WithName("UpdateBook").WithOpenApi();
    }

    private static IResult UpdateBook(IBookRepository repo, int id, Book book)
    {
        var bookToUpdate = repo.Update(id, book);
        if (bookToUpdate == null)
            return Results.NotFound("Could not find the book you are trying to update");

        return Results.Ok(bookToUpdate);

    }

    private static IResult DeleteBook(IBookRepository repo, int id)
    {
        var bookToDelete = repo.Delete(id);
        if (bookToDelete == null)
            return Results.NotFound("Could not find the book you want to delete");
        return Results.Ok(bookToDelete);
    }

    private static IResult GetBookById(IBookRepository repo, int id)
    {
        if (repo == null)
            return Results.NoContent();

        var p = repo.GetById(id);
        if (p != null)
            return Results.Ok(p);
        return Results.NotFound($"Couldn't find book with id={id}");
    }

    private static IResult GetBooks(IBookRepository repo, ILogger<Program> logger)
    {
        return Results.Ok(repo.GetAll().ToArray());
    }

    private static IResult AddBook(IBookRepository repo, Book book)
    {
        if (book != null)
        {
            var p = Results.Ok(repo.Add(book));
            if (p != null)
                return Results.Ok(p);
            return Results.NoContent();

        }
        return Results.BadRequest(book);
    }

}
