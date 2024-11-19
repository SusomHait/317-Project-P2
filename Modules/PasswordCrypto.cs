using System.Security.Cryptography;
using System.Text;

/* This module contains the function definitions used for hashing
 * and encrpytion.
 * 
 * The functionality provided here is already complete.  DO NOT
 * edit this file!
 */
namespace CSC317PassManagerP2Starter.Modules
{

    public class PasswordCrypto
    {
        /********************Compare byte arrays (can be used to compare hashes/encryptions)**************
         */

        public static bool CompareBytes(byte[] b1, byte[] b2)
        {
            return b1.SequenceEqual(b2);
        }

        /********************Hashing Algorithms***********************
         */
        public static byte[]? GetHash(string text)
        {
            if (text == null)
                return null;
            //Convert the string to ASCII Bytes.  This is needed
            //for hashing.
            byte[] source = Encoding.ASCII.GetBytes(text);
            byte[] hash = MD5.HashData(source);

            return hash;
        }

        /******************Encryption Algorithms********************
         */

        public static byte[] Encrypt(string text, Tuple<byte[], byte[]> key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key.Item1;
                aes.IV = key.Item2;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plain_text = Encoding.UTF8.GetBytes(text);
                    cs.Write(plain_text, 0, plain_text.Length);
                    cs.FlushFinalBlock();
                    return ms.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] cipher, Tuple<byte[], byte[]> key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key.Item1;
                aes.IV = key.Item2;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                using (MemoryStream ms = new MemoryStream(cipher))

                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    string plainText;
                    using (StreamReader plainTextReader = new StreamReader(cs))
                    {
                        plainText = plainTextReader.ReadToEnd();

                        ms.Close();
                        cs.Close();
                    }

                    return plainText;
                }
            }
        }

        public static Tuple<byte[], byte[]> GenKey(int keySize = 256)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.GenerateKey();
                byte[] key = aes.Key;
                byte[] iv = aes.IV;

                return Tuple.Create(key, iv);
            }
        }


    }
}
