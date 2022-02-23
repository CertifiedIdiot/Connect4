namespace Connect4.Crypto;

/// <summary>
/// DTO used for transfering encrypted network traffic.
/// </summary>
public class CryptoObj
{
    /// <summary>
    /// Gets or sets the encrypted byte[].
    /// </summary>
    /// <value>
    /// The encrypted message.
    /// </value>
    public byte[] Bytes { get; set; } = new byte[1];
}
