using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Shared.Exceptions
{
    public class ValidationException:Exception
    {
        public List<string> Errors = new List<string>();
        public ValidationException(List<string> errors) {
            Errors = errors;
        }
    }
}
