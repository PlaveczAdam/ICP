using InfiniteCreativity.DTO;
using InfiniteCreativity.DTO.Game;
using InfiniteCreativity.Services.CoreNS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace InfiniteCreativity.Controllers.CoreNS
{
    [ApiController, Route("/api/player")]
    public class PlayerController : Controller
    {
        private IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task CreatePlayer([FromBody] CreatePlayerDTO player)
        {
            await _playerService.CreatePlayer(player);
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginPlayerDTO player)
        {
            Guid id;
            try
            {
                id = await _playerService.GetPlayerIdIfValid(player);
                var claims = new List<Claim>() { new Claim(ClaimTypes.Sid, id.ToString()) };
                var identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                await HttpContext.SignInAsync("Cookies", principal, props);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return Unauthorized();
            }

            return Ok(id);
        }
        [Authorize]
        [HttpGet, Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet, Route("current")]
        public async Task<ShowPlayerDTO> GetCurrentPlayer()
        {
            return await _playerService.GetCurrentPlayerDTO();
        }

        [Authorize]
        [HttpGet, Route("game/current")]
        public async Task<ShowGamePlayerDTO> GetCurrentPlayerAll()
        {
            return await _playerService.GetCurrentPlayerAll();
        }

        [Authorize]
        [HttpGet, Route("wallet")]
        public async Task<ShowWalletDTO> GetWallet()
        {
            return await _playerService.GetWallet();
        }
    }
}
