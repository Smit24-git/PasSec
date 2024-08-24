using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Contracts.Infrastructure
{
    internal interface IDataEncryptor
    {
        string EncryptValue(string key, out string iv, string value);
    }
}
