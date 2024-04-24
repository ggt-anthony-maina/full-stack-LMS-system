using Xunit;
using Moq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



public class UserServiceTests
{


    private Mock<LibraryDbContext> dbContextMock;
    private Mock<PasswordHasher> passwordHasherMock;
    private UserService userService;

    public UserServiceTests()
    {
        dbContextMock = new Mock<LibraryDbContext>();
        passwordHasherMock = new Mock<PasswordHasher>();
        userService = new UserService(dbContextMock.Object, passwordHasherMock.Object);
    }

    [Fact]

    public async Task GetUserByEmailAsync_WhenUserExists_ReturnUser()
    {

        try
        {
            //Arrange 
           

            var userEmail = "test@example.com";
            var existingUser = new User { Email = userEmail , Password = "examplePassword"  };


            dbContextMock.Setup(m => m.Users.FirstOrDefaultAsync(u => u.Email == userEmail, CancellationToken.None))
                .ReturnsAsync(existingUser);



            // passwordHasherMock.Setup(m => m.VerifyPassword(existingUser.Password, password))   
            // .Returns(true);


            //Act
            var result = await userService.GetUserByEmailAsync(userEmail);



            //Assert
            Assert.NotNull(result);
            Assert.Equal(existingUser, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserByEmailAsync_WhenUserExists_ReturnsUser: {ex.Message}");
            throw;
        }
    }

    [Fact]
     public async Task GetUserByEmailAsync_WhenUserDoesNotExist_ReturnsNull()
    {
        try
        {
            // Arrange
          

            var userEmail = "test@example.com";

            dbContextMock.Setup(m => m.Users.FirstOrDefaultAsync(u => u.Email == userEmail, CancellationToken.None))
                .Returns(Task.FromResult<User?>(null));

            // Act
            var result = await userService.GetUserByEmailAsync(userEmail);

            // Assert
            Assert.Null(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserByEmailAsync_WhenUserDoesNotExist_ReturnsNull: {ex.Message}");
            throw;
        }
    }

     [Fact]
    public async Task GetUserByIdAsync_WhenUserExists_ReturnsUser()
    {
        try
        {
            
             // Arrange
      

        var userId = "1";
        var existingUser = new User { Id = userId , Password = "examplePassword" };

       dbContextMock.Setup(m => m.Users.FindAsync(userId))
    .Returns(ValueTask.FromResult<User?>(existingUser));



        // Act
        var result = await userService.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingUser, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetUserByIdAsync_WhenUserExists_ReturnsUser: {ex.Message}");
            throw;
       
        }
    }

    [Fact]
    public async Task CreateUserAsync_WhenUserDoesNotExist_CreatesUser()
    {
    {
        try
        {
            
        // Arrange
       

        var userDTO = new UserDTO { Username = "testuser", Email = "test@example.com", Password = "password" };

        dbContextMock.Setup(m => m.Users.FirstOrDefaultAsync(u => u.Email == userDTO.Email, CancellationToken.None))
    .Returns(Task.FromResult<User?>(null));


        // Act
        var result = await userService.CreateUserAsync(userDTO);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User created successfully.", result.Message);
        Assert.Equal(201, result.StatusCode);


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CreateUserAsync_WhenUserDoesNotExist_CreatesUser: {ex.Message}");
            throw;
       
        }
    }
}

[Fact]

 public async Task UpdateUserAsync_WhenUserExists_UpdatesUser()
    {
    {
        try
        {
            
              // Arrange
      

        var userDTO = new UserDTO { Id = "1", Username = "updateduser", Email = "updated@example.com" };
        var existingUser = new User { Id = "1", Username = "olduser", Email = "old@example.com",  Password = "examplePassword"  };

        dbContextMock.Setup(m => m.Users.FindAsync(userDTO.Id))
    .Returns(ValueTask.FromResult<User?>(existingUser));



        // Act
        var result = await userService.UpdateUserAsync(userDTO);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User updated successfully.", result.Message);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(userDTO.Username, existingUser.Username);
        Assert.Equal(userDTO.Email, existingUser.Email);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateUserAsync_WhenUserExists_UpdatesUser: {ex.Message}");
            throw;
       
        }
    }
}
  

  [Fact]

 public async Task DeleteUserAsync_WhenUserExists_DeletesUser()
 
    {

        try
        {
            
             // Arrange
       
        var userId = "1";
        var existingUser = new User { Id = userId,  Password = "examplePassword"  };

        dbContextMock.Setup(m => m.Users.FindAsync(userId))
    .Returns(ValueTask.FromResult<User?>(existingUser));


        // Act
        var result = await userService.DeleteUserAsync(userId);

        // Assert
        Assert.True(result.Success);
        Assert.Equal("User deleted successfully.", result.Message);
        Assert.Equal(200, result.StatusCode);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in  DeleteUserAsync_WhenUserExists_DeletesUser: {ex.Message}");
            throw;
        }
       
    }

    [Fact]
 public async Task Authenticate_WhenValidUsernameAndPassword_ReturnsUser()
{
    try
    {
        // Arrange
        var username = "testuser";
        var password = "password";
        var existingUser = new User { Username = username, Password = passwordHasherMock.Object.HashPassword(password) };
        
        dbContextMock.Setup(m => m.Users.FirstOrDefaultAsync(u => u.Username == username, CancellationToken.None))
            .ReturnsAsync(existingUser);


        passwordHasherMock.Setup(m => m.VerifyPassword(existingUser.Password, password))
            .Returns(true);

        // Create a new instance of UserService using the mocked DbContext and PasswordHasher
        var userService = new UserService(dbContextMock.Object, passwordHasherMock.Object);

        // Act
        var result = await userService.Authenticate(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingUser, result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error in Authenticate_WhenValidUsernameAndPassword_ReturnsUser: {ex.Message}");
        throw;
    }
}




}