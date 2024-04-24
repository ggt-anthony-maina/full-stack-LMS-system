// using HotChocolate;
// using HotChocolate.AspNetCore;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;

using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenizer, Tokenizer>(); // Register Tokenizer class
builder.Services.AddSingleton<PasswordHasher>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services
    .AddGraphQLServer()
    .AddTypeExtension<Query>()
    .AddTypeExtension<Mutation>();

   
   
// builder.Services.AddLogging(loggingBuilder =>
// {
//     loggingBuilder.ClearProviders();
//     loggingBuilder.AddSerilog(new LoggerConfiguration()
//         .WriteTo.Console()
//         .CreateLogger());
// });
       
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


///Enable CORS
 app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200") // Allow requests from this origin
           .AllowAnyHeader()
           .AllowAnyMethod();
});




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// app.UseMiddleware<AuthMiddleware>(); // Add this line

app.MapControllers();

app.MapGraphQL();

app.Run();

// Define your LibraryDbContext



public class LibraryDbContext : DbContext
{

   ///i added the line below for tests but it can be removed
       public LibraryDbContext() { }


       //this is the end of the line 
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public  virtual DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BorrowedBook>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId);
    }
}

