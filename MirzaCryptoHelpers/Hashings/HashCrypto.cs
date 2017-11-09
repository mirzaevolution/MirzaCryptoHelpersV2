using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirzaCryptoHelpers.Hashings
{
    /// <summary>
    /// General Hash class that can be used to perform dependency injection.
    /// </summary>
    public class HashCrypto
    {

        private IHashCrypto _hash;

        /// <summary>
        /// Main constructor. If no parameter passed, SHA256Crypto will be used.
        /// </summary>
        public HashCrypto()
        {
            _hash = new SHA256Crypto();
        }
        /// <summary>
        /// Injectable constructor. Used commonly for dependency injection.
        /// </summary>
        /// <param name="hash">Concreate implementation of IHash interface.</param>
        public HashCrypto(IHashCrypto hash)
        {
            _hash = hash;
            if (_hash == null)
                _hash = new SHA256Crypto();
        }

        /// <summary>
        /// Key size
        /// </summary>
        public int HashSize => _hash.HashSize;

        /// <summary>
        /// Hashes data and converts it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in Base64 encoded string format.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(string input)
        {
            return _hash.GetHashBase64String(input);
        }
        /// <summary>
        /// Hashes data and converts it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in Base64 encoded string format.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(byte[] input)
        {
            return _hash.GetHashBase64String(input);
        }
        /// <summary>
        /// Hashes string data to hash bytes.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in bytes. Returns empty if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public byte[] GetHashBytes(string input)
        {
            return _hash.GetHashBytes(input);
        }
        /// <summary>
        /// Hashes bytes data to hash bytes.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in bytes. Returns empty if fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        public byte[] GetHashBytes(byte[] input)
        {
            return _hash.GetHashBytes(input);
        }
    }
}
