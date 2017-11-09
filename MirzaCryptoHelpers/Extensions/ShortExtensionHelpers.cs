using MirzaCryptoHelpers.Common;

namespace MirzaCryptoHelpers.Extensions
{
    /// <summary>
    /// This class contains method extensions for common conversions in short format.
    /// </summary>
    public static class ShortExtensionHelpers
    {
        /// <summary>
        /// Converts short to UTF-8 bytes.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>UTF-8 bytes.</returns>
        public static byte[] ToUTF8Bytes(this short value)
        {
            return value.ToString().ToUTF8Bytes();
        }
        /// <summary>
        /// Converts short to binary string.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>Binary string.</returns>
        public static string ToBinary(this short value)
        {
            return BitHelpers.ConvertToBinary(value);
        }
        /// <summary>
        /// Converts short to octal string.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>Octal string.</returns>
        public static string ToOctal(this short value)
        {
            return BitHelpers.ConvertToOctal(value);
        }
        /// <summary>
        /// Convert short to hexadecimal string.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToHexadecimal(this short value)
        {
            return BitHelpers.ConvertToHexadecimal(value);
        }
        /// <summary>
        /// Gets MD5 hash from current short in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>MD5 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetMD5Hash(this short value)
        {
            return value.ToString().GetMD5Hash();
        }
        /// <summary>
        /// Gets SHA1 hash from current short in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>SHA1 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA1Hash(this short value)
        {
            return value.ToString().GetSHA1Hash();
        }
        /// <summary>
        /// Gets SHA256 hash from current short in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>SHA256 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA256Hash(this short value)
        {
            return value.ToString().GetSHA256Hash();
        }
        /// <summary>
        /// Gets SHA384 hash from current short in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>SHA384 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA384Hash(this short value)
        {
            return value.ToString().GetSHA384Hash();
        }
        /// <summary>
        /// Gets SHA512 hash from current short in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">short value.</param>
        /// <returns>SHA512 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA512Hash(this short value)
        {
            return value.ToString().GetSHA512Hash();
        }
    }
}
