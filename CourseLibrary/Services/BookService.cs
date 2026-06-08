using CourseLibrary.Data;
using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Services;

public class BookService : IBookService
{
    private readonly LibraryDbContext _context;

    public BookService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .OrderBy(b => b.Title)
            .ToListAsync();
    }

    public async Task<Book?> GetDetailsAsync(int id)
    {
        return await _context.Books
            .Include(b => b.Author)
            .Include(b => b.Borrowings)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<bool> AuthorExistsAsync(int authorId)
    {
        return await _context.Authors.AnyAsync(a => a.Id == authorId);
    }

    public async Task CreateAsync(Book book)
    {
        if (!await AuthorExistsAsync(book.AuthorId))
        {
            throw new InvalidOperationException("Wybrany autor nie istnieje.");
        }

        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }
}