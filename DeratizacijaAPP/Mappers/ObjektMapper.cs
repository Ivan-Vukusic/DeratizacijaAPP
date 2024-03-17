using AutoMapper;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Mappers
{
    public class ObjektMapper
    {
        public static Mapper InicijalizirajReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Objekt, ObjektDTORead>()
                    .ConstructUsing(entitet =>
                    new ObjektDTORead(
                        entitet.Sifra,
                        entitet.Mjesto,
                        entitet.Adresa,
                        entitet.Vrsta == null ? "" : entitet.Vrsta.Naziv
                        ));
                })
                );
        }

        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Objekt, ObjektDTOInsertUpdate>()
                    .ConstructUsing(entitet =>
                    new ObjektDTOInsertUpdate(
                        entitet.Mjesto,
                        entitet.Adresa,
                        entitet.Vrsta == null ? null : entitet.Vrsta.Sifra
                        ));
                })
                );
        }
    }
}
