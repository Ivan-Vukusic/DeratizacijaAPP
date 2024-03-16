using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class MappingOtrov
    {
        public static List<OtrovDTORead> MapOtrovReadList(this List<Otrov> lista)
        {
            var mapper = OtrovMapper.InicijalizirajReadToDTO();
            var vrati = new List<OtrovDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<OtrovDTORead>(e));
            });
            return vrati;
        }

        public static OtrovDTORead MapOtrovReadToDTO(this Otrov entitet)
        {
            var mapper = OtrovMapper.InicijalizirajReadToDTO();
            return mapper.Map<OtrovDTORead>(entitet);
        }

        public static OtrovDTOInsertUpdate MapOtrovInsertUpdatedToDTO(this Otrov entitet)
        {
            var mapper = OtrovMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<OtrovDTOInsertUpdate>(entitet);
        }

        public static Otrov MapOtrovInsertUpdateFromDTO(this OtrovDTOInsertUpdate dto, Otrov entitet)
        {
            entitet.Naziv = dto.naziv;
            entitet.AktivnaTvar = dto.aktivnaTvar;
            entitet.Kolicina = dto.kolicina;
            entitet.CasBroj = dto.casBroj;            
            return entitet;
        }
    }
}
