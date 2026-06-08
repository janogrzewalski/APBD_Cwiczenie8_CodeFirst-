using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Data;

public static class LibrarySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

        await context.Database.MigrateAsync();

        if (await context.Authors.AnyAsync())
        {
            return;
        }

        var authors = new List<Author>
        {
            new Author
            {
                FirstName = "Adam",
                LastName = "Mickiewicz"
            },
            new Author
            {
                FirstName = "Henryk",
                LastName = "Sienkiewicz"
            },
            new Author
            {
                FirstName = "Stanis³aw",
                LastName = "Lem"
            }
        };

        context.Authors.AddRange(authors);
        await context.SaveChangesAsync();

        var books = new List<Book>
        {
            new Book
            {
                Title = "Solaris",
                Isbn = "9788374801324",
                PublishedYear = 1961,
                AuthorId = authors[2].Id
            },
            new Book
            {
                Title = "Bajki robotów",
                Isbn = "9788308068991",
                PublishedYear = 1964,
                AuthorId = authors[2].Id
            },
            new Book
            {
                Title = "Cyberiada",
                Isbn = "9788308069004",
                PublishedYear = 1965,
                AuthorId = authors[2].Id
            },
            new Book
            {
                Title = "Krzy¿acy",
                Isbn = "9788373271937",
                PublishedYear = 1900 + 1,
                AuthorId = authors[1].Id
            },
            new Book
            {
                Title = "W pustyni i w puszczy",
                Isbn = "9788373271944",
                PublishedYear = 1911,
                AuthorId = authors[1].Id
            }
        };

        context.Books.AddRange(books);
        await context.SaveChangesAsync();
    }
}