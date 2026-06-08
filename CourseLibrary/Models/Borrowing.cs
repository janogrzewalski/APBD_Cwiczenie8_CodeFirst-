using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.Models;

public class Borrowing
{
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    public Book? Book { get; set; }

    [Required(ErrorMessage = "Imiê i nazwisko wypo¿yczaj¹cego jest wymagane.")]
    [MaxLength(150)]
    public string BorrowerName { get; set; } = string.Empty;

    public DateTime BorrowedAt { get; set; }

    public DateTime? ReturnedAt { get; set; }
}