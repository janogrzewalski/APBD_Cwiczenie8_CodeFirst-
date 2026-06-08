using CourseLibrary.Models;

namespace CourseLibrary.Services;

public interface IBookService
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetDetailsAsync(int id);
    Task<bool> AuthorExistsAsync(int authorId);
    Task CreateAsync(Book book);
}