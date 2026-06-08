using CourseLibrary.Data;
using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Services;

public class AuthorService : IAuthorService
{
    private readonly LibraryDbContext _context;

    public AuthorService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _context.Authors
            .Include(a => a.Books)
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .ToListAsync();
    }
}