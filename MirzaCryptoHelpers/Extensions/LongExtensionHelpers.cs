using MirzaCryptoHelpers.Common;

namespace MirzaCryptoHelpers.Extensions
{
    /// <summary>
    /// This class contains method extensions for common conversions in long format.
    /// </summary>
    public static class LongExtensionHelpers
    {
        /// <summary>
        /// Converts long to UTF-8 bytes.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>UTF-8 bytes.</returns>
        public static byte[] ToUTF8Bytes(long value)
        {
            return value.ToString().ToUTF8Bytes();
        }
        /// <summary>
        /// Converts long to binary string.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>Binary string.</returns>
        public static string ToBinary(long value)
        {
            return BitHelpers.ConvertToBinary(value);
        }
        /// <summary>
        /// Converts long to octal string.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>Octal string.</returns>
        public static string ToOctal(long value)
        {
            return BitHelpers.ConvertToOctal(value);
        }
        /// <summary>
        /// Converts long to hexadecimal string.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToHexadecimal(long value)
        {
            return BitHelpers.ConvertToHexadecimal(value);
        }
        /// <summary>
        /// Gets MD5 hash from current long in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>MD5 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetMD5Hash(long value)
        {
            return value.ToString().GetMD5Hash();
        }
        /// <summary>
        /// Gets SHA1 hash from current long in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>SHA1 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA1Hash(long value)
        {
            return value.ToString().GetSHA1Hash();
        }
        /// <summary>
        /// Gets SHA256 hash from current long in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>SHA256 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA256Hash(long value)
        {
            return value.ToString().GetSHA256Hash();
        }
        /// <summary>
        /// Gets SHA384 hash from current long in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>SHA384 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA384Hash(long value)
        {
            return value.ToString().GetSHA384Hash();
        }
        /// <summary>
        /// Gets SHA512 hash from current long in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">long value.</param>
        /// <returns>SHA512 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA512Hash(long value)
        {
            return value.ToString().GetSHA512Hash();
        }
    }
}
