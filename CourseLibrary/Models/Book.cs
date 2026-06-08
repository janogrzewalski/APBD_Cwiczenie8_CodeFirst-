using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytu³ jest wymagany.")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN jest wymagany.")]
    [MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    [Range(1901, int.MaxValue, ErrorMessage = "Rok wydania musi byæ wiêkszy ni¿ 1900.")]
    public int PublishedYear { get; set; }

    [Required]
    public int AuthorId { get; set; }

    public Author? Author { get; set; }

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}