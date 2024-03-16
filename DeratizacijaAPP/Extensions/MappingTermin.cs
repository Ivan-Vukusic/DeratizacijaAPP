using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class MappingTermin
    {
        public static List<TerminDTORead> MapTerminReadList(this List<Termin> lista)
        {
            var mapper = TerminMapper.InicijalizirajReadToDTO();
            var vrati = new List<TerminDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<TerminDTORead>(e));
            });
            return vrati;
        }

        public static TerminDTORead MapTerminReadToDTO(this Termin entitet)
        {
            var mapper = TerminMapper.InicijalizirajReadToDTO();
            return mapper.Map<TerminDTORead>(entitet);
        }

        public static TerminDTOInsertUpdate MapTerminInsertUpdatedToDTO(this Termin entitet)
        {
            var mapper = TerminMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<TerminDTOInsertUpdate>(entitet);
        }

        public static Termin MapTerminInsertUpdateFromDTO(this TerminDTOInsertUpdate dto, Termin entitet)
        {
            entitet.Datum = dto.datum;
            entitet.Napomena = dto.napomena;                       
            return entitet;
        }
    }
}
