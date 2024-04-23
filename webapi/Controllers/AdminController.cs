using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace webapi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IDbService _db;

        public AdminController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<AdminController>
        //[HttpGet]
        //public async Task<IResult> Get()
        //{
        //    try
        //    {
        //        _db.Include<Data.Entities.Bet>();
        //        _db.Include<BetRow>();
        //        var bets = await _db.GetAsync<Data.Entities.Bet, BetDTO>();
        //        return Results.Ok(bets);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Results.BadRequest(ex);
        //    }

        //    //return Results.NotFound();
        //}
    }
}
