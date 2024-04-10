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

        protected override void KontrolaBrisanje(Djelatnik entitet)
        {
            var lista = _context.Termini.Include(x => x.Djelatnik).Where(x => x.Djelatnik.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Djelatnik se ne može obrisati jer je postavljen na terminima: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Datum).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
