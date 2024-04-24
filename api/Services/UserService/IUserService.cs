using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Identity;

public interface IUserService
{
      Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<ServiceResponse<bool>> CreateUserAsync(UserDTO userDTO);
    Task<ServiceResponse<bool>> UpdateUserAsync(UserDTO userDTO);
    Task<ServiceResponse<bool>>  DeleteUserAsync(string id);
    Task<User?> Authenticate(string username, string password);
}

public class UserService : IUserService
{
   
    private readonly LibraryDbContext _dbContext;
    private readonly PasswordHasher _passwordHasher;

    public UserService(LibraryDbContext dbContext, PasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    

     public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }


public async Task<User?> GetUserByUsername(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    // public async Task<bool> CreateUserAsync(UserDTO userDTO)
    // {
    //     var user = new User
    //     {
    //         Username = userDTO.Username,
    //         Email = userDTO.Email
    //     };

    //     var result = await _userManager.CreateAsync(user, userDTO.Password);
    //     return result.Succeeded;
    // }

    public async Task<ServiceResponse<bool>> CreateUserAsync(UserDTO userDTO)
    {
        try
        {

            if (userDTO.Email == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Email cannot be null.",
                StatusCode = 400
            };
        }
            var existingUser = await GetUserByEmailAsync(userDTO.Email);
            if (existingUser != null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "A user with this email already exists. Please choose another email.",
                    StatusCode = 400
                };
            }


             if (string.IsNullOrEmpty(userDTO.Username))
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Username is required.",
                StatusCode = 400
            };
        }

        if (userDTO.Password == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Password cannot be null.",
                StatusCode = 400
            };
        }

            var user = new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = _passwordHasher.HashPassword(userDTO.Password)
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "User created successfully.",
                StatusCode = 201
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating user: {ex.Message}");
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Failed to create user.",
                StatusCode = 500
            };
        }
    }


   public async Task<ServiceResponse<bool>> UpdateUserAsync(UserDTO userDTO)
{
    try
    {
        var user = await _dbContext.Users.FindAsync(userDTO.Id);

        if (user == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not found.",
                StatusCode = 404
            };
        }

         user.Username = userDTO.Username ?? user.Username;
        user.Email = userDTO.Email ?? user.Email;

        await _dbContext.SaveChangesAsync();
        return new ServiceResponse<bool>
        {
            Data = true,
            Message = "User updated successfully.",
            StatusCode = 200
        };
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating user: {ex.Message}");
        return new ServiceResponse<bool>
        {
            Success = false,
            Message = "Failed to update user.",
            StatusCode = 500
        };
    }
}

    public async Task<ServiceResponse<bool>> DeleteUserAsync(string id)
{
    try
    {
        var user = await _dbContext.Users.FindAsync(id);

        if (user == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "User not found.",
                StatusCode = 404
            };
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

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



     public async Task<User?> Authenticate(string username, string password)
    {

        
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        if (user != null && _passwordHasher.VerifyPassword(user.Password, password))
        {
            return user;
        }
        return null;
    }

}