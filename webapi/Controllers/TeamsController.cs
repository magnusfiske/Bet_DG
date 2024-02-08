using Bet.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly IDbService _db;

    public TeamsController(IDbService db)
    {
        _db = db;
    }

    // GET: <TeamsController>
    [Authorize(Roles = "Registered")]
    [HttpGet]
    public async Task<IResult> Get()
    {
        var response = await _db.GetAsync<Data.Entities.Team, TeamDTO>();
        var updatedPositions = UpdatePositions(response);
        return Results.Ok(updatedPositions);
    }

    // GET <TeamsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var results = await _db.SingleAsync<Data.Entities.Team, TeamDTO>(e => e.Id.Equals(id));
        return results != null ? Results.Ok(results) : Results.NotFound();
    }

    // POST <TeamsController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] TeamDTO dto)
    {
        try
        {
            var entity = await _db.AddAsync<Data.Entities.Team, TeamDTO>(dto);
            if (await _db.SaveChangesAsync())
            {
                var node = typeof(Data.Entities.Team).Name.ToLower();
                return Results.Created($"/{node}s/{entity.Id}", entity);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(Data.Entities.Team).Name} " +
                $"entity.\n{ex}.");
        }

        return Results.BadRequest();
    }

    // PUT <TeamsController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] TeamDTO dto)
    {
        try
        {
            if (!await _db.AnyAsync<Data.Entities.Team>(e => e.Id.Equals(id)))
                return Results.NotFound();

            _db.Update<Data.Entities.Team, TeamDTO>(id, dto);

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn´t update the {typeof(Data.Entities.Team).Name} entity. \n {ex}");
        }
        return Results.BadRequest($"Couldn´t update the {typeof(Data.Entities.Team).Name} entity. \n");
    }

    // DELETE <TeamsController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if (!await _db.DeleteAsync<Data.Entities.Team>(id))
                return Results.NotFound();

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Coultdn´t delete the {typeof(Data.Entities.Team)} entity. \n{ex}");
        }
        return Results.BadRequest($"Coultdn´t delete the {typeof(Data.Entities.Team)} entity.");
    }

    private List<TeamDTO> UpdatePositions(List<TeamDTO> teamsFromDb)
    { 

        List<webapi.Team>? teamsFromWeb = Webscraper.Teams;
        if (teamsFromWeb != null)
        {
            foreach (var team in teamsFromDb)
            {
                foreach (var scrapedTeam in teamsFromWeb)
                {
                    if (team.Name == scrapedTeam.Name)
                    {
                        team.Position = scrapedTeam.Position;
                    }
                }
            }
        }
        return teamsFromDb;
    }
}
