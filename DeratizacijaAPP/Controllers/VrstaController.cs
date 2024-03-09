using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Vrsta u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VrstaController : ControllerBase
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
        public VrstaController(DeratizacijaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve vrste iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///     GET api/v1/vrsta
        /// 
        /// </remarks>
        /// <returns>Vrste u bazi</returns>
        /// <response code = "200">Sve ok, ako nema podataka content length: 0</response>
        /// <response code = "400">Zahtjev nije valjan</response>
        /// <response code = "503">Baza na koju se spajam nije dostupna</response>
        [HttpGet]
        public IActionResult Get()
        {
            // Kontrola ukoliko upit nije valjan
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var vrste = _context.Vrste.ToList();
                if (vrste == null || vrste.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(vrste);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dohvaća jednu vrstu objekta za promjenu
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
                var vrsta = _context.Vrste.Find(sifra);
                if (vrsta == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(vrsta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novu vrstu u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/vrsta
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="vrsta">Vrsta za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Vrsta s šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Vrsta vrsta)
        {
            if (!ModelState.IsValid || vrsta == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Vrste.Add(vrsta);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, vrsta);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojeće vrste u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/vrsta/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",  
        /// }
        /// </remarks>
        /// <param name="sifra">Šifra vrste koja se mijenja</param>  
        /// <param name="vrsta">Vrsta za unijeti u JSON formatu</param>         
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi vrste koju želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Svi poslani podaci od vrste koji su spremljeni u bazi</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Vrsta vrsta)
        {
            if (sifra <= 0 || !ModelState.IsValid || vrsta == null)
            {
                return BadRequest();
            }
            try
            {
                var vrstaUBazi = _context.Vrste.Find(sifra);
                if (vrstaUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }
                vrstaUBazi.Naziv = vrsta.Naziv;               

                _context.Vrste.Update(vrstaUBazi);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, vrstaUBazi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše vrstu iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/vrsta/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra vrste koja se briše</param>  
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi smjera kojeg želimo obrisati</response>
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
                var vrstaUBazi = _context.Vrste.Find(sifra);
                if (vrstaUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Vrste.Remove(vrstaUBazi);
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
