using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Contracts.Infrastructure
{
    public interface IDataDecryptor
    {
        string DecryptCipher(string key, string iv, string cipher);
    }
}
