namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    /// <summary>
    /// This class holds public and private keys in xml
    /// to perform asymmetric encryption.
    /// </summary>
    public class SessionKeys
    {
        /// <summary>
        /// Public key to encrypt the data.
        /// </summary>
        public string PublicKeyXml { get; set; }
        /// <summary>
        /// Private key to decrypt the data.
        /// </summary>
        public string PrivateKeyXml { get; set; }
    }
}
