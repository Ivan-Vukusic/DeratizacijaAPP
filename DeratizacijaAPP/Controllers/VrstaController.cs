using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Vrsta u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class VrstaController : DeratizacijaController<Vrsta, VrstaDTORead, VrstaDTOInsertUpdate>
    {

        public VrstaController(DeratizacijaContext context) : base(context)
        {
            DbSet = _context.Vrste;
        }

        protected override void KontrolaBrisanje(Vrsta entitet)
        {
            var lista = _context.Objekti.Include(x => x.Vrsta).Where(x => x.Vrsta.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Vrsta se ne može obrisati jer je postavljena na objektima: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Mjesto).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

    }
}
