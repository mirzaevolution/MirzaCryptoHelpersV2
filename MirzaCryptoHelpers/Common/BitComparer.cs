using System;
using MirzaCryptoHelpers.Hashings;
namespace MirzaCryptoHelpers.Common
{
    /// <summary>
    /// Class that performs comparison between 2 data.
    /// </summary>
    public class BitComparer
    {
        /// <summary>
        /// Compares 2 data in bytes and returns true if both are same, otherwise false.
        /// </summary>
        /// <param name="data1">Data 1.</param>
        /// <param name="data2">Data 2.</param>
        /// <returns>Returs true if both are same, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">First data cannot be null.</exception>
        /// <exception cref="ArgumentNullException">Second data cannot be null.</exception>
        public static bool CompareBytes(byte[] data1, byte[] data2)
        {
            if (data1 == null)
                throw new ArgumentNullException(nameof(data1), "First data cannot be null.");
            if (data2 == null)
                throw new ArgumentNullException(nameof(data2), "Second data cannot be null.");
            if (data1.Length != data2.Length)
                return false;
            for(int i=0;i<data1.Length;i++)
            {
                if (data1[i] != data2[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Compares 2 data using SHA-256 algorithm.
        /// </summary>
        /// <param name="data1">Data 1.</param>
        /// <param name="data2">Data 2.</param>
        /// <returns>Returs true if both are same, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">First data cannot be null.</exception>
        /// <exception cref="ArgumentNullException">Second data cannot be null.</exception>
        public static bool CompareHashes(byte[] data1, byte[] data2)
        {
            if (data1 == null)
                throw new ArgumentNullException(nameof(data1), "First data cannot be null.");
            if (data2 == null)
                throw new ArgumentNullException(nameof(data2), "Second data cannot be null.");
            if (data1.Length != data2.Length)
                return false;
            byte[] hashedData1 = new SHA256Crypto().GetHashBytes(data1);
            byte[] hashedData2 = new SHA256Crypto().GetHashBytes(data2);
            if (hashedData1 == null || hashedData2 == null)
                return false;
            for(int i=0;i<hashedData1.Length;i++)
            {
                if (hashedData1[i] != hashedData2[i])
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Compares 2 data using algorithm that implements IHashCrypto interface.
        /// </summary>
        /// <param name="data1">Data 1.</param>
        /// <param name="data2">Data 2.</param>
        /// <param name="hash">Hash algorithm that implements IHashCrypto interface.</param>
        /// <returns>Returs true if both are same, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">First data cannot be null.</exception>
        /// <exception cref="ArgumentNullException">Second data cannot be null.</exception>
        /// <exception cref="ArgumentNullException">Hash algorithm cannot be null.</exception>
        public static bool CompareHashes(byte[] data1, byte[] data2, IHashCrypto hash)
        {
            if (data1 == null)
                throw new ArgumentNullException(nameof(data1), "First data cannot be null.");
            if (data2 == null)
                throw new ArgumentNullException(nameof(data2), "Second data cannot be null.");
            if (hash == null)
                throw new ArgumentNullException(nameof(hash), "Hash algorithm cannot be null.");
            if (data1.Length != data2.Length)
                return false;
            byte[] hashedData1 = null;
            byte[] hashedData2 = null;
            try
            {
                hashedData1 = hash.GetHashBytes(data1);
            }
            catch { hashedData1 = hashedData1 != null ? null : hashedData1; }
            try
            {
                hashedData2 = hash.GetHashBytes(data2);
            }
            catch { hashedData2 = hashedData2 != null ? null : hashedData2; }

            if (hashedData1 == null || hashedData2 == null)
                return false;
            for (int i = 0; i < hashedData1.Length; i++)
            {
                if (hashedData1[i] != hashedData2[i])
                    return false;
            }
            return true;
        }

    }
}
