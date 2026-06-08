using CourseLibrary.Data;
using CourseLibrary.Models;
using CourseLibrary.Services;
using CourseLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Controllers;

public class BorrowingsController : Controller
{
    private readonly IBorrowingService _borrowingService;
    private readonly LibraryDbContext _context;

    public BorrowingsController(IBorrowingService borrowingService, LibraryDbContext context)
    {
        _borrowingService = borrowingService;
        _context = context;
    }

    public async Task<IActionResult> Create(int? bookId)
    {
        var model = new BorrowingCreateViewModel
        {
            BookId = bookId ?? 0,
            Books = await GetBookSelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BorrowingCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Books = await GetBookSelectListAsync();
            return View(model);
        }

        var borrowing = new Borrowing
        {
            BookId = model.BookId,
            BorrowerName = model.BorrowerName,
            BorrowedAt = DateTime.Now
        };

        try
        {
            await _borrowingService.CreateAsync(borrowing);
            return RedirectToAction("Details", "Books", new { id = model.BookId });
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError("", exception.Message);
            model.Books = await GetBookSelectListAsync();
            return View(model);
        }
    }

    private async Task<List<SelectListItem>> GetBookSelectListAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .OrderBy(b => b.Title)
            .Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Title + " - " + b.Author!.FirstName + " " + b.Author.LastName
            })
            .ToListAsync();
    }
}