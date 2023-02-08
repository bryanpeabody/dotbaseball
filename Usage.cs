using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dotbaseball
{
    public class Usage 
    {
        public static void ShowUsage()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "";
            Console.WriteLine("dotBaseball: Pulls baseball stats from the web.");
            Console.WriteLine("Version: {0} ", version);
            Console.WriteLine();
            Console.WriteLine("Usage: ");
            Console.WriteLine("\t--info: shows program information and usage");
            Console.WriteLine("\tamateurdraft <year> <round>: shows the amateur draft for the given year and round");
            Console.WriteLine("Options:");
            Console.WriteLine("\t --sort <sortField>: Sort the data by a specific column.");
        }
    }
}