using DeratizacijaAPP.Data;
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
