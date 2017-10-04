using System;
using System.Security.Cryptography;
using MirzaCryptoHelpers.Common;

namespace MirzaCryptoHelpers.SymmetricCryptos
{
    /// <summary>
    /// This class simplifies symmetric cryptography operations using DES.
    /// </summary>
    public sealed class DESCrypto:ISymmetricCrypto
    {
        private static readonly byte[] _iv = new byte[8] { 144, 121, 235, 22, 85, 91, 182, 197 };

        /// <summary>
        /// Valid IV size.
        /// </summary>
        public int ValidIVSize { get { return 8; } }

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
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, 8);
                    des.Key = passwordBytes;
                    des.IV = _iv;
                    using (ICryptoTransform crypto = des.CreateEncryptor())
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

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, 8);
                    des.Key = passwordBytes;
                    des.GenerateIV();
                    iv = des.IV;
                    using (ICryptoTransform crypto = des.CreateEncryptor())
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
                throw new CryptographicException("Invalid iv size. It must be 8 bytes.");

            byte[] result = null;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, 8);
                    des.Key = passwordBytes;
                    des.IV = iv;
                    using (ICryptoTransform crypto = des.CreateEncryptor())
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
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, 8);
                    des.Key = passwordBytes;
                    des.IV = _iv;
                    using (ICryptoTransform crypto = des.CreateDecryptor())
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
                throw new CryptographicException("Invalid iv size. It must be 8 bytes.");

            byte[] result = null;
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                try
                {
                    byte[] passwordBytes = BitHelpers.CreateSecurePassword(password, 8);
                    des.Key = passwordBytes;
                    des.IV = iv;
                    using (ICryptoTransform crypto = des.CreateDecryptor())
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
