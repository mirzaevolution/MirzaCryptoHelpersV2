namespace MirzaCryptoHelpers.Hashings
{
    public interface IHashCrypto
    {
        int HashSize { get; }
        byte[] GetHashBytes(string input);
        byte[] GetHashBytes(byte[] input);
        string GetHashBase64String(string input);
        string GetHashBase64String(byte[] input);
    }
}
