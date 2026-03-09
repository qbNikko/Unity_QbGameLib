using System;
using System.IO.Hashing;
using System.Text;

namespace QbGameLib.Crypto
{
    public class Hash
    {
        
        //MD5
        public static string MD5Hex(string data)
        {
            return MD5Hex(Encoding.UTF8.GetBytes(data));
        }
        
        public static string MD5Hex(byte[] data)
        {
            String hash = String.Empty;
            foreach (byte b in MD5(data))
            {
                hash+=b.ToString("x2");
            }
            return hash;
        }
        
        public static byte[] MD5(string data)
        {
            return MD5(Encoding.UTF8.GetBytes(data));
        }
        
        public static byte[] MD5(byte[] data)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                return md5.ComputeHash(data);
            }
        }
        
        //CRC32
        public static string CRC32Hex(string data)
        {
            return CRC32Hex(Encoding.UTF8.GetBytes(data));
        } 
        
        public static string CRC32Hex(byte[] data)
        {
            return BitConverter.ToString(CRC32(data)).Replace("-", "");
        }

        public static byte[] CRC32(string data)
        {
            return CRC32(Encoding.UTF8.GetBytes(data));
        } 
        
        public static byte[] CRC32(byte[] data)
        {
            return Crc32.Hash(data);
        } 
    }
}