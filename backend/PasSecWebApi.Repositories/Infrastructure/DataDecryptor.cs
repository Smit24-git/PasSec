using PasSecWebApi.Repositories.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Infrastructure
{
    public class DataDecryptor : IDataDecryptor
    {
        /// <summary>
        /// decrupts cipher text using key and iv used for encryption.
        /// </summary>
        /// <param name="key"> must be 32 char long </param>
        /// <param name="iv"> initialization vector used for encryption </param>
        /// <param name="cipher"> encrypted text </param>
        /// <returns> decrypted value </returns>
        public string DecryptCipher(string key, string iv, string cipher)
        {
            string value;
            using (Aes aes = Aes.Create())
            {
                var bCipher = Convert.FromBase64String(cipher);
                var bIV = Convert.FromBase64String(iv);
                var bKey = Convert.FromBase64String(key);

                using (MemoryStream mStream = new MemoryStream(bCipher))
                {
                    using (CryptoStream stream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            value = reader.ReadToEnd();
                        }
                    }
                }
            }
            return value;

        }
    }
}
