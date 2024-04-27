using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Otrov u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OtrovController : DeratizacijaController<Otrov, OtrovDTORead, OtrovDTOInsertUpdate>
    {

        public OtrovController(DeratizacijaContext context) : base(context)
        {
            DbSet = _context.Otrovi;
        }

        protected override void KontrolaBrisanje(Otrov entitet)
        {
            var lista = _context.Termini.Include(x => x.Otrov).Where(x => x.Otrov.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Otrov se ne može obrisati jer je postavljen na terminima: ");
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
