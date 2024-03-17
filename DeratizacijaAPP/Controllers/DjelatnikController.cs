 using DeratizacijaAPP.Data;
using DeratizacijaAPP.Extensions;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Djelatnik u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DjelatnikController : ControllerBase
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
        public DjelatnikController(DeratizacijaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve djelatnike iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///     GET api/v1/djelatnik
        /// 
        /// </remarks>
        /// <returns>Djelatnici u bazi</returns>
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
                var djelatnici = _context.Djelatnici.ToList();
                if (djelatnici == null || djelatnici.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(djelatnici.MapDjelatnikReadList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dohvaća jednog djelatnika za promjenu
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
                var djelatnik = _context.Djelatnici.Find(sifra);
                if (djelatnik == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(djelatnik.MapDjelatnikInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novog djelatnika u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/djelatnik
        ///     {ime: "Primjer imena"}
        /// </remarks>
        /// <param name="djelatnik">Djelatnik za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Djelatnik sa šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(DjelatnikDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var djelatnik = dto.MapDjelatnikInsertUpdateFromDTO(new Djelatnik());
                _context.Djelatnici.Add(djelatnik);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, djelatnik.MapDjelatnikReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg djelatnika u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/djelatnik/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "ime": "Novo ime",
        ///  "prezime": "Novo prezime",
        ///  "broj mobitela": "099/123-4567"
        ///  "oib": "12345678910"
        ///  "struka": "Sanitarni tehničar"
        /// }
        /// </remarks>
        /// <param name="sifra">Šifra djelatnika koji se mijenja</param>  
        /// <param name="djelatnik">Djelatnik za unijeti u JSON formatu</param>         
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi djelatnika kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Svi poslani podaci od djelatnika koji su spremljeni u bazi</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, DjelatnikDTOInsertUpdate dto)
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var djelatnikUBazi = _context.Djelatnici.Find(sifra);
                if (djelatnikUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }
                
                var djelatnik = dto.MapDjelatnikInsertUpdateFromDTO(djelatnikUBazi);

                _context.Djelatnici.Update(djelatnik);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, djelatnik.MapDjelatnikReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše djelatnika iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/djelatnik/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra djelatnika koji se briše</param>  
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi djelatnika kojeg želimo obrisati</response>
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
                var djelatnikUBazi = _context.Djelatnici.Find(sifra);
                
                if (djelatnikUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Djelatnici.Remove(djelatnikUBazi);
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
