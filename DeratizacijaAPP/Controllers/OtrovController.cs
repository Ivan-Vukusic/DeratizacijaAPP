using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Otrov u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OtrovController : ControllerBase
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
        public OtrovController(DeratizacijaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve otrove iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita
        /// 
        ///     GET api/v1/otrov
        /// 
        /// </remarks>
        /// <returns>Otrovi u bazi</returns>
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
                var otrovi = _context.Otrovi.ToList();
                if (otrovi == null || otrovi.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(otrovi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Dodaje novi otrov u bazu
        /// </summary>
        /// <remarks>
        ///     POST api/v1/otrov
        ///     {naziv: "Primjer naziva"}
        /// </remarks>
        /// <param name="otrov">Otrov za unijeti u JSON formatu</param>
        /// <response code="201">Kreirano</response>
        /// <response code="400">Zahtjev nije valjan</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Otrov sa šifrom koju je dala baza</returns>
        [HttpPost]
        public IActionResult Post(Otrov otrov)
        {
            if (!ModelState.IsValid || otrov == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Otrovi.Add(otrov);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, otrov);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg otrova u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/otrov/1
        ///
        /// {
        ///  "sifra": 0,
        ///  "naziv": "Novi naziv",
        ///  "aktivna tvar": "bromadiolon",
        ///  "količina": "1.5"
        ///  "cas broj": "1234-56-7"  
        /// }
        /// </remarks>
        /// <param name="sifra">Šifra otrova koji se mijenja</param>  
        /// <param name="otrov">Otrov za unijeti u JSON formatu</param>         
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">Nema u bazi otrova kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Baza nedostupna</response> 
        /// <returns>Svi poslani podaci za otrov koji su spremljeni u bazi</returns>
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, Otrov otrov)
        {
            if (sifra <= 0 || !ModelState.IsValid || otrov == null)
            {
                return BadRequest();
            }
            try
            {
                var otrovUBazi = _context.Otrovi.Find(sifra);
                if (otrovUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }
                otrovUBazi.Naziv = otrov.Naziv;
                otrovUBazi.AktivnaTvar = otrov.AktivnaTvar;
                otrovUBazi.Kolicina = otrov.Kolicina;
                otrovUBazi.CasBroj = otrov.CasBroj;
                
                _context.Otrovi.Update(otrovUBazi);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, otrovUBazi);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable,
                    ex.Message);
            }
        }

        /// <summary>
        /// Briše otrov iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/otrov/1
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra otrova koji se briše</param>  
        /// <response code="200">Sve je u redu, obrisano je u bazi</response>
        /// <response code="204">Nema u bazi otrova kojeg želimo obrisati</response>
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
                var otrovUBazi = _context.Otrovi.Find(sifra);
                if (otrovUBazi == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, sifra);
                }

                _context.Otrovi.Remove(otrovUBazi);
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
