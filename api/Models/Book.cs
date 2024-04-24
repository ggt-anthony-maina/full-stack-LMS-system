public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public Author? Author { get; set; }

      public int AuthorId { get; set; } // Added AuthorId property
    public Genre? Genre { get; set; }

      public int GenreId { get; set; } // Added AuthorId property
    public int Copies { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class BorrowedBook
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }

   
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
