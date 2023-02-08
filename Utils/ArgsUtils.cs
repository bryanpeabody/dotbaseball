using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotbaseball.Utils
{
    public class ArgsUtils 
    {
        public static string ParseString(string arg)
        {
            return (arg == null) ? string.Empty : arg.Trim();
        }

        public static int parseInt(string arg)
        {
            if (arg == null) 
            {
                throw new Exception("Argument cannot be null!");
            }
            else
            {
                int result;
                return Int32.TryParse(arg, out result) ? result : throw new Exception("Could not parse int parameter!");
            } 
        }
    }
}