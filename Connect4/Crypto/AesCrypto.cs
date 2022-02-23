namespace Connect4.Crypto;

using System.Security.Cryptography;
//This code/class based on: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0

public class AesCrypto : ICrypto
{
    private byte[] _key = new byte[32];
    private byte[] _iv = new byte[16];
    private bool setupDone = false;

    public CryptoObj Init()
    {
        using (Aes aes = Aes.Create())
        {
            _key = aes.Key;
            _iv = aes.IV;
        }
        var output = _key.Concat(_iv).ToArray();
        setupDone = true;
        return new CryptoObj() { Bytes = output };
    }
    public void SetUp(CryptoObj setup)
    {
        _key = setup.Bytes[..32];
        _iv = setup.Bytes[32..];
        setupDone = true;
    }
    public CryptoObj Encrypt(string text)
    {
        if (!setupDone) throw new CryptographicException("Setup not done, can not complete encryption");
        if (text == null) throw new ArgumentNullException(nameof(text));

        byte[] encrypted;
        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(_key, _iv);

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(text);
                }
                encrypted = memoryStream.ToArray();
            }
            aes.Clear();
        }

        return new CryptoObj() { Bytes = encrypted };
    }

    public string Decrypt(CryptoObj chiper)
    {
        if (!setupDone) throw new CryptographicException("Setup not done, can not complete decryption");
        if (chiper == null) throw new ArgumentNullException(nameof(chiper));

        var output = string.Empty;

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(_key, _iv);
            using (var memoryStream = new MemoryStream(chiper.Bytes))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (var streamWriter = new StreamReader(cryptoStream))
            {
                output = streamWriter.ReadToEnd();
            }
            aes.Clear();
        }
        return output ?? "";
    }
}
