using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IDbService _db;

    public UsersController(IDbService db)
    {
        _db = db;
    }

    // GET: api/<UsersController>
    [HttpGet]
    public async Task<IResult> Get()
    {
        _db.Include<User>();
        return Results.Ok(await _db.GetAsync<User, UserDTO>());
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        _db.Include<User>();
        var results = await _db.SingleAsync<User, UserDTO>(e => e.Id.Equals(id));
        return results != null ? Results.Ok(results) : Results.NotFound();
    }

    // POST api/<UsersController>
    [HttpPost]
    public async Task<IResult> Post([FromBody] UserDTO dto)
    {
        try
        {
            var entity = await _db.AddAsync<User, UserDTO>(dto);
            if (await _db.SaveChangesAsync())
            {
                var node = typeof(User).Name.ToLower();
                return Results.Created($"/{node}s/{entity.Id}", entity);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(User).Name} " +
                $"entity.\n{ex}.");
        }

        return Results.BadRequest();
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] UserDTO dto)
    {
        try
        {
            if (!await _db.AnyAsync<User>(e => e.Id.Equals(id)))
                return Results.NotFound();

            _db.Update<User, UserDTO>(id, dto);

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn´t update the {typeof(User).Name} entity. \n {ex}");
        }
        return Results.BadRequest($"Couldn´t update the {typeof(User).Name} entity. \n");
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            if (!await _db.DeleteAsync<User>(id))
                return Results.NotFound();

            if (await _db.SaveChangesAsync())
                return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Coultdn´t delete the {typeof(User)} entity. \n{ex}");
        }
        return Results.BadRequest($"Coultdn´t delete the {typeof(User)} entity.");
    }
}
