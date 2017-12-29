using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace ERP.Web.Helpers
{
    public class Encription
    {
        public static string GetEncryptedText(string InputString)
        {
            string str = "";
            try
            {
                string s = InputString;
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                Encoding encoding = new UTF8Encoding();
                byte[] bytes1 = encoding.GetBytes("12348765");
                byte[] rgbIV = { 1, 2, 3, 4, 8, 7, 6, 5 };
                ICryptoTransform encryptor = cryptoServiceProvider.CreateEncryptor(bytes1, rgbIV);
                byte[] bytes2 = encoding.GetBytes(s);
                str = Convert.ToBase64String(encryptor.TransformFinalBlock(bytes2, 0, bytes2.Length));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return str;
        }

        public static string GetDecryptedText(string EncryptedString)
        {
            string str = "";
            try
            {
                string s = EncryptedString;
                DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
                Encoding encoding = new UTF8Encoding();
                byte[] bytes1 = encoding.GetBytes("12348765");
                byte[] rgbIV = { 1, 2, 3, 4, 8, 7, 6, 5 };
                ICryptoTransform decryptor = cryptoServiceProvider.CreateDecryptor(bytes1, rgbIV);
                encoding.GetBytes(s);
                byte[] inputBuffer = Convert.FromBase64String(EncryptedString);
                byte[] bytes2 = decryptor.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                str = encoding.GetString(bytes2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return str;
        }
    }
}