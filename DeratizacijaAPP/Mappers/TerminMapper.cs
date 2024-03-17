using AutoMapper;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Mappers
{
    public class TerminMapper
    {
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Termin, TerminDTORead>()
                .ConstructUsing(entitet =>
                 new TerminDTORead(
                    entitet.Sifra,
                    entitet.Datum,
                    entitet.Djelatnik == null ? "" : (entitet.Djelatnik.Ime + " " + entitet.Djelatnik.Prezime).Trim(),
                    entitet.Objekt == null ? "" : (entitet.Objekt.Mjesto
                        + ", " + entitet.Objekt.Adresa).Trim(),
                    entitet.Otrov == null ? "" : entitet.Otrov.Naziv,
                    entitet.Napomena));
                    
            })
            );
        }



        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
             new MapperConfiguration(c =>
             {
                 c.CreateMap<Termin, TerminDTOInsertUpdate>()
                 .ConstructUsing(entitet =>
                  new TerminDTOInsertUpdate(
                     entitet.Datum,
                     entitet.Djelatnik == null ? null : entitet.Djelatnik.Sifra,
                     entitet.Objekt == null ? null : entitet.Objekt.Sifra,
                     entitet.Otrov == null ? null : entitet.Otrov.Sifra,
                     entitet.Napomena));
             })
             );
        }
    }
}
