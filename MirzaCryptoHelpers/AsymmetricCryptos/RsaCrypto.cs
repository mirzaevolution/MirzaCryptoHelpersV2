﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    /// <summary>
    /// This class performs Encryption and Decryption using RSA Algorithm.
    /// </summary>
    public sealed class RSACrypto : IAsymmetricCrypto
    {
        
        /// <summary>
        /// To encrypt/decrypt data, it must be called first to generate both keys
        /// and used to perform the operations.
        /// </summary>
        /// <param name="keySize">Rsa Legal Key Size (384-16384 with skipSize=8).</param>
        /// <returns>SessionsKeys. Returns null if GenerateKeys fails.</returns>
        /// <exception cref="CryptographicException">'keySize' must be in range of 384-16384 with skipSize=8</exception>
        public SessionKeys GenerateKeys(int keySize=4096)
        {
            if (!IsValidRsaKey(keySize))
                throw new CryptographicException("'keySize' is invalid for RSA. Valid range 384-16384 with skipSize=8.");

            SessionKeys sessionKeys = new SessionKeys();
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize)
                {
                    PersistKeyInCsp = false
                })
                {
                    
                    sessionKeys.PrivateKeyXml = rsa.ToXmlString(true);
                    sessionKeys.PublicKeyXml = rsa.ToXmlString(false);
                }
            }
            catch { sessionKeys = null; }
            return sessionKeys;
        }
        
        /// <summary>
        /// Encrypts the data using public key generated by GenerateKeys() method.
        /// </summary>
        /// <param name="data">Data to encrypt in bytes.</param>
        /// <param name="publicKeyXml">Public key in xml format.</param>
        /// <param name="keySize">Rsa Legal Key Size (384-16384 with skipSize=8).</param>
        /// <returns>Encrypted data.</returns>
        /// <exception cref="ArgumentNullException">'data' is null.</exception>
        /// <exception cref="ArgumentNullException">'publicKeyXml' is null/empty.</exception>
        /// <exception cref="CryptographicException">'keySize' must be in range of 384-16384 with skipSize=8</exception>
        public byte[] Encrypt(byte[] data, string publicKeyXml, int keySize= 4096)
        {
            if (data == null || data.Length==0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(publicKeyXml))
                throw new ArgumentNullException(nameof(publicKeyXml));
            if (!IsValidRsaKey(keySize))
                throw new CryptographicException("'keySize' is invalid for RSA. Valid range 384-16384 with skipSize=8.");
            byte[] cipher = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize)
                {
                    PersistKeyInCsp = false
                })
                {
                    rsa.FromXmlString(publicKeyXml);
                    cipher = rsa.Encrypt(data, true);

                }
            }
            catch { cipher = cipher != null ? null : cipher; }
            return cipher;
        }
        
        /// <summary>
        /// Decrypts the data using private key generated by GenerateKeys() method.
        /// PrivateKey must be generated along with PublicKey.
        /// </summary>
        /// <param name="data">Data to decrypt in bytes.</param>
        /// <param name="privateKeyXml">Private key in xml format.</param>
        /// <param name="keySize">Rsa Legal Key Size (384-16384 with skipSize=8).</param>
        /// <returns>Decrypted data.</returns>
        /// <exception cref="ArgumentNullException">'data' is null.</exception>
        /// <exception cref="ArgumentNullException">'privateKeyXml' is null/empty.</exception>
        /// <exception cref="CryptographicException">'keySize' must be in range of 384-16384 with skipSize=8</exception>
        public byte[] Decrypt(byte[] data, string privateKeyXml, int keySize=4096)
        {
            if(data == null || data.Length==0)
                throw new ArgumentNullException(nameof(data));
            if (String.IsNullOrEmpty(privateKeyXml))
                throw new ArgumentNullException(nameof(privateKeyXml));
            if (!IsValidRsaKey(keySize))
                throw new CryptographicException("'keySize' is invalid for RSA. Valid range 384-16384 with skipSize=8.");
            byte[] plain = null;
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize)
                {
                    PersistKeyInCsp = false
                })
                {
                    rsa.FromXmlString(privateKeyXml);
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
            if (keySize >= 384 && keySize <= 16384 && ((keySize%8)==0))
                return true;
            return false;
        }
    }
}
