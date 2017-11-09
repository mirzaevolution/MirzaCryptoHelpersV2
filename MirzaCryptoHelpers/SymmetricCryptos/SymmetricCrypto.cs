using MirzaCryptoHelpers.Common;
using System;
namespace MirzaCryptoHelpers.SymmetricCryptos
{
    /// <summary>
    /// General Symmetric Cryptography class that can be used by any class which implements ISymmetricCrypto interface.
    /// This class can be used for dependency injection.
    /// </summary>
    public class SymmetricCrypto
    {
        private ISymmetricCrypto _symmetricCrypto;

        /// <summary>
        /// Main constructor that initializes encryption algorithm using AES
        /// </summary>
        public SymmetricCrypto()
        {
            _symmetricCrypto = new AESCrypto();
        }

        /// <summary>
        /// Secondary constructor that accepts class that implements ISymmetricCrypto interface.
        /// </summary>
        /// <param name="symmetricCrypto">Any class that implements ISymmetricCrypto interface. If null, an instance of AESCrypto will be used.</param>
        public SymmetricCrypto(ISymmetricCrypto symmetricCrypto)
        {
            _symmetricCrypto = symmetricCrypto;
            if (_symmetricCrypto == null)
                _symmetricCrypto = new AESCrypto();
        }

        /// <summary>
        /// Encrypts bytes of data with password using static IV.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <param name="password">Key/Password to encrypt.</param>
        /// <returns>Encrypted data in bytes. Returns null if encryption fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        public byte[] Encrypt(byte[] data, string password)
        {
            return _symmetricCrypto.Encrypt(data, password);
        }
        /// <summary>
        /// Decrypts bytes of data with password using static IV.
        /// </summary>
        /// <param name="data">Encrypted data.</param>
        /// <param name="password">Password/Key to decrypt.</param>
        /// <returns>Decrypted data in bytes. Returns null either decryption fails or password is incorrect.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        public byte[] Decrypt(byte[] data, string password)
        {
            return _symmetricCrypto.Decrypt(data, password);
        }

        /// <summary>
        /// Encrypts data in string format with password using static IV.
        /// It will return cipher text in Base64 string format.
        /// </summary>
        /// <param name="plainText">Plain text or data to encrypt.</param>
        /// <param name="password">Password to encrypt.</param>
        /// <returns>Cipher text in Base64 string format. Returns null either decryption fails or password is incorrect.</returns>
        /// <exception cref="ArgumentNullException">'plainText' is null/empty.</exception>
        /// <exception cref="ArgumentNullException">'password' is null/empty.</exception>
        public string Encrypt(string plainText, string password)
        {
            if (String.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(password);
            string base64CipherText = "";
            try
            {
                var cipherBytes = _symmetricCrypto.Encrypt(
                        BitHelpers.ConvertStringToBytes(plainText),
                        password
                    );
                base64CipherText = BitHelpers.ConvertToBase64String(cipherBytes);
            }
            catch { base64CipherText = ""; }
            return base64CipherText;
        }

        /// <summary>
        /// Decrypts data from Base64 string cipher text to plain text using static IV.
        /// NOTE: If cipher text is not in valid Base64 string format, InvalidOperationException will be thrown.
        /// </summary>
        /// <param name="base64CipherText">Cipher text in Base64 string format.</param>
        /// <param name="password">Password to decrypt data.</param>
        /// <returns>Plain text. Returns null either decryption fails or password is incorrect.</returns>
        public string Decrypt(string base64CipherText, string password)
        {
            if (String.IsNullOrEmpty(base64CipherText))
                throw new ArgumentNullException(nameof(base64CipherText));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            var cipherBytes = BitHelpers.ConvertFromBase64String(base64CipherText);
            if (cipherBytes == null)
                throw new InvalidOperationException("Cipher text is in invalid format. Make sure, cipher text is Base64 encoded string. " +
                    "By calling string Encrypt(string plainText, string password) you'll get cipher text in Base64 encoded string format.");
            string plainText = "";
            try
            {
               plainText = BitHelpers.ConvertBytesToString(_symmetricCrypto.Decrypt(cipherBytes, password));
            }
            catch { plainText = ""; }
            return plainText;
        }

    }
}
