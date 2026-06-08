using CourseLibrary.Data;
using CourseLibrary.Models;
using CourseLibrary.Services;
using CourseLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Controllers;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly LibraryDbContext _context;

    public BooksController(IBookService bookService, LibraryDbContext context)
    {
        _bookService = bookService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllAsync();
        return View(books);
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookService.GetDetailsAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    public async Task<IActionResult> Create()
    {
        var model = new BookCreateViewModel
        {
            Authors = await GetAuthorSelectListAsync()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Authors = await GetAuthorSelectListAsync();
            return View(model);
        }

        var book = new Book
        {
            Title = model.Title,
            Isbn = model.Isbn,
            PublishedYear = model.PublishedYear,
            AuthorId = model.AuthorId
        };

        try
        {
            await _bookService.CreateAsync(book);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError("", exception.Message);
            model.Authors = await GetAuthorSelectListAsync();
            return View(model);
        }
    }

    private async Task<List<SelectListItem>> GetAuthorSelectListAsync()
    {
        return await _context.Authors
            .OrderBy(a => a.LastName)
            .ThenBy(a => a.FirstName)
            .Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FirstName + " " + a.LastName
            })
            .ToListAsync();
    }
}