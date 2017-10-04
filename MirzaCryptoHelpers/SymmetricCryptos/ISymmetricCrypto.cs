namespace MirzaCryptoHelpers.SymmetricCryptos
{
    public interface ISymmetricCrypto
    {
        byte[] Encrypt(byte[] data, string password);
        byte[] Decrypt(byte[] data, string password);
    }
}
