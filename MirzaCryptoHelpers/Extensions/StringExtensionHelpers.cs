﻿using System;
using MirzaCryptoHelpers.Common;
using MirzaCryptoHelpers.Hashings;

namespace MirzaCryptoHelpers.Extensions
{
    /// <summary>
    /// This class contains method extensions for common conversions.
    /// </summary>
    public static class StringExtensionHelpers
    {
        /// <summary>
        /// Convert string to UTF-8 bytes.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>UTF-8 bytes.</returns>
        public static byte[] ToUTF8Bytes(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return BitHelpers.ConvertStringToBytes(value);
        }
        /// <summary>
        /// Convert string to binary string.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Binary string.</returns>
        public static string ToBinary(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return BitHelpers.ConvertToBinary(value);
        }
        /// <summary>
        /// Convert from binary string to string. If conversion fails, return null.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Converted string. If conversion fails, return null.</returns>
        public static string FromBinary(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            string result;
            BitHelpers.ConvertFromBinary(value, out result);
            return result;
        }
        /// <summary>
        /// Convert string to octal string.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Octal string.</returns>
        public static string ToOctal(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return BitHelpers.ConvertToOctal(value);
        }
        /// <summary>
        /// Convert from octal string to string. If conversion fails, return null.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Converted string. If conversion fails, return null.</returns>
        public static string FromOctal(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            string result;
            BitHelpers.ConvertFromOctal(value, out result);
            return result;
        }
        /// <summary>
        /// Convert string to octal string.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToHexadecimal(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return BitHelpers.ConvertToHexadecimal(value);
        }
        /// <summary>
        /// Convert from hexadecimal string to string. If conversion fails, return null.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null.</param>
        /// <returns>Converted string. If conversion fails, return null.</returns>
        public static string FromHexadecimal(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            string result;
            BitHelpers.ConvertFromHexadecimal(value, out result);
            return result;
        }
        /// <summary>
        /// Get MD5 hash from current string in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null</param>
        /// <returns>MD5 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetMD5Hash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new MD5Crypto().GetHashBase64String(value);
        }

        /// <summary>
        /// Get SHA1 hash from current string in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null</param>
        /// <returns>SHA1 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA1Hash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new SHA1Crypto().GetHashBase64String(value);
        }

        /// <summary>
        /// Get SHA256 hash from current string in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null</param>
        /// <returns>SHA256 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA256Hash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new SHA256Crypto().GetHashBase64String(value);
        }

        /// <summary>
        /// Get SHA384 hash from current string in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null</param>
        /// <returns>SHA384 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA384Hash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new SHA384Crypto().GetHashBase64String(value);

        }

        /// <summary>
        /// Get SHA512 hash from current string in Base64 encoded string format. Returns null if fails.
        /// </summary>
        /// <param name="value">String value. If null/empty, returns null</param>
        /// <returns>SHA512 hash in Base64 encoded string. Returns null if fails </returns>
        public static string GetSHA512Hash(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return new SHA512Crypto().GetHashBase64String(value);
        }
        /// <summary>
        /// Convert current string to Base64 encoded string.
        /// </summary>
        /// <param name="value">String value.</param>
        /// <returns>Base64 encoded string.</returns>
        public static string ToBase64String(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            return BitHelpers.ConvertToBase64String(BitHelpers.ConvertStringToBytes(value));
        }
        /// <summary>
        /// Convert Base64 encoded string to normal string. Returns null either it's invalid format or conversion fails.
        /// </summary>
        /// <param name="value">Base64 encoded string.</param>
        /// <returns>Normal string. Returns null either it's invalid format or conversion fails.</returns>
        public static string FromBase64String(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            var getBytes = BitHelpers.ConvertFromBase64String(value);
            if (getBytes == null)
                return null;
            return BitHelpers.ConvertBytesToString(getBytes);
        }
    }
}
