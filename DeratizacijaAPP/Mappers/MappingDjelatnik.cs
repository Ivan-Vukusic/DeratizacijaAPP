using AutoMapper;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Mappers
{
    public class MappingDjelatnik : Mapping<Djelatnik, DjelatnikDTORead, DjelatnikDTOInsertUpdate>
    {

        public MappingDjelatnik()
        {
            MapperMapReadToDTO = new Mapper(
            new MapperConfiguration(c =>
            {
                c.CreateMap<Djelatnik, DjelatnikDTORead>()
                .ConstructUsing(entitet =>
                 new DjelatnikDTORead(
                    entitet.Sifra,
                    entitet.Ime,
                    entitet.Prezime,
                    entitet.BrojMobitela,
                    entitet.Oib,
                    entitet.Struka,
                    PutanjaDatoteke(entitet)));
            })
            );

            MapperMapInsertUpdateToDTO = new Mapper(
              new MapperConfiguration(c =>
              {
                  c.CreateMap<Djelatnik, DjelatnikDTOInsertUpdate>()
               .ConstructUsing(entitet =>
                new DjelatnikDTOInsertUpdate(
                   entitet.Ime,
                   entitet.Prezime,
                   entitet.BrojMobitela,
                   entitet.Oib,
                   entitet.Struka,
                   PutanjaDatoteke(entitet)));
              })
              );
        }


        private static string PutanjaDatoteke(Djelatnik e)
        {
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string slika = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "djelatnici" + ds + e.Sifra + ".png");
                return File.Exists(slika) ? "/slike/djelatnici/" + e.Sifra + ".png" : null;
            }
            catch
            {
                return null;
            }

        }



    }
}
