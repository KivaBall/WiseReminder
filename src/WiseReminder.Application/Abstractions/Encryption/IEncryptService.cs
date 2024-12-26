namespace WiseReminder.Application.Abstractions.Encryption;

public interface IEncryptService
{
    string Encrypt(string value);
    bool Check(string hashedValue, string value);
}