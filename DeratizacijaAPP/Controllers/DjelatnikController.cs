using DeratizacijaAPP.Data;
using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Djelatnik u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DjelatnikController : DeratizacijaController<Djelatnik, DjelatnikDTORead, DjelatnikDTOInsertUpdate>
    {
        
        public DjelatnikController(DeratizacijaContext context) : base(context)
        {
            DbSet = _context.Djelatnici;
            _mapper = new MappingDjelatnik();
        }

        [HttpGet]
        [Route("trazi/{uvjet}")]
        public IActionResult TraziDjelatnik(string uvjet)
        {            
            if (uvjet == null || uvjet.Length < 3)
            {
                return BadRequest(ModelState);
            }
                        
            uvjet = uvjet.ToLower();
            try
            {
                IEnumerable<Djelatnik> query = _context.Djelatnici;
                var niz = uvjet.Split(" ");

                foreach (var s in uvjet.Split(" "))
                {
                    query = query.Where(p => p.Ime.ToLower().Contains(s) || p.Prezime.ToLower().Contains(s));
                }

                var djelatnici = query.ToList();

                return new JsonResult(_mapper.MapReadList(djelatnici));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("postaviSliku/{sifra:int}")]
        public IActionResult PostaviSliku(int sifra, SlikaDTO slika)
        {
            if (sifra <= 0)
            {
                return BadRequest("Šifra mora biti veća od nula (0)");
            }
            if (slika.Base64 == null || slika.Base64?.Length == 0)
            {
                return BadRequest("Slika nije postavljena");
            }
            var p = _context.Djelatnici.Find(sifra);
            if (p == null)
            {
                return BadRequest("Ne postoji djelatnik s šifrom " + sifra + ".");
            }
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string dir = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "djelatnici");

                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }
                var putanja = Path.Combine(dir + ds + sifra + ".png");
                System.IO.File.WriteAllBytes(putanja, Convert.FromBase64String(slika.Base64));
                return Ok("Uspješno pohranjena slika");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("traziStranicenje/{stranica}")]
        public IActionResult TraziDjelatnikStranicenje(int stranica, string uvjet = "")
        {
            var poStranici = 8;
            uvjet = uvjet.ToLower();
            try
            {
                var djelatnici = _context.Djelatnici
                    .Where(p => EF.Functions.Like(p.Ime.ToLower(), "%" + uvjet + "%")
                                || EF.Functions.Like(p.Prezime.ToLower(), "%" + uvjet + "%"))
                    .Skip((poStranici * stranica) - poStranici)
                    .Take(poStranici)
                    .OrderBy(p => p.Prezime)
                    .ToList();


                return new JsonResult(_mapper.MapReadList(djelatnici));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        protected override void KontrolaBrisanje(Djelatnik entitet)
        {
            var lista = _context.Termini.Include(x => x.Djelatnik).Where(x => x.Djelatnik.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Djelatnik se ne može obrisati jer je postavljen na terminima: ");
                foreach (var e in lista)
                {
                    if (e.Datum.HasValue)  
                    {
                        sb.Append(e.Datum.Value.ToShortDateString()).Append(", ");
                    }
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
