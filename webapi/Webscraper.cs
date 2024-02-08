using Bet.Data.Entities;
using System.Reflection.Metadata;
using System.Xml;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Bet.API.Controllers;

namespace webapi;

public static class Webscraper
{
    public static List<Team>? Teams { get 
        {
            return GetDataFromWeb();
        } 
        private set
        {
            Teams = value;
        }
    }

    private static List<Team> GetDataFromWeb()
    {
        List<Team> teams = new List<Team>();

        var web = new HtmlWeb();

        try
        {
            var document = web.Load("https://www.svenskfotboll.se/serier-cuper/tabell-och-resultat/allsvenskan-2024/115560/");

            var teamHTMLElements = document.DocumentNode.QuerySelectorAll("tr.standings-table__row");

            foreach (var teamElement in teamHTMLElements)
            {
                var name = HtmlEntity.DeEntitize(teamElement.QuerySelector("td.standings-table__cell-team").InnerText);
                var position = int.Parse(HtmlEntity.DeEntitize(teamElement.QuerySelector("td.standings-table__cell-position").InnerText));

                var team = new Team() { Name = name, Position = position };

                teams.Add(team);
            }
        } catch(Exception ex)
        {
            throw ex;
        }

        return teams;
    }
}

public class Team
{
public string? Name { get; set; }
public int? Position { get; set; }
}
