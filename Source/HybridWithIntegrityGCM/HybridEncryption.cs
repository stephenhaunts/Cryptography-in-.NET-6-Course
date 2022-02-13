/*
MIT License

Copyright (c) 2022

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Pluralsight.HybridWithIntegrityGCM;

public class HybridEncryption
{

    public EncryptedPacket EncryptData(byte[] original, RsaEncryption rsaParams)
    {
        var sessionKey = RandomNumberGenerator.GetBytes(32);

        var encryptedPacket = new EncryptedPacket { Nonce = RandomNumberGenerator.GetBytes(12) };

        (byte[] ciphereText, byte[] tag) encrypted = AesGCMEncryption.Encrypt(original, sessionKey, encryptedPacket.Nonce, null);

        encryptedPacket.EncryptedData = encrypted.ciphereText;
        encryptedPacket.Tag = encrypted.tag;
        encryptedPacket.EncryptedSessionKey = rsaParams.EncryptData(sessionKey);
         
        return encryptedPacket;
    }

    public byte[] DecryptData(EncryptedPacket encryptedPacket, RsaEncryption rsaParams)
    {
        var decryptedSessionKey = rsaParams.DecryptData(encryptedPacket.EncryptedSessionKey);

        var decryptedData = AesGCMEncryption.Decrypt(encryptedPacket.EncryptedData, decryptedSessionKey,
            encryptedPacket.Nonce, encryptedPacket.Tag, null);

        return decryptedData;
    }
}