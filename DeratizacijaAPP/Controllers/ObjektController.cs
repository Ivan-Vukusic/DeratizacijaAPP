using Microsoft.EntityFrameworkCore;
using DeratizacijaAPP.Data;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Objekt u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ObjektController : DeratizacijaController<Objekt, ObjektDTORead, ObjektDTOInsertUpdate>
    {

        public ObjektController(DeratizacijaContext context) : base(context)
        {
            DbSet = _context.Objekti;
        }

        protected override void KontrolaBrisanje(Objekt entitet)
        {
            var lista = _context.Termini.Include(x => x.Objekt).Where(x => x.Objekt.Sifra == entitet.Sifra).ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Objekt se ne može obrisati jer je postavljen na terminima: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Objekt).Append(", ");
                }

                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

        protected override List<ObjektDTORead> UcitajSve()
        {
            var lista = _context.Objekti
                    .Include(o => o.Vrsta)
                    .ToList();
            if (lista == null || lista.Count == 0)
            {
                throw new Exception("Ne postoje podaci u bazi");
            }
            return _mapper.MapReadList(lista);
        }

        protected override Objekt NadiEntitet(int sifra)
        {
            return _context.Objekti.Include(i => i.Vrsta).FirstOrDefault(x => x.Sifra == sifra) ?? throw new Exception("Ne postoji objekt s šifrom " + sifra + " u bazi");
        }

        protected override Objekt KreirajEntitet(ObjektDTOInsertUpdate dto)
        {
            var vrsta = _context.Vrste.Find(dto.vrstaSifra) ?? throw new Exception("Ne postoji vrsta s šifrom " + dto.vrstaSifra + " u bazi");
            var entitet = _mapper.MapInsertUpdatedFromDTO(dto);           
            entitet.Vrsta = vrsta;
            return entitet;
        }

        protected override Objekt PromjeniEntitet(ObjektDTOInsertUpdate dto, Objekt entitet)
        {
            var vrsta = _context.Vrste.Find(dto.vrstaSifra) ?? throw new Exception("Ne postoji vrsta s šifrom " + dto.vrstaSifra + " u bazi");
            
            entitet.Mjesto = dto.mjesto;
            entitet.Adresa = dto.adresa;
            entitet.Vrsta = vrsta;

            return entitet;
        }

    }
}


