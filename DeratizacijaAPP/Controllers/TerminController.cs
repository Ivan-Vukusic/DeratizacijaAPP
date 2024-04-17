using Microsoft.EntityFrameworkCore;
using DeratizacijaAPP.Data;
using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace DeratizacijaAPP.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad entitetom Termin u bazi
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TerminController : DeratizacijaController<Termin, TerminDTORead, TerminDTOInsertUpdate>
    {

        public TerminController(DeratizacijaContext context) : base(context)
        {
            DbSet = _context.Termini;
            _mapper = new MappingTermin();
        }

        protected override void KontrolaBrisanje(Termin entitet)
        {
            var lista = _context.Termini.Include(x => x.Objekt).Where(x => x.Objekt.Sifra == entitet.Sifra)                
                .ToList();

            if (lista != null && lista.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Termin se ne može obrisati jer sadrži objekt: ");
                foreach (var e in lista)
                {
                    sb.Append(e.Objekt).Append(", ");
                }
                throw new Exception(sb.ToString().Substring(0, sb.ToString().Length - 2));
            }
        }

        protected override List<TerminDTORead> UcitajSve()
        {
            var lista = _context.Termini
                    .Include(t => t.Djelatnik)
                    .Include(t => t.Objekt)
                    .Include(t => t.Otrov)
                    .ToList();

            if (lista == null || lista.Count == 0)
            {
                throw new Exception("Ne postoje podaci u bazi");
            }
            return _mapper.MapReadList(lista);
        }

        protected override Termin NadiEntitet(int sifra)
        {
            return _context.Termini.Include(i => i.Djelatnik).Include(i => i.Objekt).Include(i => i.Otrov)
                .FirstOrDefault(x => x.Sifra == sifra) ?? throw new Exception("Ne postoji termin s šifrom " + sifra + " u bazi");
        }

        protected override Termin KreirajEntitet(TerminDTOInsertUpdate dto)
        {
            var djelatnik = _context.Djelatnici.Find(dto.djelatnikSifra) ?? throw new Exception("Ne postoji djelatnik s šifrom " + dto.djelatnikSifra + " u bazi");
            var objekt = _context.Objekti.Find(dto.objektSifra) ?? throw new Exception("Ne postoji objekt s šifrom " + dto.objektSifra + " u bazi");
            var otrov = _context.Otrovi.Find(dto.otrovSifra) ?? throw new Exception("Ne postoji otrov s šifrom " + dto.otrovSifra + " u bazi");            
            var entitet = _mapper.MapInsertUpdatedFromDTO(dto);

            entitet.Djelatnik = djelatnik;
            entitet.Objekt = objekt;
            entitet.Otrov = otrov;            
            return entitet;
        }

        protected override Termin PromjeniEntitet(TerminDTOInsertUpdate dto, Termin entitet)
        {
            var djelatnik = _context.Djelatnici.Find(dto.djelatnikSifra) ?? throw new Exception("Ne postoji djelatnik s šifrom " + dto.djelatnikSifra + " u bazi");            
            var objekt = _context.Objekti.Find(dto.objektSifra) ?? throw new Exception("Ne postoji objekt s šifrom " + dto.objektSifra + " u bazi");
            var otrov = _context.Otrovi.Find(dto.otrovSifra) ?? throw new Exception("Ne postoji otrov s šifrom " + dto.otrovSifra + " u bazi");

            entitet.Datum = dto.datum;
            entitet.Djelatnik = djelatnik;            
            entitet.Objekt = objekt;
            entitet.Otrov = otrov;
            entitet.Napomena = dto.napomena;

            return entitet;
        }

    }
}


