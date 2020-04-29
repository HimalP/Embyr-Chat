﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Embyr.Global
{
    class Decrypt
    {
        private static string cipherText;
        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;
        private const string passPhrase = "mwpHHP17937139//!";

        public static string cT
        {
            get { return Decode(cipherText, passPhrase); }
            set { cipherText = value; }
        }

        public static string GlobalVar
        {
            get { return Decode(cipherText, passPhrase); }
            set { cipherText = value; }
        }

        public static string Decode(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}

