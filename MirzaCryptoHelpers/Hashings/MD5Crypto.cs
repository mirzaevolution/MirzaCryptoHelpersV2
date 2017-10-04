using System;
using System.Security.Cryptography;
using MirzaCryptoHelpers.Common;
namespace MirzaCryptoHelpers.Hashings
{
    /// <summary>
    /// This class is used to hash data with MD5 Algorithm.
    /// </summary>
    public sealed class MD5Crypto : IHash
    {
        /// <summary>
        /// Size of the current hash algorithm.
        /// </summary>
        public int HashSize
        { get => 128; }

        /// <summary>
        /// Hash string data to hash bytes.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in bytes. Returns empty if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public byte[] GetHashBytes(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            return GetHashBytes(BitHelpers.ConvertStringToBytes(input));
        }
        /// <summary>
        /// Hash bytes data to hash bytes.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in bytes. Returns empty if fails.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        public byte[] GetHashBytes(byte[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentNullException(nameof(input));
            byte[] result = null;
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                try
                {
                    result = md5.ComputeHash(input);
                }
                catch { result = null; }
            }
            return result;
        }
        /// <summary>
        /// Hash data and convert it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hash data in Base64 encoded string format.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(string input)
        {
            return BitHelpers.ConvertToBase64String(GetHashBytes(input));
        }
        /// <summary>
        /// Hash data and convert it to Base64 encoded string.
        /// </summary>
        /// <param name="input">Input as bytes.</param>
        /// <returns>Hash data in Base64 encoded string format.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public string GetHashBase64String(byte[] input)
        {
            return BitHelpers.ConvertToBase64String(GetHashBytes(input));
        }

    }
}
