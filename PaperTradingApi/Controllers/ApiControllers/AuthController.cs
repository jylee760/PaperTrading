using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperTrading.Entities.ApiRepositories;
using PaperTrading.Models.DTO;

namespace PaperTrading.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerReq)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerReq.Username,
                Email = registerReq.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerReq.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerReq.Roles);
                if (identityResult.Succeeded)
                {
                    return Ok("Created");
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Username);
            if (user == null)
            {
                return BadRequest("Username or Password wrong");
            }
            var checkPass = await userManager.CheckPasswordAsync(user,login.Password);
            if (checkPass) { 
                var roles = await userManager.GetRolesAsync(user);
                if (roles != null)
                {
                    var jwtToken = tokenRepository.CreateJwtTokeN(user, roles.ToList());
                    var response = new LoginResponseDTO
                    {
                        jwtToken = jwtToken
                    };
                    return Ok(response);
                }
                
            }
            return BadRequest("Username or Password wrong");
        }
    }
}

