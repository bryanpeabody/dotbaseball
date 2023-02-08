using System.Reflection;
using dotbaseball;
using dotbaseball.Utils;
using dotbaseball.AmateurDraft;

if (args != null && args.Length > 0)
{
    if (args[0] == "--info")
    {
        Usage.ShowUsage();
    }
    else if (args[0].ToLower() == "amateurdraft")
    {        
        if (args.Length < 2)
        {
            Usage.ShowUsage();
        }
        else
        {        
            try
            {    
                int year = ArgsUtils.parseInt(args[1]);
                int round = ArgsUtils.parseInt(args[2]);
                string sort = string.Empty;

                if (args[3] == "--sort" && args.Length == 5)
                {
                    sort = ArgsUtils.ParseString(args[4]);
                }
            
                var draft = new AmateurDraft(year, round, sort);
                draft.show();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
} 
else 
{
    Usage.ShowUsage();
}

