using CourseLibrary.Data;
using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Services;

public class BorrowingService : IBorrowingService
{
    private readonly LibraryDbContext _context;

    public BorrowingService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Borrowing borrowing)
    {
        var bookExists = await _context.Books
            .AnyAsync(b => b.Id == borrowing.BookId);

        if (!bookExists)
        {
            throw new InvalidOperationException("Nie mo¿na dodaæ wypo¿yczenia dla ksi¹¿ki, która nie istnieje.");
        }

        borrowing.BorrowedAt = DateTime.Now;

        _context.Borrowings.Add(borrowing);
        await _context.SaveChangesAsync();
    }
}