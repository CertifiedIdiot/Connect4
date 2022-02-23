namespace Connect4.Crypto
{
    public interface ICrypto
    {
        string Decrypt(CryptoObj chiper);
        CryptoObj Encrypt(string text);
        CryptoObj Init();
        void SetUp(CryptoObj setup);
    }
}