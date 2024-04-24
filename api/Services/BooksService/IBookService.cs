public interface IBookService
{
    Task<Book?> GetBookByIdAsync(int id);
    Task<ServiceResponse<bool>> CreateBookAsync(BookDTO bookDTO);
    Task<ServiceResponse<bool>> UpdateBookAsync(BookDTO bookDTO);
    Task<ServiceResponse<bool>> DeleteBookAsync(int id);
}

public class BookService : IBookService
{
    private readonly LibraryDbContext _dbContext;

    public BookService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _dbContext.Books.FindAsync(id);
    }

    public async Task<ServiceResponse<bool>> CreateBookAsync(BookDTO bookDTO)
    {
        try
        {
            var book = new Book
            {
                Title = bookDTO.Title!,
                AuthorId = bookDTO.AuthorId,
                GenreId = bookDTO.GenreId,
                Copies = bookDTO.Copies
            };

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Book created successfully.",
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating book: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to create book.",
                StatusCode = 500
            };
        }
    }

    public async Task<ServiceResponse<bool>> UpdateBookAsync(BookDTO bookDTO)
    {
        try
        {
            var book = await _dbContext.Books.FindAsync(bookDTO.Id);

            if (book == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Book not found.",
                    StatusCode = 404
                };
            }

            book.Title = bookDTO.Title!;
            book.AuthorId = bookDTO.AuthorId;
            book.GenreId = bookDTO.GenreId;
            book.Copies = bookDTO.Copies;

            await _dbContext.SaveChangesAsync();
            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Book updated successfully.",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to update book.",
                StatusCode = 500
            };
        }
    }

    public async Task<ServiceResponse<bool>> DeleteBookAsync(int id)
    {
        try
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Book not found.",
                    StatusCode = 404
                };
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Book deleted successfully.",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to delete book.",
                StatusCode = 500
            };
        }
    }
}
