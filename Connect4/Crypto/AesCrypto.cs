namespace Connect4.Crypto;

using Connect4.Interfaces;
using System.Security.Cryptography;
//This code/class based on: https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-6.0

/// <summary>
/// Class for AES ancryption of strings.
/// </summary>
/// <seealso cref="Connect4.Crypto.ICrypto" />
public class AesCrypto : ICrypto
{
    private byte[] _key = new byte[32];
    private byte[] _iv = new byte[16];
    private bool setupDone = false;

    /// <summary>
    /// Initializes this instance of <see cref="AesCrypto"/> with key and initialization vector.
    /// </summary>
    /// <remarks>This is the method to use if this instance of <see cref="AesCrypto"/> is the one that initializes the crypto session.</remarks>
    /// <param name="network">The network used for initial exchange of keys.</param>
    public void Init(INetwork network)
    {
        using (Aes aes = Aes.Create())
        {
            _key = aes.Key;
            _iv = aes.IV;
            aes.Clear();
        }
        var output = _key.Concat(_iv).ToArray();
        setupDone = true;
        var json = JsonHandler.Serialize(new CryptoObj() { Bytes = output });
        Thread.Sleep(500);
        network.Send(json);
    }

    /// <summary>
    /// Sets up this instance of <see cref="AesCrypto"/> with key and IV values received in the passed in <see cref="CryptoObj"/> object.
    /// </summary>
    /// <remarks>This is the method to use if this instance of <see cref="AesCrypto"/> is not the one that initializes the crypto session, but rather receives the needed data from somewhere else.</remarks>
    /// <param name="network">The network used for initial exchange of keys.</param>
    public void SetUp(INetwork network)
    {
        var json = network.Receive();
        var setup = JsonHandler.Deserialize<CryptoObj>(json);
        _key = setup.Bytes[..32];
        _iv = setup.Bytes[32..];
        setupDone = true;
    }

    /// <summary>
    /// Encrypts the incoming <see cref="string"/>.
    /// </summary>
    /// <remarks>Make sure that this <see cref="AesCrypto"/> is setup through either <see cref="Init"/> or <see cref="SetUp(CryptoObj)"/> methods before trying to encrypt.</remarks>
    /// <param name="text">The <see cref="string"/> to be encrypted.</param>
    /// <returns>A <see cref="CryptoObj"/> containing the encrypted <see cref="byte"/> array.</returns>
    /// <exception cref="CryptographicException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public CryptoObj Encrypt(string text)
    {
        if (!setupDone) throw new CryptographicException("Setup not done, can not complete encryption.");
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

    /// <summary>
    /// Decrypts the incoming <see cref="CryptoObj"/>.
    /// </summary>
    /// /// <remarks>Make sure that this <see cref="AesCrypto"/> is setup through either <see cref="Init"/> or <see cref="SetUp(CryptoObj)"/> methods before trying to decrypt.</remarks>
    /// <param name="encrypted">The incoming <see cref="CryptoObj"/> to be decrypted.</param>
    /// <returns>The decrypted <see cref="string"/>.</returns>
    /// <exception cref="CryptographicException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public string Decrypt(CryptoObj encrypted)
    {
        if (!setupDone) throw new CryptographicException("Setup not done, can not complete decryption.");
        if (encrypted == null) throw new ArgumentNullException(nameof(encrypted));

        var output = string.Empty;

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(_key, _iv);
            using (var memoryStream = new MemoryStream(encrypted.Bytes))
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
