using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Data.Analysis;
using dotbaseball.Utils;
using dotbaseball.Formatters;
using CommandLine;

namespace dotbaseball.AmateurDraft
{
    public class AmateurDraft
    {
        private readonly string _url;
        private readonly string _sort;
        private readonly DataFrame _df;

        public AmateurDraft(string[] args)
        {
            var parsedArgs = ArgsParser.Parse(args, 2);            

            string year = parsedArgs.parameters[0];
            string draftRound = parsedArgs.parameters[1];
            string sort = parsedArgs.sort;

            _url = string.Format("https://www.baseball-reference.com/draft/?year_ID={0}&draft_round={1}&draft_type=junreg&query_type=year_round&", year, draftRound);
            _sort = getSortBy(sort);
            _df = this.parse();
        }

        public void show()
        {            
            _df.OrderBy(_sort).PrettyPrint();
        }

        public string getSortBy(string sort)
        {          
            string lowerSort = sort.ToLower().Trim();

            if (lowerSort == Fields.Year.ToLower()) 
            {
                return Fields.Year;
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
                return Fields.OverallPick;
            }
        }
        
        public DataFrame parse()
        {
            HtmlWeb hw = new HtmlWeb();
			HtmlDocument doc = hw.Load(_url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@id='draft_stats']//tr"); //|th|td|a[@href]

            int rowNum = 0;
			
            List<int> year = new List<int>();
            List<int> overallPick = new List<int>();
            List<int> round = new List<int>();
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
                        team.Add(data[6].InnerText);                                // Team
                        signed.Add(data[7].InnerText);                              // Signed
                        bonus.Add(data[8].InnerText);                               // Bonus
                        name.Add(StringUtils.Clean(data[9].InnerText, "&nbsp;"));   // Name
                        position.Add(data[10].InnerText);                           // Position
                        type.Add(data[23].InnerText);                               // Type
                        draftedOutOf.Add(data[24].InnerText);                       // Drafted out of
						break;
				}
			}
            
            DataFrameColumn[] columns = {
                new PrimitiveDataFrameColumn<int>(Fields.Year, year),
                new PrimitiveDataFrameColumn<int>(Fields.OverallPick, overallPick),
                new PrimitiveDataFrameColumn<int>(Fields.Round, round),
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