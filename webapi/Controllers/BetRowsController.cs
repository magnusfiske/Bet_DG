using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bet.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BetRowsController : ControllerBase
{
    private readonly IDbService _db;

    public BetRowsController(IDbService db)
    {
        _db = db;
    }

    // GET: api/<BetRowsController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        return Results.Ok(await _db.GetAsync<BetRow, BetRowDTO>());
    }

    // GET api/<BetRowsController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var results = await _db.SingleAsync<BetRow, BetRowDTO>(e => e.Id.Equals(id));
        return results != null ? Results.Ok(results) : Results.NotFound();
    }

    // POST api/<BetRowsController>
    //[HttpPost]
    //public async Task<IResult> Post([FromBody] BetRowDTO dto)
    //{
    //    try
    //    {
    //        var entity = await _db.AddAsync<BetRow, BetRowDTO>(dto);
    //        if (await _db.SaveChangesAsync())
    //        {
    //            var node = typeof(BetRow).Name.ToLower();
    //            return Results.Created($"/{node}s/{entity.Id}", entity);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return Results.BadRequest($"Couldn't add the {typeof(BetRow).Name} " +
    //            $"entity.\n{ex}.");
    //    }

    //    return Results.BadRequest();
    //}

    [HttpPost]
    public async Task<IResult> PostTable([FromBody] List<BetRowDTO> dtos)
    {
        try
        {
            var entities = await _db.AddRangeAsync<BetRow, BetRowDTO>(dtos.ToArray());
            if (await _db.SaveChangesAsync())
            {
                var node = typeof(BetRow).Name.ToLower();
                return Results.Created($"/{node}s/{entities[0].Id} - {entities[entities.Count() - 1].Id}", entities);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(BetRow).Name} " +
                $"entities.\n{ex}.");
        }
        return Results.BadRequest();
    }

    // PUT api/<BetRowsController>/5
    //[HttpPut("{id}")]
    //public async Task<IResult> Put(int id, [FromBody] BetRowDTO dto)
    //{
    //    try
    //    {
    //        if (!await _db.AnyAsync<BetRow>(e => e.Id.Equals(id)))
    //            return Results.NotFound();

    //        _db.Update<BetRow, BetRowDTO>(id, dto);

    //        if (await _db.SaveChangesAsync())
    //            return Results.NoContent();
    //    }
    //    catch (Exception ex)
    //    {
    //        return Results.BadRequest($"Couldn´t update the {typeof(BetRow).Name} entity. \n {ex}");
    //    }
    //    return Results.BadRequest($"Couldn´t update the {typeof(BetRow).Name} entity. \n");
    //}

    [HttpPut("{betId}")]
    public async Task<IResult> UpdateTable(int betId, [FromBody] BetRowDTO[] dtos)
    {
        try
        {
            if (!await _db.AnyAsync<BetRow>(e => e.BetId.Equals(betId)))
                return Results.NotFound();

            _db.UpdateRange<BetRow, BetRowDTO>(dtos);

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn´t update the {typeof(BetRow).Name} entity. \n {ex}");
        }
        return Results.BadRequest($"Couldn´t update the {typeof(BetRow).Name} entity. \n");
    }

    // DELETE api/<BetRowController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if (!await _db.DeleteAsync<BetRow>(id))
                return Results.NotFound();

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Coultdn´t delete the {typeof(BetRow)} entity. \n{ex}");
        }
        return Results.BadRequest($"Coultdn´t delete the {typeof(BetRow)} entity.");
    }
}
