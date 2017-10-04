namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    
    public interface IAsymmetricCrypto
    {
        SessionKeys GenerateKeys(int keySize);
        byte[] Encrypt(byte[] data, string publicKeyXml, int keySize);
        byte[] Decrypt(byte[] data, string privateKeyXml, int keySize);
    }
}
