using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfMap.Class
{
    class CommonClass
    {
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
    }
}
