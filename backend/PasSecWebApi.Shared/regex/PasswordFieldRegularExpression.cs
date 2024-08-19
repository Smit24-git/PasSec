using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasSecWebApi.Shared.regex
{
    public static partial class PasswordFieldRegularExpression
    {
        private const string Pattern = "(?=.*[^a-zA-Z0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?!.*[\\s])([a-zA-Z0-9]|[^a-zA-Z0-9]){16,}";

        [GeneratedRegex(pattern: Pattern)]
        public static partial Regex PasswordFieldRegex();
    }
}
