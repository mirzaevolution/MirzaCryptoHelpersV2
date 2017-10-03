using System;
using System.Text;
using System.Security.Cryptography;
using MirzaCryptoHelpers.Hashings;
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

        /// <summary>
        /// Create secure password based on the predefined input.
        /// Hash algorithm must implement IHash interface and choose algorithm
        /// will determine the size of password returned.
        /// </summary>
        /// <param name="input">Input as string. It's called predefined input.</param>
        /// <param name="hashCrypto">Concrete hash algorithm that implements IHash interface</param>
        /// <param name="iteration">Iteration count.</param>
        /// <returns>Hashed password in bytes</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        /// <exception cref="ArgumentNullException">'hashCrypto' cannot be null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">'iteration' is invalid.</exception>
        public static byte[] CreateSecurePassword(string input, IHash hashCrypto, int iteration = 10000)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            if (hashCrypto == null)
                throw new ArgumentNullException(nameof(hashCrypto));
            if (iteration < 5000)
                throw new ArgumentOutOfRangeException(nameof(iteration), "Min value for iteration is 5000");
            byte[] data = null;
            try
            {
                byte[] hashedInput = hashCrypto.GetHashBytes(input);
                using (Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(input, hashedInput, iteration))
                {
                    data = generator.GetBytes(hashCrypto.HashSize / 8);
                }
            }
            catch { data = null; }
            return data;
        }
        #endregion
        
        #region ToBinary Operations
        /// <summary>
        /// Convert string input to binary.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Binary string. Returns Empty string if fails.</returns>
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
        /// <param name="input">Input as char.</param>
        /// <returns>Binary string. Returns Empty string if fails.</returns>
        public static string ConvertToBinary(char input)
        {
            return ConvertToBinary((long)input);
        }
        /// <summary>
        /// Convert Int16 input to binary
        /// </summary>
        /// <param name="input">Input as Int16.</param>
        /// <returns>Binary string. Returns Empty string if fails.</returns>
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
        /// Convert Int32 input to binary
        /// </summary>
        /// <param name="input">Input as Int32.</param>
        /// <returns>Binary string. Returns Empty string if fails.</returns>
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
        /// Convert Int64 input to binary.
        /// </summary>
        /// <param name="input">Input as Int64.</param>
        /// <returns>Binary string. Returns Empty string if fails.</returns>
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

        #region FromBinary
        /// <summary>
        /// Convert from binary to array of Int64. Each binary converted value will be stored in 'result' index.
        /// </summary>
        /// <param name="binary">Binary data in string format. Ex: 0011 0011.</param>
        /// <param name="result">Array of Int64. It'll be empty if conversion fails.</param>
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
        /// <param name="binary">Binary data in string format. Ex: 0011 0011.</param>
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
        

        #region ToOctal Operations
        /// <summary>
        /// Convert string input to octal.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Octal string. Returns empty string if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public static string ConvertToOctal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            StringBuilder stringBuilder = new StringBuilder();
            int len = input.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == len - 1)
                    stringBuilder.Append(ConvertToOctal(input[i]));
                else
                    stringBuilder.Append(ConvertToOctal(input[i]) + " ");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Convert char input to octal.
        /// </summary>
        /// <param name="input">Input as char.</param>
        /// <returns>Octal string. Returns empty if fails.</returns>
        public static string ConvertToOctal(char input)
        {
            return ConvertToOctal((long)input);
        }
        /// <summary>
        /// Convert Int16 input to octal.
        /// </summary>
        /// <param name="input">Input as Int16.</param>
        /// <returns>Octal in string. Returns empty if fails.</returns>
        public static string ConvertToOctal(short input)
        {
            
            string result = ""; ;
            try
            {
                result = Convert.ToString(input, 8);
            }
            catch { result = ""; }
            return result;
        }
        /// <summary>
        /// Convert Int32 input to octal.
        /// </summary>
        /// <param name="input">Input as Int32.</param>
        /// <returns>Octal string. Returns empty if fails.</returns>
        public static string ConvertToOctal(int input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 8);
            }
            catch { result = ""; }
            return result;
        }
        /// <summary>
        /// Convert Int64 to octal.
        /// </summary>
        /// <param name="input">Input as Int64.</param>
        /// <returns>Octal string. Returns empty if fails.</returns>
        public static string ConvertToOctal(long input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 8);
            }
            catch { result = ""; }
            return result;
        }
        #endregion

        #region FromOctal Operations
        /// <summary>
        /// Convert from octal to array of Int64. Each octal converted value will be stored in 'result' index.
        /// </summary>
        /// <param name="octal">Octal data in string format. Ex: 435 014 555.</param>
        /// <param name="result">Array of Int64. It'll be empty if conversion fails.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'octal' cannot be null/empty.</exception>
        public static bool ConvertFromOctal(string octal, out long[] result)
        {
            if (String.IsNullOrEmpty(octal))
                throw new ArgumentNullException(nameof(octal));
            string[] octals = octal.Trim().Split();
            int len = octals.Length;
            result = new long[len];
            for (int i = 0; i < len; i++)
            {
                try
                {
                    result[i] = Convert.ToInt64(octals[i],8);

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
        /// Convert from octal string to real string.
        /// </summary>
        /// <param name="octal">Octal data in string format. Ex: 435 014 555.</param>
        /// <param name="result">Real string as a result of conversion.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'octal' cannot be null/empty</exception>
        public static bool ConvertFromOctal(string octal, out string result)
        {
            long[] values = null;
            bool conversionResult = ConvertFromOctal(octal, out values);
            result = "";
            if (!conversionResult)
                return false;
            else
            {
                foreach (long i in values)
                {
                    result += (char)i;
                }

                return true;
            }

        }
        #endregion

                
        #region ToHexadecimal Operations
        /// <summary>
        /// Convert string input to hexadecimal.
        /// </summary>
        /// <param name="input">Input as string.</param>
        /// <returns>Hexadecimal string. Returns empty string if fails.</returns>
        /// <exception cref="ArgumentNullException">'input' cannot be null/empty.</exception>
        public static string ConvertToHexadecimal(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
            StringBuilder stringBuilder = new StringBuilder();
            int len = input.Length;
            for (int i = 0; i < len; i++)
            {
                if (i == len - 1)
                    stringBuilder.Append(ConvertToHexadecimal(input[i]));
                else
                    stringBuilder.Append(ConvertToHexadecimal(input[i]) + " ");
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Convert char input to hexadecimal.
        /// </summary>
        /// <param name="input">Input as char.</param>
        /// <returns>Hexadecimal string. Returns empty if fails.</returns>
        public static string ConvertToHexadecimal(char input)
        {
            return ConvertToHexadecimal((long)input);
        }
        /// <summary>
        /// Convert Int16 input to hexadecimal.
        /// </summary>
        /// <param name="input">Input as Int16.</param>
        /// <returns>Hexadecimal in string. Returns empty if fails.</returns>
        public static string ConvertToHexadecimal(short input)
        {

            string result = ""; 
            try
            {
                result = Convert.ToString(input, 16);
            }
            catch { result = ""; }
            return result;
        }
        /// <summary>
        /// Convert Int32 input to hexadecimal.
        /// </summary>
        /// <param name="input">Input as Int32.</param>
        /// <returns>Hexadecimal string. Returns empty if fails.</returns>
        public static string ConvertToHexadecimal(int input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 16);
            }
            catch { result = ""; }
            return result;
        }
        /// <summary>
        /// Convert Int64 to hexadecimal.
        /// </summary>
        /// <param name="input">Input as Int64.</param>
        /// <returns>Hexadecimal string. Returns empty if fails.</returns>
        public static string ConvertToHexadecimal(long input)
        {
            string result = "";
            try
            {
                result = Convert.ToString(input, 16);
            }
            catch { result = ""; }
            return result;
        }
        #endregion

        #region FromHexadecimal Operations
        /// <summary>
        /// Convert from hexadecimal to array of Int64. Each hexadecimal converted value will be stored in 'result' index.
        /// </summary>
        /// <param name="hexadecimal">Hexadecimal data in string format. Ex: 435 014 555.</param>
        /// <param name="result">Array of Int64. It'll be empty if conversion fails.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'hexadecimal' cannot be null/empty.</exception>
        public static bool ConvertFromHexadecimal(string hexadecimal, out long[] result)
        {
            if (String.IsNullOrEmpty(hexadecimal))
                throw new ArgumentNullException(nameof(hexadecimal));
            string[] hexadecimals = hexadecimal.Trim().Split();
            int len = hexadecimals.Length;
            result = new long[len];
            for (int i = 0; i < len; i++)
            {
                try
                {
                    result[i] = Convert.ToInt64(hexadecimals[i], 16);

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
        /// Convert from hexadecimal string to real string.
        /// </summary>
        /// <param name="hexadecimal">Hexadecimal data in string format. Ex: 1C7A B363 87CD.</param>
        /// <param name="result">Real string as a result of conversion.</param>
        /// <returns>True if conversion succeeds. Else returns false.</returns>
        /// <exception cref="ArgumentNullException">'hexadecimal' cannot be null/empty</exception>
        public static bool ConvertFromHexadecimal(string hexadecimal, out string result)
        {
            long[] values = null;
            bool conversionResult = ConvertFromHexadecimal(hexadecimal, out values);
            result = "";
            if (!conversionResult)
                return false;
            else
            {
                foreach (long i in values)
                {
                    result += (char)i;
                }

                return true;
            }

        }
        #endregion
    }
}
