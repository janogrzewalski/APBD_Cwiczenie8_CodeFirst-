using CourseLibrary.Models;

namespace CourseLibrary.Services;

public interface IAuthorService
{
    Task<List<Author>> GetAllAsync();
}