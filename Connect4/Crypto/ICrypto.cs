namespace Connect4.Crypto
{
    public interface ICrypto
    {
        /// <summary>
        /// Decrypts the incoming <see cref="CryptoObj"/>.
        /// </summary>
        /// /// <remarks>Make sure to setup through either <see cref="Init"/> or <see cref="SetUp(CryptoObj)"/> methods before trying to decrypt.</remarks>
        /// <param name="encrypted">The incoming <see cref="CryptoObj"/> to be decrypted.</param>
        /// <returns>The decrypted <see cref="string"/>.</returns>
        string Decrypt(CryptoObj encrypted);
        /// <summary>
        /// Encrypts the incoming <see cref="string"/>.
        /// </summary>
        /// <remarks>Make sure to setup through either <see cref="Init"/> or <see cref="SetUp(CryptoObj)"/> methods before trying to encrypt.</remarks>
        /// <param name="text">The <see cref="string"/> to be encrypted.</param>
        /// <returns>A <see cref="CryptoObj"/> containing the encrypted <see cref="byte"/> array.</returns>
        CryptoObj Encrypt(string text);
        /// <summary>
        /// Initializes this instance with key and initialization vector.
        /// </summary>
        /// <remarks>This is the method to use if this instance is the one that initializes the crypto session.</remarks>
        /// <returns><see cref="CryptoObj"/> containing the key and iv for distribution if needed.</returns>
        CryptoObj Init();
        /// <summary>
        /// Sets up with key and IV values received in the passed in <see cref="CryptoObj"/> object.
        /// </summary>
        /// <remarks>This is the method to use if this instance is not the one that initializes the crypto session, but rather receives the needed data from somewhere else.</remarks>
        /// <param name="setup">Object containing the values needed to set up this instance for encryption/decryption.</param>
        void SetUp(CryptoObj setup);
    }
}