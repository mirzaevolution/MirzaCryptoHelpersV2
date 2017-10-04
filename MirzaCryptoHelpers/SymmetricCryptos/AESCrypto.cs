using System;
using System.Security.Cryptography;
using MirzaCryptoHelpers.Common;
using MirzaCryptoHelpers.Hashings;
namespace MirzaCryptoHelpers.SymmetricCryptos
{
    /// <summary>
    /// This class simplifies symmetric cryptography operations using AES.
    /// </summary>
    public sealed class AESCrypto : ISymmetricCrypto
    {
        
        private static readonly byte[] _iv = new byte[16] { 255, 126, 242, 239, 122, 156, 180, 151, 176, 121, 145, 143, 152, 254, 125, 156 };

        /// <summary>
        /// Valid IV size.
        /// </summary>
        public int ValidIVSize { get { return 16; } }
        /// <summary>
        /// Encrypt bytes of data with password using static IV.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <param name="password">Key/Password to encrypt.</param>
        /// <returns>Encrypted data in bytes. Returns null if encryption fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        public byte[] Encrypt(byte[] data, string password)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            byte[] result = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, new SHA256Crypto());
                    aes.Key = passwordBytes;
                    aes.IV = _iv;
                    using (ICryptoTransform crypto = aes.CreateEncryptor())
                    {
                        result = crypto.TransformFinalBlock(data, 0, data.Length);
                    }
                }
                catch { result = null; }
            }
            
            return result;
            
        }
        /// <summary>
        /// Encrypt bytes of data with password using self-generated IV.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <param name="password">Key/Password to encrypt.</param>
        /// <param name="iv">Self-generated IV.</param>
        /// <returns>Encrypted data in bytes. Returns null if encryption fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        public byte[] Encrypt(byte[] data, string password, out byte[] iv)
        {
            iv = null;
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            byte[] result = null;

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, new SHA256Crypto());
                    aes.Key = passwordBytes;
                    aes.GenerateIV();
                    iv = aes.IV;
                    using (ICryptoTransform crypto = aes.CreateEncryptor())
                    {
                        result = crypto.TransformFinalBlock(data, 0, data.Length);
                    }
                }
                catch { result = null; }
            }
            return result;
        }
        /// <summary>
        /// Encrypt bytes of data with password using dynamic IV.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <param name="password">Key/Password to encrypt.</param>
        /// <param name="iv">Dynamic IV</param>
        /// <returns>Encrypted data in bytes. Returns null if encryption fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        /// <exception cref="CryptographicException">'iv' has invalid size.</exception>
        public byte[] Encrypt(byte[] data, string password, byte[] iv)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (!IsValidIVSize(iv))
                throw new CryptographicException("Invalid iv size. It must be 16 bytes.");

            byte[] result = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, new SHA256Crypto());
                    aes.Key = passwordBytes;
                    aes.IV = iv;
                    using (ICryptoTransform crypto = aes.CreateEncryptor())
                    {
                        result = crypto.TransformFinalBlock(data, 0, data.Length);
                    }
                }
                catch { result = null; }
            }

            return result;

        }


        /// <summary>
        /// Decrypt bytes of data with password using static IV.
        /// </summary>
        /// <param name="data">Encrypted bytes of data.</param>
        /// <param name="password">Password/Key to decrypt.</param>
        /// <returns>Decrypted data in bytes. Returns null either decryption fails or password is incorrect.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty.</exception>
        public byte[] Decrypt(byte[] data, string password)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            byte[] result = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, new SHA256Crypto());
                    aes.Key = passwordBytes;
                    aes.IV = _iv;
                    using (ICryptoTransform crypto = aes.CreateDecryptor())
                    {
                        result = crypto.TransformFinalBlock(data, 0, data.Length);
                    }
                }
                catch { result = null; }
            }

            return result;
        }
        
        /// <summary>
        /// Decrypt bytes of data with password using dynamic IV.
        /// </summary>
        /// <param name="data">Encrypted bytes of data.</param>
        /// <param name="password">Password/Key to decrypt.</param>
        /// <param name="iv">Dynamic IV.</param>
        /// <returns>Decrypted data in bytes. Returns null either decryption fails or password is incorrect.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null.</exception>
        /// <exception cref="ArgumentNullException">'password' cannot be null/empty</exception>
        /// <exception cref="CryptographicException">'iv' has invalid size.</exception>
        public byte[] Decrypt(byte[] data, string password, byte[] iv)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (!IsValidIVSize(iv))
                throw new CryptographicException("Invalid iv size. It must be 16 bytes.");

            byte[] result = null;
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, new SHA256Crypto());
                    aes.Key = passwordBytes;
                    aes.IV = iv;
                    using (ICryptoTransform crypto = aes.CreateDecryptor())
                    {
                        result = crypto.TransformFinalBlock(data, 0, data.Length);
                    }
                }
                catch { result = null; }
            }

            return result;
        }
        private bool IsValidIVSize(byte[] iv)
        {
            if (iv.Length != ValidIVSize)
                return false;
            return true;
        }
    }
}
