namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    /// <summary>
    /// This class holds public and private keys in xml
    /// to perform asymmetric encryption.
    /// </summary>
    public class SessionKeys
    {
        /// <summary>
        /// Gets/Sets public key to encrypt the data.
        /// </summary>
        public string PublicKeyXml { get; set; }
        /// <summary>
        /// Gets/Sets private key to decrypt the data.
        /// </summary>
        public string PrivateKeyXml { get; set; }
    }
}
