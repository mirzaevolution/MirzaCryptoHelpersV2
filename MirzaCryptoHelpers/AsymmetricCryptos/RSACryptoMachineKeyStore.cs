using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    /// <summary>
    /// Encrypts/Decrypts data using CSP with Microsoft Strong Cryptographic Provider.
    /// </summary>
    public class RSACryptoMachineKeyStore
    {
        private const int PROVIDER_RSA_INT = 1;
        private const string PROVIDER_RSA_NAME = "Microsoft Strong Cryptographic Provider";
        private const string CONTAINER_NAME = "{AF9945DE-BE93-4938-8408-366ACA886983}";

        /// <summary>
        /// Clears keys from container.
        /// </summary>
        public void ClearKeysFromContainer()
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters(PROVIDER_RSA_INT, PROVIDER_RSA_NAME, CONTAINER_NAME)))
                {
                    rsa.PersistKeyInCsp = false;
                    rsa.Clear();
                }
            }
            catch { }
        }
        /// <summary>
        /// Encrypts data using Microsoft Strong Cryptographic Provider and stores keys to container.
        /// It will store keys to computer's key store. It Uses UseMachineKeyStore flag for CspParameters.
        /// </summary>
        /// <param name="data">Data to encrypt in bytes.</param>
        /// <param name="keySize">Rsa Legal Key Size (384-16384 with skipSize=8).</param>
        /// <returns>Encrypted data.</returns>
        /// <exception cref="ArgumentNullException">'data' is null</exception>
        /// <exception cref="CryptographicException">'keySize' must be in range of 384-16384 with skipSize=8</exception>
        public byte[] Encrypt(byte[] data, int keySize=4096)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (!IsValidRsaKey(keySize))
                throw new CryptographicException("'keySize' is invalid for RSA. Valid range 384-16384 with skipSize=8.");
            byte[] cipher = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters(PROVIDER_RSA_INT, PROVIDER_RSA_NAME, CONTAINER_NAME))
                {
                    KeySize = keySize,
                    PersistKeyInCsp = true
                })
                {
                    cipher = rsa.Encrypt(data, true);
                }
            }
            catch { cipher = cipher != null ? null : cipher; }
            return cipher;
        }
        /// <summary>
        /// Decrypts data using Microsoft Strong Cryptographic Provider and stores keys to container.
        /// It will store keys to computer's key store. It Uses UseMachineKeyStore flag for CspParameters.
        /// </summary>
        /// <param name="data">Data to encrypt in bytes.</param>
        /// <param name="keySize">Rsa Legal Key Size (384-16384 with skipSize=8).</param>
        /// <returns>Decrypted data.</returns>
        /// <exception cref="ArgumentNullException">'data' is null</exception>
        /// <exception cref="CryptographicException">'keySize' must be in range of 384-16384 with skipSize=8</exception>
        public byte[] Decrypt(byte[] data, int keySize=4096)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            if (!IsValidRsaKey(keySize))
                throw new CryptographicException("'keySize' is invalid for RSA. Valid range 384-16384 with skipSize=8.");
            byte[] plain = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(new CspParameters(PROVIDER_RSA_INT, PROVIDER_RSA_NAME, CONTAINER_NAME))
                {
                    KeySize = keySize,
                    PersistKeyInCsp = true
                })
                {
                    plain = rsa.Decrypt(data, true);
                }
            }
            catch { plain = plain != null ? null : plain; }
            return plain;
        }



        /// <summary>
        /// Gets valid RSA key sizes.
        /// </summary>
        /// <returns>Valid RSA key sizes in array of integer type.</returns>
        public int[] GetValidKeySizes()
        {
            List<int> keys = new List<int>();
            for (int i = 384; i <= 16384; i++)
            {
                if (i % 8 == 0)
                {
                    keys.Add(i);
                }
            }
            return keys.ToArray();
        }
        private bool IsValidRsaKey(int keySize)
        {
            if (keySize >= 384 && keySize <= 16384 && ((keySize % 8) == 0))
                return true;
            return false;
        }
    }
}
