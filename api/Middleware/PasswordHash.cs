using System;
using System;
using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return hashedPassword == HashPassword(password);
    }
}
