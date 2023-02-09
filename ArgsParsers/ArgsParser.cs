using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotbaseball.Utils
{
    public class ArgsParser 
    {
        public static BaseballArgs Parse(string[] args, int expected)
        {
            if (args == null || args.Length < ++expected)
            {
                Console.WriteLine("Invalid number of parameters. This module expected {0} parameters.", expected);
                Environment.Exit(0);
            }

            var ba = new BaseballArgs();
            ba.parameters.AddRange(args);
            
            // Remove index 0 - always the module name
            ba.parameters.RemoveAt(0);

            // Process --sort, if present
            int idx = ba.parameters.FindIndex(x => x.Trim() == "--sort");            
            if (idx != -1)
            {                
                try
                {
                    if (ba.parameters.Count >= (idx + 2))
                    {
                        // Remove --sort and the next value
                        ba.sort = ba.parameters[idx+1];
                        ba.parameters.RemoveRange(idx, 2);
                    }
                    else if (ba.parameters.Count >= (idx + 1))
                    {
                        // Just remove the --sort
                        ba.parameters.RemoveAt(idx);
                    }
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }

            return ba;
        }
    }
}