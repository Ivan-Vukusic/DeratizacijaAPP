using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class MappingDjelatnik
    {
        public static List<DjelatnikDTORead> MapDjelatnikReadList(this List<Djelatnik> lista)
        {
            var mapper = DjelatnikMapper.InicijalizirajReadToDTO();
            var vrati = new List<DjelatnikDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<DjelatnikDTORead>(e));
            });
            return vrati;
        }

        public static DjelatnikDTORead MapDjelatnikReadToDTO(this Djelatnik entitet)
        {
            var mapper = DjelatnikMapper.InicijalizirajReadToDTO();
            return mapper.Map<DjelatnikDTORead>(entitet);
        }

        public static DjelatnikDTOInsertUpdate MapDjelatnikInsertUpdatedToDTO(this Djelatnik entitet)
        {
            var mapper = DjelatnikMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<DjelatnikDTOInsertUpdate>(entitet);
        }

        public static Djelatnik MapDjelatnikInsertUpdateFromDTO(this DjelatnikDTOInsertUpdate dto, Djelatnik entitet)
        {
            entitet.Ime = dto.ime;
            entitet.Prezime = dto.prezime;
            entitet.BrojMobitela = dto.brojMobitela;
            entitet.Oib = dto.oib;
            entitet.Struka = dto.struka;
            return entitet;
        }
    }
}
