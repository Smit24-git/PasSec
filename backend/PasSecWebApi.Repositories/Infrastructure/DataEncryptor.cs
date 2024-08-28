using PasSecWebApi.Repositories.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Infrastructure
{
    public class DataEncryptor : IDataEncryptor
    {
        /// <summary>
        /// generates new IV for AES cipher.
        /// </summary>
        /// <param name="key"> security key must be 32 char long</param>
        /// <param name="iv"> Initialization Vector required to encrypt Data. </param>
        /// <param name="value"> value to encrypt </param>
        /// <returns> encrypted cipher text along with IV used </returns>
        public (string, string) EncryptValue(string key, string? iv, string value)
        {
            // var spellBytes = Convert.ToBase64String(Encoding.ASCII.GetBytes(spell));
            byte[] bCipher, bIV;
            string cipher;

            //encryption
            var bKey = Convert.FromBase64String(key);
            using (Aes aes = Aes.Create())
            {
                if(string.IsNullOrEmpty(iv)) {
                    bIV = aes.IV;
                    iv = Convert.ToBase64String(bIV);
                } else {
                    bIV = Convert.FromBase64String(iv);
                }

                using (MemoryStream mStream = new MemoryStream())
                {
                    //mStream.Write(ivBytes, 0, ivBytes.Length);
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cStream))
                        {
                            sw.Write(value);
                        }
                    }
                    bCipher = mStream.ToArray()!;
                    cipher = Convert.ToBase64String(bCipher);
                }
            }
            return (cipher,iv);
        }
    }
}
