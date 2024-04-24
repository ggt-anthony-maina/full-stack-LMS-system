using HotChocolate;
using Microsoft.EntityFrameworkCore;


public class Mutation
{
   public async Task<ServiceResponse<User>> CreateUser(string username, string password, string email, [Service] LibraryDbContext dbContext)
{
    try
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (existingUser != null)
        {
            return new ServiceResponse<User>
            {
                Success = false,
                Message = "A user with this email already exists. Please choose another email.",
                StatusCode = 400
            };
        }

        var passwordHasher = new PasswordHasher();
        var hashedPassword = passwordHasher.HashPassword(password);

        var user = new User { Id = Guid.NewGuid().ToString(), Username = username, Password = hashedPassword, Email = email };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        Console.WriteLine($"User created: Username - {user.Username}, Email - {user.Email}");

        return new ServiceResponse<User>
        {
            Data = user,
            Message = "User created successfully.",
            StatusCode = 201
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating user: {ex.Message}");
        return new ServiceResponse<User>
        {
            Success = false,
            Message = "Failed to create user.",
            StatusCode = 500
        };
    }
}


   
public ServiceResponse<User> UpdateUser(string id, string username, string password, string email, [Service] LibraryDbContext dbContext)
{
    try
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id.ToString());
        if (user == null)
        {
            return new ServiceResponse<User>
            {
                Success = false,
                Message = "User not found.",
                StatusCode = 404
            };
        }
        
        var passwordHasher = new PasswordHasher();
        user.Username = username ?? user.Username;
        user.Password = passwordHasher.HashPassword(password) ?? user.Password;
        user.Email = email ?? user.Email;

        dbContext.SaveChanges();

        return new ServiceResponse<User>
        {
            Data = user,
            Message = "User updated successfully.",
            StatusCode = 200
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating user: {ex.Message}");
        return new ServiceResponse<User>
        {
            Success = false,
            Message = "Failed to update user.",
            StatusCode = 500
        };
    }
}


    public ServiceResponse<bool> DeleteUser(string id, [Service] LibraryDbContext dbContext)
{
    try
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

        if (user == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not found.",
                StatusCode = 404
            };
        }

        dbContext.Users.Remove(user);
        dbContext.SaveChanges();

        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "User deleted successfully.",
            StatusCode = 200
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting user: {ex.Message}");
        return new ServiceResponse<bool>
        {
            Success = false,
            Message = "Failed to delete user.",
            StatusCode = 500
        };
    }
}



    
   public ServiceResponse<Course> CreateCourse(int id, string name, string description, [Service] LibraryDbContext dbContext)
{
    try
    {
        var course = new Course { Id  = id, Name = name, Description = description };
        dbContext.Courses.Add(course);
        dbContext.SaveChanges();

        return new ServiceResponse<Course>
        {
            Data = course,
            Message = "Course created successfully.",
            StatusCode = 200
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating course: {ex.Message}");
        return new ServiceResponse<Course>
        {
            Success = false,
            Message = "Failed to create course.",
            StatusCode = 500
        };
    }
}


    public Course UpdateCourse(int id, string name, string description, [Service] LibraryDbContext dbContext)
    {
        var course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            throw new Exception("Course not found");
        }

        course.Name = name ?? course.Name;
        course.Description = description ?? course.Description;

        dbContext.SaveChanges();
        return course;
    }

    public bool DeleteCourse(int id, [Service] LibraryDbContext dbContext)
    {
        var course = dbContext.Courses.FirstOrDefault(c => c.Id == id);
        if (course == null)
        {
            throw new Exception("Course not found");
        }

        dbContext.Courses.Remove(course);
        dbContext.SaveChanges();
        return true;
    }


    
    public Book CreateBook(string title, int authorId, int genreId, int copies, [Service] LibraryDbContext dbContext)
    {
        var book = new Book
        {
            Title = title,
            AuthorId = authorId,
            GenreId = genreId,
            Copies = copies
        };
        dbContext.Books.Add(book);
        dbContext.SaveChanges();
        return book;
    }

    public Book UpdateBook(int id, string title, int authorId, int genreId, int copies, [Service] LibraryDbContext dbContext)
    {
        var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        book.Title = title ?? book.Title;
        book.AuthorId = authorId;
        book.GenreId = genreId;
        book.Copies = copies;

        dbContext.SaveChanges();
        return book;
    }

    public bool ReturnBook(int userId, int bookId, [Service] LibraryDbContext dbContext)
{
    var borrowedBook = dbContext.BorrowedBooks
        .FirstOrDefault(bb => bb.UserId == userId.ToString() && bb.BookId == bookId && bb.ReturnDate == null);

    if (borrowedBook == null)
    {
        throw new Exception("Book not found or already returned");
    }

    borrowedBook.ReturnDate = DateTime.UtcNow;
    dbContext.SaveChanges();

    return true;
}


public bool BorrowBook(int userId, int bookId, [Service] LibraryDbContext dbContext)
{
    // Check if the user has already borrowed the book and not returned it
    if (dbContext.BorrowedBooks.Any(bb => bb.UserId == userId.ToString() && bb.BookId == bookId && bb.ReturnDate == null))
    {
        throw new Exception("User has already borrowed the book and not returned it");
    }

    var borrowedBook = new BorrowedBook
    {
        UserId = userId.ToString(),
        BookId = bookId,
        BorrowDate = DateTime.UtcNow
    };

    dbContext.BorrowedBooks.Add(borrowedBook);
    dbContext.SaveChanges();

    return true;
}


    public bool DeleteBook(int id, [Service] LibraryDbContext dbContext)
    {
        var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            throw new Exception("Book not found");
        }

        dbContext.Books.Remove(book);
        dbContext.SaveChanges();
        return true;
    }
}

// Define similar classes for CourseQuery, CourseMutation, BookQuery, and BookMutation
