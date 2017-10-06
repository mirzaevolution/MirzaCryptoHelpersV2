# MirzaCryptoHelpersV2

Lightweight helper library to perform common cryptographic operations.
This helper library is wrapper from existing .NET framework library and was created to ease development when we have to deal with cryptography operations, hashing and conversions by simply wrapping all the related operations found in .NET Framework into streamlined classes, methods and extension methods.

### Examples:
1. Converting string to binary and otherwise:
```csharp
using MirzaCryptoHelpers.Extensions;

string hello = "Hello";
string helloInBin = hello.ToBinary(); // 1001000 1100101 1101100 1101100 1101111
string helloFromBin = helloInBin.FromBinary(); //it'll be converted back to "Hello"
```

2. Encrypt string using AES with password:
```csharp
using MirzaCryptoHelpers.SymmetricCryptos;
using MirzaCryptoHelpers.Common;

string input = "Hello World!";
string password = "P@s$w0rD~!";
byte[] cipherInBytes = new AESCrypto().Encrypt(
    BitHelpers.ConvertStringToBytes(input), password
);  //byte[16] { 6, 186, 118, 13, 202, 18, 116, 245, 127, 199, 186, 125, 9, 117, 187, 9 }
string cipherInBase64 =  BitHelpers.ConvertToBase64String(cipherInBytes); //Brp2DcoSdPV/x7p9CXW7CQ==

string plain = BitHelpers.ConvertBytesToString(
	new AESCrypto().Decrypt(
    		BitHelpers.ConvertFromBase64String(cipherInBase64), password
        )
   ); //Hello World!
```
And many other helpers available. Ready? Take a look [our wiki](https://github.com/mirzaevolution/MirzaCryptoHelpersV2/wiki)


## Instal at Nuget.Org
```
Install-Package MirzaCryptoHelpersV2 -Version 2.0.0
```

Best Regards,


#### Mirza Ghulam Rasyid.
