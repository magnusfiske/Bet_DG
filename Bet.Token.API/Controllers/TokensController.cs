using Bet.Common.DTOs;
using Bet.Token.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bet.Token.API.Controllers
{
    
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokensController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [Route("api/token")]
        [HttpPost]
        public async Task<IResult> Get(LoginUserDTO loginUser)
        {
            try
            {
                var result = await _tokenService.GetTokenAsync(loginUser);

                if(string.IsNullOrEmpty(result.UserName) || string.IsNullOrEmpty(result.AccessToken))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(result);
            }
            catch
            {
                return Results.Unauthorized();
            }

            
        }

        [Route("api/tokens/create")]
        [HttpPost]
        public async Task<IResult> Create(TokenUserDTO tokenUser)
        {
            try
            {
                var jwt = await _tokenService.GenerateTokenAsync(tokenUser);

                if(string.IsNullOrEmpty(jwt))
                {
                    return Results.Unauthorized();
                }
                return Results.Created("Token", jwt);
            }
            catch 
            {
                return Results.Unauthorized();
            }

        }
    }
}
