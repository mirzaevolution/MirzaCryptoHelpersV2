﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    /// <summary>
    /// This is common class for Asymmetric Cryptography operations. 
    /// You can use this class to perform dependency injection.
    /// </summary>
    public class AsymmetricCrypto
    {
        
        private IAsymmetricCrypto _asymmetricCryptography;

        /// <summary>
        /// Default constructor assigns RsaCrypto class to perform Asymmetric Cryptography operations. 
        /// </summary>
        public AsymmetricCrypto()
        {
            _asymmetricCryptography = new RSACrypto();
        }


        /// <summary>
        /// Injectable constructor that can use your own IAsymmetricCryptography implementation.
        /// </summary>
        /// <param name="asymmetricCryptography">Class that implements IAsymmetricCryptography.</param>
        public AsymmetricCrypto(IAsymmetricCrypto asymmetricCryptography)
        {
            _asymmetricCryptography = asymmetricCryptography;
            if(_asymmetricCryptography == null)
            {
                _asymmetricCryptography = new RSACrypto();
            }
        }


        /// <summary>
        /// To encrypt/decrypt data, it must be called first to generate both keys
        /// and used to perform the operations.
        /// </summary>
        /// <returns>SessionsKeys. Returns null if GenerateKeys fails.</returns>
        /// <exception cref="CryptographicException">'keySize' must be in valid range.</exception>
        public SessionKeys GenerateKeys(int keySize)
        {
            return _asymmetricCryptography.GenerateKeys(keySize);
        }


        /// <summary>
        /// Encrypts the data using public key generated by GenerateKeys() method.
        /// </summary>
        /// <param name="data">Data to encrypt in bytes.</param>
        /// <param name="publicKeyXml">Public key in xml format.</param>
        /// <param name="keySize">Key</param>
        /// <returns>Encrypted data in bytes.</returns>
        /// <exception cref="ArgumentNullException">'data' is null</exception>
        /// <exception cref="ArgumentNullException">'publicKeyXml' is null/empty.</exception>
        /// <exception cref="CryptographicException">'keySize' must be in valid range.</exception>
        public byte[] Encrypt(byte[] data, string publicKeyXml, int keySize)
        {
            return _asymmetricCryptography.Encrypt(data, publicKeyXml, keySize);
        }


        /// <summary>
        /// Decrypts the data using private key generated by GenerateKeys() method.
        /// PrivateKey must be generated along with PublicKey.
        /// </summary>
        /// <param name="data">Data to decrypt in bytes.</param>
        /// <param name="privateKeyXml">Private key in xml format.</param>
        /// <param name="keySize">Key</param>
        /// <returns>Decrypted data in xml format.</returns>
        /// <exception cref="ArgumentNullException">'data' is null</exception>
        /// <exception cref="ArgumentNullException">'privateKeyXml' is null/empty.</exception>
        /// <exception cref="CryptographicException">'keySize' must be in valid range.</exception>
        public byte[] Decrypt(Byte[] data, string privateKeyXml, int keySize)
        {
            return _asymmetricCryptography.Decrypt(data, privateKeyXml, keySize);
        }
    }
}
