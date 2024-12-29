namespace WiseReminder.Infrastructure.Encryption;

public sealed class EncryptService : IEncryptService
{
    public string Encrypt(string value)
    {
        return BCrypt.Net.BCrypt.HashPassword(value);
    }

    public bool Check(string hashedValue, string value)
    {
        return BCrypt.Net.BCrypt.Verify(value, hashedValue);
    }
}