using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QbGameLib.Crypto
{
    public class Crypto : IDisposable
    {
        public static void Gen(ref byte[] data)
        {
            Random rand = new Random();
            rand.NextBytes(data);
        }
        
        private Aes aes;
        
        public Crypto(string key,string iv) : this(Encoding.UTF8.GetBytes(key),Encoding.UTF8.GetBytes(iv)) {}
        
        public Crypto(byte[] key,byte[] iv)
        {
            aes = Aes.Create(); 
            aes.Key = key; 
            aes.IV = iv;
        }

        public byte[] Encrypt(byte[] date)
        {
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    csEncrypt.Write(date, 0, date.Length);
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }
        
        public byte[] Decrypt(byte[] date)
        {
            using (MemoryStream msDecrypt = new MemoryStream(date))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (MemoryStream msPlain = new MemoryStream())
                    {
                        csDecrypt.CopyTo(msPlain);
                        return msPlain.ToArray();
                    }
                }
            }
        }

        public void Dispose()
        {
            aes?.Dispose();
        }
    }
}