using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class MappingVrsta
    {
        public static List<VrstaDTORead> MapVrstaReadList(this List<Vrsta> lista)
        {
            var mapper = VrstaMapper.InicijalizirajReadToDTO();
            var vrati = new List<VrstaDTORead>();
            lista.ForEach(e => {
                vrati.Add(mapper.Map<VrstaDTORead>(e));
            });
            return vrati;
        }

        public static VrstaDTORead MapVrstaReadToDTO(this Vrsta entitet)
        {
            var mapper = VrstaMapper.InicijalizirajReadToDTO();
            return mapper.Map<VrstaDTORead>(entitet);
        }

        public static VrstaDTOInsertUpdate MapVrstaInsertUpdatedToDTO(this Vrsta entitet)
        {
            var mapper = VrstaMapper.InicijalizirajInsertUpdateToDTO();
            return mapper.Map<VrstaDTOInsertUpdate>(entitet);
        }

        public static Vrsta MapVrstaInsertUpdateFromDTO(this VrstaDTOInsertUpdate dto, Vrsta entitet)
        {
            entitet.Naziv = dto.naziv;
            
            return entitet;
        }
    }
}
