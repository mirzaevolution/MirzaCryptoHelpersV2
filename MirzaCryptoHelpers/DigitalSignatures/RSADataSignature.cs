using System;
using System.Security.Cryptography;

namespace MirzaCryptoHelpers.DigitalSignatures
{
    /// <summary>
    /// Hash algorithm to use for digital signature operations.
    /// </summary>
    public enum HashAlgorithm
    {
        MD5,
        SHA1,
        SHA256,
        SHA384,
        SHA512
    }
    /// <summary>
    /// Helper class to perform signing and verifying data using RSA Crypto Algorithm.
    /// </summary>
    public static class RSADataSignature
    {
        /// <summary>
        /// Create digital signature.
        /// </summary>
        /// <param name="dataToSign">Original data to signed.</param>
        /// <param name="hashAlgorithm">Hash algorithm to use.</param>
        /// <param name="privateKeyXml">Private key in xml string to sign the data.</param>
        /// <returns>Signed data in bytes. Returns null if fails.</returns>
        /// <exception cref="ArgumentNullException">'dataToSign' is null/empty.</exception>
        /// <exception cref="ArgumentNullException">'privateKeyXml' is null/empty.</exception>
        /// <example>
        ///     To Get RSA Keys, 
        ///     <code>
        ///     using MirzaCryptoHelpers.AsymmetricCryptos;
        ///     SessionKeys sessionKeys = new RSACrypto().GenerateKeys();
        ///     </code>
        /// </example>
        public static byte[] SignData(byte[] dataToSign, HashAlgorithm hashAlgorithm, string privateKeyXml)
        {
            if (dataToSign == null || dataToSign.Length == 0)
                throw new ArgumentNullException(nameof(dataToSign));
            if (String.IsNullOrEmpty(privateKeyXml))
                throw new ArgumentNullException(nameof(privateKeyXml));
            byte[] signedData = null;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    
                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(privateKeyXml);
                    signedData = rsa.SignData(dataToSign, hashAlgorithm.ToString());
                }
                catch { signedData = null; }
            }
            return signedData;
        }   /// <summary>
            /// Verify digital signature.
            /// </summary>
            /// <param name="originalData">Original data.</param>
            /// <param name="signedData">Signed data.</param>
            /// <param name="hashAlgorithm">Hash algorithm to use.</param>
            /// <param name="publicKeyXml">Public key in xml format to verify the data.</param>
            /// <returns>Boolean value indicating wheter data is valid or not.</returns>
            /// <exception cref="ArgumentNullException">'originalData' is null/empty.</exception>
            /// <exception cref="ArgumentNullException">'signedData' is null/empty.</exception>
            /// <exception cref="ArgumentNullException">'publicKeyXml' is null/empty.</exception>
            /// <example>
            ///     To Get RSA Keys, 
            ///     <code>
            ///     using MirzaCryptoHelpers.AsymmetricCryptos;
            ///     SessionKeys sessionKeys = new RSACrypto().GenerateKeys();
            ///     </code>
            /// </example>
        public static bool VerifyData(byte[] originalData, byte[] signedData, HashAlgorithm hashAlgorithm, string publicKeyXml)
        {
            
            if (originalData == null || originalData.Length == 0)
                throw new ArgumentNullException(nameof(originalData));
            if (signedData == null || signedData.Length == 0)
                throw new ArgumentNullException(nameof(signedData));
            if (String.IsNullOrEmpty(publicKeyXml))
                throw new ArgumentNullException(publicKeyXml);
            bool verified = false;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) 
            {
                try
                {

                    rsa.PersistKeyInCsp = false;
                    rsa.FromXmlString(publicKeyXml);
                    verified =  rsa.VerifyData(originalData, hashAlgorithm.ToString(), signedData);
                }
                catch { verified = false; }
            }
            return verified;
        }
    }
    
}
