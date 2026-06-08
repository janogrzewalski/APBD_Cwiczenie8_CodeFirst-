using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseLibrary.ViewModels;

public class BookCreateViewModel
{
    [Required(ErrorMessage = "Tytuł jest wymagany.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN jest wymagany.")]
    public string Isbn { get; set; } = string.Empty;

    [Range(1901, int.MaxValue, ErrorMessage = "Rok wydania musi być większy niż 1900.")]
    public int PublishedYear { get; set; }

    [Required(ErrorMessage = "Autor jest wymagany.")]
    public int AuthorId { get; set; }

    public List<SelectListItem> Authors { get; set; } = new();
}