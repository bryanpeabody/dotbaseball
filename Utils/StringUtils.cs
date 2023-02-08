using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotbaseball.Utils
{
    public class StringUtils 
    {
        public static string Clean(string input, string remove)
        {
            if (input != null && input.Length > 0 && input.Contains(remove))
            {
                int idx = input.LastIndexOf(remove);
                input = input.Substring(0, idx);
            }

            return input ?? "";
        }
    }
}