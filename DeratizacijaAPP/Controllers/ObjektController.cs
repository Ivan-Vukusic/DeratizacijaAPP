 using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Objekt u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ObjektController : ControllerBase
    {
        /// <summary>
        /// Kontekst za rad s bazom koji će biti postavljen pomoći Dependency Injection-a
        /// </summary>
        private readonly DeratizacijaContext _context;

        /// <summary>
        /// Konstruktor klase koja prima Deratizacija kontekst
        /// pomoću DI principa
        /// </summary>
        /// <param name="context"></param>
        public ObjektController(DeratizacijaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve objekte iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///     GET api/v1/objekt
        /// 
        /// </remarks>
        /// <returns>Objekti u bazi</returns>
        /// <response code = "200">Sve ok, ako nema podataka content length: 0</response>
        /// <response code = "400">Zahtjev nije valjan</response>
        /// <response code = "503">Baza na koju se spajam nije dostupna</response>
        [HttpGet]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var objekti = _context.Objekti
                    .Include(o => o.Vrsta)
                    .ToList();
                if (objekti == null || objekti.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(objekti);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dohvaća jedan objekt za promjenu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra)
        {
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var objekt = _context.Objekti.Find(sifra);
                if (objekt == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(objekt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novi objekt u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/objekt
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="objekt">Objekt za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Objekt sa šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Objekt objekt)
        {
            if (!ModelState.IsValid || objekt == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Objekti.Add(objekt);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, objekt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg objekta u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/objekt/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "mjesto": "Novo mjesto"
        ///  "adresa": "Nova adresa"
        ///  "vrsta": "Nova vrsta objekta"
        /// }
        /// </remarks>
        /// <param name="sifra">Šifra objekta koji se mijenja</param>  
        /// <param name="objekt">Objekt za unijeti u JSON formatu</param>         
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi objekta kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Svi poslani podaci od objekta koji su spremljeni u bazi</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Objekt objekt)
        {
            if (sifra <= 0 || !ModelState.IsValid || objekt == null)
            {
                return BadRequest();
            }
            try
            {
                var objektUBazi = _context.Objekti.Find(sifra);
                if (objektUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                objektUBazi.Mjesto = objekt.Mjesto;
                objektUBazi.Adresa = objekt.Adresa;
                objektUBazi.Vrsta = objekt.Vrsta;
                

                _context.Objekti.Update(objektUBazi);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, objektUBazi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše objekt iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/objekt/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra objekta koji se briše</param>  
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi objekta kojeg želimo obrisati</response>
        /// <response code="503">Problem s bazom</response>
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("aplication/json")]
        public IActionResult Delete(int sifra)
        {
            if (!ModelState.IsValid || sifra <= 0)
            {
                return BadRequest();
            }
            try
            {
                var objektUBazi = _context.Objekti.Find(sifra);
                if (objektUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Objekti.Remove(objektUBazi);
                _context.SaveChanges();
                return new JsonResult(new { poruka = "Obrisano" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

    }
}
