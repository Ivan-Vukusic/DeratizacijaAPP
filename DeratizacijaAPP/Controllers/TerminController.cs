using DeratizacijaAPP.Data;
using DeratizacijaAPP.Extensions;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Termin u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TerminController : ControllerBase
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
        public TerminController(DeratizacijaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve termine iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///     GET api/v1/termin
        /// 
        /// </remarks>
        /// <returns>Termini u bazi</returns>
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
                var termini = _context.Termini
                    .Include(t => t.Djelatnik)
                    .Include(t => t.Objekt.Vrsta)
                    .Include(t => t.Otrov)
                    .ToList();
                if (termini == null || termini.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(termini.MapTerminReadList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dohvaća jedan termin za promjenu
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
                var termin = _context.Termini
                    .Include(i => i.Djelatnik)
                    .Include(i => i.Objekt)
                    .Include(i => i.Otrov)
                    .FirstOrDefault(x => x.Sifra == sifra);

                if (termin == null)
                {
                    return new EmptyResult();
                }
                return new JsonResult(termin.MapTerminInsertUpdatedToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novi termin u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/termin
        ///     {datum: "Primjer datuma: yyyy-mm-dd"}
        /// </remarks>
        /// <param name="termin">Termin za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Termin sa šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(TerminDTOInsertUpdate dto)
        {
            if (!ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }

            var djelatnik = _context.Djelatnici.Find(dto.djelatnikSifra);

            if (djelatnik == null) 
            {
                return BadRequest();
            }

            var objekt = _context.Objekti.Find(dto.objektSifra);

            if (objekt == null)
            {
                return BadRequest();
            }

            var otrov = _context.Otrovi.Find(dto.otrovSifra);

            if (otrov == null)
            {
                return BadRequest();
            }

            var entitet = dto.MapTerminInsertUpdateFromDTO(new Termin());

            entitet.Djelatnik = djelatnik;
            entitet.Objekt = objekt;
            entitet.Otrov = otrov;

            try
            {
                _context.Termini.Add(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, entitet.MapTerminReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg termina u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/termin/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "datum": "Novi datum"
        ///  "djelatnik": "Novi djelatnik"
        ///  "objekt": "Novi objekt"
        ///  "otrov": "Novi otrov"
        ///  "napomena": "Nova napomena"
        /// }
        /// </remarks>
        /// <param name="sifra">Šifra termina koji se mijenja</param>  
        /// <param name="termin">Termin za unijeti u JSON formatu</param>         
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi termina kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Svi poslani podaci od termina koji su spremljeni u bazi</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, TerminDTOInsertUpdate dto)
        {
            if (sifra <= 0 || !ModelState.IsValid || dto == null)
            {
                return BadRequest();
            }
            try
            {
                var entitet = _context.Termini
                    .Include(i => i.Djelatnik)
                    .Include(i => i.Objekt)
                    .Include(i => i.Otrov)
                    .FirstOrDefault(x => x.Sifra == sifra);

                if (entitet == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                var djelatnik = _context.Djelatnici.Find(dto.djelatnikSifra);

                if (djelatnik == null)
                {
                    return BadRequest();
                }

                var objekt = _context.Objekti.Find(dto.objektSifra);

                if (objekt == null)
                {
                    return BadRequest();
                }

                var otrov = _context.Otrovi.Find(dto.otrovSifra);

                if (otrov == null)
                {
                    return BadRequest();
                }

                entitet = dto.MapTerminInsertUpdateFromDTO(entitet);
                                
                entitet.Djelatnik = djelatnik;
                entitet.Objekt = objekt;
                entitet.Otrov = otrov;

                _context.Termini.Update(entitet);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, entitet.MapTerminReadToDTO());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše termin iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/termin/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra termina koji se briše</param>  
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi termina kojeg želimo obrisati</response>
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
                var terminUBazi = _context.Termini.Find(sifra);
                if (terminUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Termini.Remove(terminUBazi);
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
