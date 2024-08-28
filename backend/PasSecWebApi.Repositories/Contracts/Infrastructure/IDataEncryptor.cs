using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Repositories.Contracts.Infrastructure
{
    public interface IDataEncryptor
    {
        (string, string) EncryptValue(string key, string? iv, string value);
    }
}
