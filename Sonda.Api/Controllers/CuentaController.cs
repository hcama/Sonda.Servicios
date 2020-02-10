using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sonda.Api.Resources;
using Sonda.Data.Security;

namespace Sonda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CuentaController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration iconfiguration;

        public CuentaController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration iconfiguration )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.iconfiguration = iconfiguration;
        }
        [HttpPost("login")]
        public async Task<ActionResult<JwtTokenResource>> Login([FromBody] RegisterUserResource model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                isPersistent: false, lockoutOnFailure: false);
            if(result.Succeeded)
            {
                var usuario = await userManager.FindByEmailAsync(model.Email);
                var roles = await userManager.GetRolesAsync(usuario);
                return BuildJwtToken(model, roles);
            }else
            {
                ModelState.AddModelError(string.Empty, "Intento de ingreso inválido");
                return BadRequest(ModelState);
            }
        }
        [HttpPost("createUser")]
        public async Task<ActionResult<JwtTokenResource>> CreateUser([FromBody] RegisterUserResource model)
        {
            var user = new ApplicationUser { UserName= model.Email, Email= model.Email};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return BuildJwtToken(model, new List<string>());
            }
            else
            {
                return BadRequest("El usuario o la contraseña son invàlidos");
            }
        }
        private JwtTokenResource BuildJwtToken(RegisterUserResource model, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,model.Email),
                new Claim("Mi valor", "Lo que quiera"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach(var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfiguration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);
            return new JwtTokenResource()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

    }
}