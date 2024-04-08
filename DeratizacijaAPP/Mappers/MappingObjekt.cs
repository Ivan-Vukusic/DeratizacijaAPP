using AutoMapper;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Mappers
{
    public class MappingObjekt : Mapping<Objekt, ObjektDTORead, ObjektDTOInsertUpdate>
    {
        public MappingObjekt()
        {
            MapperMapReadToDTO = new Mapper(
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


            MapperMapInsertUpdatedFromDTO = new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<ObjektDTOInsertUpdate, Objekt>();
                })
                );

            MapperMapInsertUpdateToDTO = new Mapper(
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
