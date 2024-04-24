using HotChocolate;


public class Query
{
    public IQueryable<User> GetUsers([Service] LibraryDbContext dbContext) =>
        dbContext.Users;

    public User? GetUserById(int id, [Service] LibraryDbContext dbContext) =>
        dbContext.Users.FirstOrDefault(u => u.Id == id.ToString());

          public IQueryable<Course> GetCourses([Service] LibraryDbContext dbContext) =>
        dbContext.Courses;

    public Course? GetCourseById(int id, [Service] LibraryDbContext dbContext) =>
        dbContext.Courses.FirstOrDefault(c => c.Id == id);


    public Book? GetBook() =>
        new Book
        {
            Title = "C# in depth.",
            Author = new Author
            {
                Name = "Jon Skeet"
            }
        };

         public IQueryable<Book> GetBooks([Service] LibraryDbContext dbContext) =>
        dbContext.Books;

    public Book? GetBookById(int id, [Service] LibraryDbContext dbContext) =>
        dbContext.Books.FirstOrDefault(b => b.Id == id);
}

