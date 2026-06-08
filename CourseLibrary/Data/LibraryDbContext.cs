using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Borrowing> Borrowings => Set<Borrowing>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(b => b.Isbn)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(b => b.Isbn)
                .IsUnique();

            entity.Property(b => b.PublishedYear)
                .IsRequired();

            entity.HasMany(b => b.Borrowings)
                .WithOne(br => br.Book)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.BorrowerName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(b => b.BorrowedAt)
                .IsRequired();

            entity.Property(b => b.ReturnedAt)
                .IsRequired(false);
        });
    }
}