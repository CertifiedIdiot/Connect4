namespace Connect4.Crypto
{
    using Connect4.Interfaces;

    public interface ICrypto
    {
        /// <summary>
        /// Decrypts the incoming <see cref="CryptoObj"/>.
        /// </summary>
        /// /// <remarks>Make sure to setup through either <see cref="Init"/> or <see cref="SetUp(INetwork)"/> methods before trying to decrypt.</remarks>
        /// <param name="encrypted">The incoming <see cref="CryptoObj"/> to be decrypted.</param>
        /// <returns>The decrypted <see cref="string"/>.</returns>
        string Decrypt(CryptoObj encrypted);
        /// <summary>
        /// Encrypts the incoming <see cref="string"/>.
        /// </summary>
        /// <remarks>Make sure to setup through either <see cref="Init"/> or <see cref="SetUp(INetwork)"/> methods before trying to encrypt.</remarks>
        /// <param name="text">The <see cref="string"/> to be encrypted.</param>
        /// <returns>A <see cref="CryptoObj"/> containing the encrypted <see cref="byte"/> array.</returns>
        CryptoObj Encrypt(string text);
        /// <summary>
        /// Initializes this instance with key and initialization vector.
        /// </summary>
        /// <remarks>This is the method to use if this instance is the one that initializes the crypto session.</remarks>
        /// <param name="network">The network used for initial exchange of keys.</param>
        void Init(INetwork network);
        /// <summary>
        /// Sets up with key and IV values received through the <see cref="INetwork"/>.
        /// </summary>
        /// <remarks>This is the method to use if this instance is not the one that initializes the crypto session, but rather receives the needed data from somewhere else.</remarks>
        /// <param name="network">The network used for initial exchange of keys.</param>
        void SetUp(INetwork network);
    }
}