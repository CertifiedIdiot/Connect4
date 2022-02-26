namespace Connect4.Tests
{
    using Connect4.Crypto;
    using Xunit;
    using System;
    using System.Security.Cryptography;
    using Moq;
    using Connect4.Interfaces;

    public class AesRsaCryptoTests
    {
        readonly AesRsaCrypto sut;
        readonly INetwork moqNet;
        public AesRsaCryptoTests()
        {
            var mockINetwork = new Mock<INetwork>();
            mockINetwork.Setup(x => x.Receive()).Returns(JsonHandler.Serialize(new RSACryptoServiceProvider(4096).ExportParameters(false)));
            moqNet = mockINetwork.Object;
            sut = new AesRsaCrypto();
            sut.Init(moqNet);
        }

        [Fact]
        public void EncryptDecrypt_Roundtrip_ShouldWork()
        {
            const string expected = "The quick brown fox jumps over the lazy dog";

            var encrypted = sut.Encrypt(expected);
            var actual = sut.Decrypt(encrypted);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Encrypt_NullInput_ShouldThrowArgumentNullExeption() => Assert.Throws<ArgumentNullException>(() => sut.Encrypt(null!));

        [Fact]
        public void Encrypt_SutNotInitialized_ShouldThrowCryptographicException()
        {
            var sutNoInit = new AesRsaCrypto();

            Assert.Throws<CryptographicException>(() => sutNoInit.Encrypt("hello world"));
        }
        [Fact]
        public void Decrypt_NullInput_ShouldThrowArgumentNullExeption() => Assert.Throws<ArgumentNullException>(() => sut.Decrypt(null!));

        [Fact]
        public void Decrypt_SutNotInitialized_ShouldThrowCryptographicException()
        {
            var sutNoInit = new AesRsaCrypto();

            Assert.Throws<CryptographicException>(() => sutNoInit.Decrypt(new CryptoObj()));
        }
    }
}
