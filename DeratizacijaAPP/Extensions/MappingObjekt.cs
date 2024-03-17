using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class MappingObjekt
    {
        public static List<ObjektDTORead> MapObjektReadList(this List<Objekt> lista)
        {
            var mapper = ObjektMapper.InicijalizirajReadToDTO();
            var vrati = new List<ObjektDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<ObjektDTORead>(e));
            });
            return vrati;
        }

        public static ObjektDTORead MapObjektReadToDTO(this Objekt entitet)
        {
            var mapper = ObjektMapper.InicijalizirajReadToDTO();
            return mapper.Map<ObjektDTORead>(entitet);
        }

        public static ObjektDTOInsertUpdate MapObjektInsertUpdatedToDTO(this Objekt entitet)
        {
            var mapper = ObjektMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<ObjektDTOInsertUpdate>(entitet);
        }

        public static Objekt MapObjektInsertUpdateFromDTO(this ObjektDTOInsertUpdate dto, Objekt entitet)
        {
            entitet.Mjesto = dto.mjesto;
            entitet.Adresa = dto.adresa;                                   
            return entitet;
        }
    }
}
