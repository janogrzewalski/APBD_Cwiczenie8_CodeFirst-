using CourseLibrary.Models;

namespace CourseLibrary.Services;

public interface IBorrowingService
{
    Task CreateAsync(Borrowing borrowing);
}