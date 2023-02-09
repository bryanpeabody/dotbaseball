using System.Reflection;
using dotbaseball;
using dotbaseball.Utils;
using dotbaseball.AmateurDraft;

if (args.Length == 0 ||  args[0] == "--info")
{
    Usage.ShowUsage();
    Environment.Exit(0);
}

switch(args[0].ToLower())
{
    case "amateurdraft":
        new AmateurDraft(args).show();
        break;
    case "amateurdraftbyteam":
        new AmateurDraftByTeam(args).show();
        break;
    default:
        Usage.ShowUsage();
        break;
}