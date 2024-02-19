using System.Security.Cryptography;

namespace FitnessAI.Infrastructure.Encryption;

public class KeyInfo
{
    public KeyInfo()
    {
        using (var myAes = Aes.Create())
        {
            Key = myAes.Key;
            Iv = myAes.IV;
        }
    }

    public KeyInfo(string key, string iv)
    {
        Key = Convert.FromBase64String(key);
        Iv = Convert.FromBase64String(iv);
    }

    public byte[] Key { get; }
    public byte[] Iv { get; }

    public string KeyString => Convert.ToBase64String(Key);
    public string IvString => Convert.ToBase64String(Iv);
}