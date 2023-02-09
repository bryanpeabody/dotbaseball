using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotbaseball.Utils
{
    public class BaseballArgs 
    {
        public List<string> parameters;
        public string sort { get; set; }

        public BaseballArgs()
        {
            parameters = new List<string>();
            sort = string.Empty;
        }     
    }
}