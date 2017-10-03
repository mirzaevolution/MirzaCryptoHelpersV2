namespace MirzaCryptoHelpers.AsymmetricCryptos
{
    
    public interface IAsymmetricCryptography
    {
        SessionKeys GenerateKeys();
        byte[] Encrypt(byte[] data, string publicKeyXml);
        byte[] Decrypt(byte[] data, string privateKeyXml);
    }
}
