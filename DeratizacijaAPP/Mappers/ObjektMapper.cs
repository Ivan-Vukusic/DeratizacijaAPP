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
                    c.CreateMap<Objekt, ObjektDTORead>();
                })
                );
        }

        public static Mapper InicijalizirajReadFromDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<ObjektDTORead, Objekt>();
                })
                );
        }

        public static Mapper InicijalizirajInsertUpdateToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                {
                    c.CreateMap<Objekt, ObjektDTOInsertUpdate>();
                })
                );
        }
    }
}
