using Bet.Data.Entities;
using Bet.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bet.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BetController : ControllerBase
{
    private readonly IDbService _db;

    public BetController(IDbService db)
    {
        _db = db;
    }

    // GET: api/<BetController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        try
        {
            _db.Include<Data.Entities.Bet>();
            _db.Include<BetRow>();
            var bets = await _db.GetAsync<Data.Entities.Bet, BetDTO>();
            return Results.Ok(bets);
        }
        catch (Exception ex) 
        {
            return Results.BadRequest(ex);
        }

        //return Results.NotFound();
    }

    // GET api/<BetController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        try
        {
            _db.Include<Data.Entities.Bet>();
            _db.Include<BetRow>();
            var results = await _db.SingleAsync<Data.Entities.Bet, BetDTO>(e => e.UserId.Equals(id));
            return results != null ? Results.Ok(results) : Results.NotFound(new BetDTO());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex);
        }
    }

    // POST api/<BetController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] BetDTO dto)
    {
        try
        {
            var entity = await _db.AddAsync<Data.Entities.Bet, BetDTO>(dto);
            if (await _db.SaveChangesAsync())
            {
                var node = typeof(Data.Entities.Bet).Name.ToLower();
                return Results.Created($"/{node}s/{entity.Id}", entity);
            }
        } 
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(Data.Entities.Bet).Name} " +
                $"entity.\n{ ex}.");
        }

        return Results.BadRequest();
    }

    // PUT api/<BetController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] BetDTO dto)
    {
        try
        {
            if (!await _db.AnyAsync<Data.Entities.Bet>(e => e.Id.Equals(id)))
                return Results.NotFound();

            _db.Update<Data.Entities.Bet, BetDTO>(id, dto);

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn´t update the {typeof(Data.Entities.Bet).Name} entity. \n {ex}");
        }
        return Results.BadRequest($"Couldn´t update the {typeof(Data.Entities.Bet).Name} entity. \n");
    }

    // DELETE api/<BetController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if(!await _db.DeleteAsync<Data.Entities.Bet>(id))
                return Results.NotFound();

            if(await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Coultdn´t delete the {typeof(Data.Entities.Bet)} entity. \n{ex}");
        }
        return Results.BadRequest($"Coultdn´t delete the {typeof(Data.Entities.Bet)} entity.");
    }
}
