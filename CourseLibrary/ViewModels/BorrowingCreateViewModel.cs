using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseLibrary.ViewModels;

public class BorrowingCreateViewModel
{
    [Required(ErrorMessage = "Książka jest wymagana.")]
    public int BookId { get; set; }

    [Required(ErrorMessage = "Imię i nazwisko wypożyczającego jest wymagane.")]
    public string BorrowerName { get; set; } = string.Empty;

    public List<SelectListItem> Books { get; set; } = new();
}