namespace WiseReminder.Application.Abstractions.Encryption;

public interface IEncryptService
{
    string Encrypt(string value);
    string Check(string encryptedValue, string value);
}