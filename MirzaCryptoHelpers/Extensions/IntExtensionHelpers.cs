using MirzaCryptoHelpers.Common;

namespace MirzaCryptoHelpers.Extensions
{
    /// <summary>
    /// This class contains method extensions for common conversions in int format.
    /// </summary>
    public static class IntExtensionHelpers
    {
        /// <summary>
        /// Convert int to UTF-8 bytes.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>UTF-8 bytes.</returns>
        public static byte[] ToUTF8Bytes(this int value)
        {
            return value.ToString().ToUTF8Bytes();
        }
        /// <summary>
        /// Convert int to binary string.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>Binary string.</returns>
        public static string ToBinary(this int value)
        {
            return BitHelpers.ConvertToBinary(value);
        }
        /// <summary>
        /// Convert int to octal string.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>Octal string.</returns>
        public static string ToOctal(this int value)
        {
            return BitHelpers.ConvertToOctal(value);
        }
        /// <summary>
        /// Convert int to hexadecimal string.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToHexadecimal(this int value)
        {
            return BitHelpers.ConvertToHexadecimal(value);
        }
        /// <summary>
        /// Get MD5 hash from current int in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>MD5 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetMD5Hash(this int value)
        {
            return value.ToString().GetMD5Hash();
        }
        /// <summary>
        /// Get SHA1 hash from current int in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>SHA1 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA1Hash(this int value)
        {
            return value.ToString().GetSHA1Hash();
        }
        /// <summary>
        /// Get SHA256 hash from current int in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>SHA256 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA256Hash(this int value)
        {
            return value.ToString().GetSHA256Hash();
        }
        /// <summary>
        /// Get SHA384 hash from current int in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>SHA384 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA384Hash(this int value)
        {
            return value.ToString().GetSHA384Hash();
        }
        /// <summary>
        /// Get SHA512 hash from current int in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">int value.</param>
        /// <returns>SHA512 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA512Hash(this int value)
        {
            return value.ToString().GetSHA512Hash();
        }
    }
}
