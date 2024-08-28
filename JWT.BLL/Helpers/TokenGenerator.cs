using System.Security.Cryptography;

namespace JWT.BLL.Helpers;

public static class TokenGenerator
{
    public static string GenerateToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}