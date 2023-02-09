using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Data.Analysis;
using dotbaseball.Utils;
using dotbaseball.Formatters;

namespace dotbaseball.AmateurDraft
{
    public class AmateurDraftByTeam
    {
        private readonly string _url;
        private readonly string _sort;
        private readonly DataFrame _df;

        public AmateurDraftByTeam(string[] args)
        {
            var parsedArgs = ArgsParser.Parse(args, 2);            

            string teamId = parsedArgs.parameters[0].ToUpper();
            string year = parsedArgs.parameters[1];
            string sort = parsedArgs.sort;

            _url = string.Format("https://www.baseball-reference.com/draft/?team_ID={0}&year_ID={1}&draft_type=junreg&query_type=franch_year", teamId, year);
            _sort = getSortBy(sort);
            _df = this.parse();
        }

        public void show()
        {            
            _df.OrderBy(_sort).PrettyPrint();
        }

        private string getSortBy(string sort)
        {          
            string lowerSort = sort.ToLower().Trim();

            if (lowerSort == Fields.Year.ToLower()) 
            {
                return Fields.Year;
            }
            else if (lowerSort == Fields.RoundPick.ToLower())
            {
                return Fields.RoundPick;
            }
            else if (lowerSort == Fields.OverallPick.ToLower())
            {
                return Fields.OverallPick;
            }
            else if (lowerSort == Fields.Round.ToLower())
            {
                return Fields.Round;
            }
            else if (lowerSort == Fields.Team.ToLower())
            {
                return Fields.Team;
            }
            else if (lowerSort == Fields.Signed.ToLower())
            {
                return Fields.Signed;
            }                                  
            else if (lowerSort == Fields.Bonus.ToLower())
            {
                return Fields.Bonus;
            }            
            else if (lowerSort == Fields.Name.ToLower())
            {
                return Fields.Name;
            }
            else if (lowerSort == Fields.Position.ToLower())
            {
                return Fields.Position;
            }
            else if (lowerSort == Fields.Type.ToLower())
            {
                return Fields.Type;
            }
            else if (lowerSort == Fields.DraftedOutOf.ToLower())
            {
                return Fields.DraftedOutOf;
            }            
            else
            {
                return Fields.Round;
            }
        }
        
        private DataFrame parse()
        {
            HtmlWeb hw = new HtmlWeb();
			HtmlDocument doc = hw.Load(_url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@id='draft_stats']//tr"); //|th|td|a[@href]

            int rowNum = 0;
			
            List<int> year = new List<int>();
            List<int> round = new List<int>();
            List<int> overallPick = new List<int>();
            List<int> roundPick = new List<int>();            
            List<string> team = new List<string>();
            List<string> signed = new List<string>();
            List<string> bonus = new List<string>();
            List<string> name = new List<string>();
            List<string> position = new List<string>();
            List<string> type = new List<string>();
            List<string> draftedOutOf = new List<string>();

			foreach (HtmlNode row in nodes)
			{
                rowNum++;

				switch (rowNum)
				{
					case 1: // Headers						
						break;

					default:     
                        var data = row.SelectNodes("td|th").ToList<HtmlNode>();

                        year.Add(Convert.ToInt16(data[0].InnerText));               // Year                        
                        round.Add(Convert.ToInt16(data[1].InnerText));              // Round                        
                        overallPick.Add(Convert.ToInt16(data[3].InnerText));        // Overall Pick
                        roundPick.Add(Convert.ToInt16(data[4].InnerText));          // Round Pick
                        team.Add(data[5].InnerText);                                // Team
                        signed.Add(data[6].InnerText);                              // Signed
                        bonus.Add(data[7].InnerText);                               // Bonus
                        name.Add(StringUtils.Clean(data[8].InnerText, "&nbsp;"));   // Name
                        position.Add(data[9].InnerText);                            // Position
                        type.Add(data[22].InnerText);                               // Type
                        draftedOutOf.Add(data[23].InnerText);                       // Drafted out of
						break;
				}
			}
            
            DataFrameColumn[] columns = {
                new PrimitiveDataFrameColumn<int>(Fields.Year, year),                
                new PrimitiveDataFrameColumn<int>(Fields.Round, round),
                new PrimitiveDataFrameColumn<int>(Fields.OverallPick, overallPick),
                new PrimitiveDataFrameColumn<int>(Fields.RoundPick, roundPick),
                new StringDataFrameColumn(Fields.Team, team),
                new StringDataFrameColumn(Fields.Signed, signed),
                new StringDataFrameColumn(Fields.Bonus, bonus),
                new StringDataFrameColumn(Fields.Name, name),
                new StringDataFrameColumn(Fields.Position, position),
                new StringDataFrameColumn(Fields.Type, type),
                new StringDataFrameColumn(Fields.DraftedOutOf, draftedOutOf),
            };

            DataFrame df = new(columns);            
            return df;
        }
    }
}