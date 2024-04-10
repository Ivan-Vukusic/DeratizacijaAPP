using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace DeratizacijaAPP.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutorizacijaController : ControllerBase
    {

        private readonly DeratizacijaContext _context;


        public AutorizacijaController(DeratizacijaContext context)
        {
            _context = context;
        }



        [HttpPost("token")]
        public IActionResult GenerirajToken(OperaterDTO operater)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var operBaza = _context.Operateri
                   .Where(p => p.Email!.Equals(operater.email))
                   .FirstOrDefault();

            if (operBaza == null)
            {
                // Šaljem Status403Forbidden jer frontend hvata sve 401 i baca na login pa nikada ne dobijem poruku da
                // nije dobro korisničko ime ili lozinka
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, ne mogu naći operatera");
            }



            if (!BCrypt.Net.BCrypt.Verify(operater.password, operBaza.Lozinka))
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Niste autorizirani, lozinka ne odgovara");
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("MojKljucKojijeJakoTajan i dovoljno dugačak da se može koristiti");


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(8)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);


            return Ok(jwt);

        }
    }
}