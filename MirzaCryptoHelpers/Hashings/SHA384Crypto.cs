using System;
using System.Security.Cryptography;
using MirzaCryptoHelpers.Common;
namespace MirzaCryptoHelpers.Hashings
{
    /// <summary>
    /// This class is used to hash data with SHA384 Algorithm.
    /// </summary>
    public sealed class SHA384Crypto : IHashCrypto
    {

        /// <summary>
        /// Gets size of the current hash algorithm.
        /// </summary>
        public int HashSize
        { get => 384; }


        /// <summary>
        /// Hash string data to hash bytes.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in bytes. Returns null if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public byte[] GetHashBytes(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            var result = BitHelpers.ConvertStringToBytes(input);
            if (result == null)
                return null;
            return GetHashBytes(result);
        }
        /// <summary>
        /// Hashes bytes data to hash bytes.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in bytes. Returns null if fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        public byte[] GetHashBytes(byte[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentNullException(nameof(input));
            byte[] result = null;
            using (SHA384CryptoServiceProvider sha384 = new SHA384CryptoServiceProvider())
            {
                try
                {
                    result = sha384.ComputeHash(input);
                }
                catch { result = null; }
            }
            return result;
        }
        /// <summary>
        /// Hashes data and converts it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in Base64 encoded string format. Returns null if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(string input)
        {
            var result = GetHashBytes(input);
            if (result == null)
                return null;
            return BitHelpers.ConvertToBase64String(result);
        }
        /// <summary>
        /// Hashes data and converts it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in Base64 encoded string format. Returns null if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(byte[] input)
        {
            var result = GetHashBytes(input);
            if (result == null)
                return null;
            return BitHelpers.ConvertToBase64String(result);
        }

    }
}
