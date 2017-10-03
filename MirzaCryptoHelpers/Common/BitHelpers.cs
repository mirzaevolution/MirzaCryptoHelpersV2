using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MirzaCryptoHelpers.Common
{
    /// <summary>
    /// Static class that provides basic helpers to support cryptographic operations.
    /// </summary>
    public static class BitHelpers
    {
        #region Common Operations
        /// <summary>
        /// Generate random numbers.
        /// </summary>
        /// <param name="length">Length of bytes.</param>
        /// <returns>Random numbers.</returns>
        /// <exception cref="InvalidOperationException">'length' must be greater than 0</exception>
        public static byte[] GenerateRandomNumbers(int length)
        {
            if (length <= 0)
                throw new InvalidOperationException("param 'length' must be greater than 0");
            byte[] randomBytes = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }


        /// <summary>
        /// Convert given string to UTF-8 bytes.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>UTF-8 bytes</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public static byte[] ConvertStringToBytes(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            return UTF8Encoding.UTF8.GetBytes(input);
        }


        /// <summary>
        /// Convert bytes data to UTF-8 string.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <returns>UTF-8 string.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        public static string ConvertBytesToString(byte[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            return UTF8Encoding.UTF8.GetString(data);
        }


        /// <summary>
        /// Convert bytes data to Base64 encoded string.
        /// </summary>
        /// <param name="data">Data in bytes.</param>
        /// <returns>Base64 encoded string.</returns>
        /// <exception cref="ArgumentNullException">'data' cannot be null/empty.</exception>
        public static string ConvertToBase64String(byte[] data)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException(nameof(data));
            return Convert.ToBase64String(data);
        }


        /// <summary>
        /// Convert Base64 encoded string to bytes.
        /// </summary>
        /// <param name="base64String">Base64 encoded string.</param>
        /// <returns>Data in bytes. Returns null if operation fails.</returns>
        /// <exception cref="ArgumentNullException">'base64String' cannot be null/empty.</exception>
        public static byte[] ConvertFromBase64String(string base64String)
        {
            if (String.IsNullOrEmpty(base64String))
                throw new ArgumentNullException(nameof(base64String));
            byte[] result = null;
            try
            {
                result = Convert.FromBase64String(base64String);
            }
            catch { result = null; }
            return result;
        }
        #endregion

        #region ToBinary Operations
        /// <summary>
        /// Convert string to binary.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Binary string.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public static string ConvertToBinary(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            StringBuilder stringBuilder = new StringBuilder();
            int len = input.Length;
            for(int i=0;i<len;i++)
            {
                if (i == len - 1)
                    stringBuilder.Append(ConvertToBinary(input[i]));
                else
                    stringBuilder.Append(ConvertToBinary(input[i]) + " ");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Convert char to binary.
        /// </summary>
        /// <param name="input">Input as char</param>
        /// <returns>Binary string.</returns>
        public static string ConvertToBinary(char input)
        {
            return ConvertToBinary((long)input);
        }
        /// <summary>
        /// Convert short to binary
        /// </summary>
        /// <param name="input">Input as short</param>
        /// <returns>Binary string.</returns>
        public static string ConvertToBinary(short input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 2);
            }
            catch { result = ""; }
            return result;

        }
        /// <summary>
        /// Convert Int32 to binary
        /// </summary>
        /// <param name="input">Input as Int32</param>
        /// <returns>Binary string.</returns>
        public static string ConvertToBinary(int input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 2);
            }
            catch { result = ""; }
            return result;

        }
        /// <summary>
        /// Convert Int64 to binary
        /// </summary>
        /// <param name="input">Input as Int64</param>
        /// <returns>Binary string.</returns>
        public static string ConvertToBinary(long input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 2);
            }
            catch { result = ""; }
            return result;
        }
        #endregion

        #region FromBinaryToInt64
        /// <summary>
        /// Convert from binary to array of long. Each binary converted value will be stored in 'result' index.
        /// </summary>
        /// <param name="binary">Binary data in string format. Ex: 0011 0011</param>
        /// <param name="result">Array of long. It'll be empty if conversion fails.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'binary' cannot be null/empty</exception>
        public static bool ConvertFromBinary(string binary, out long[] result)
        {
            if (String.IsNullOrEmpty(binary))
                throw new ArgumentNullException(nameof(binary));
            string[] binaries = binary.Trim().Split();
            int len = binaries.Length;
            result = new long[len];
            for(int i=0;i<len;i++)
            {
                try
                {
                    result[i] = Convert.ToInt64(binaries[i], 2);
                    
                }
                catch
                {
                    result = null;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Convert from binary string to real string.
        /// </summary>
        /// <param name="binary">Binary data in string format. Ex: 0011 0011</param>
        /// <param name="result">Real string as a result of conversion.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'binary' cannot be null/empty</exception>
        public static bool ConvertFromBinary(string binary, out string result)
        {
            long[] values = null;
            bool conversionResult = ConvertFromBinary(binary, out values);
            result = "";
            if (!conversionResult)
                return false;
            else
            {
                foreach(long i in values)
                {
                    result += (char)i;
                }

                return true;
            }

        }
        #endregion


    }
}
