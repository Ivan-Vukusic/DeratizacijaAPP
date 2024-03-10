using DeratizacijaAPP.Mappers;
using DeratizacijaAPP.Models;

namespace DeratizacijaAPP.Extensions
{
    public static class Mapping
    {
        public static List<VrstaDTORead> MapVrstaReadList(this List<Vrsta> lista)
        {
            var mapper = VrstaMapper.InicijalizirajReadToDTO();
            var vrati = new List<VrstaDTORead>();
            lista.ForEach(e => 
            {
                vrati.Add(mapper.Map<VrstaDTORead>(e));
            });
            return vrati;
        }

        public static VrstaDTORead MapVrstaReadToDTO(this Vrsta entitet)
        {
            var mapper = VrstaMapper.InicijalizirajReadToDTO();            
            return mapper.Map<VrstaDTORead>(entitet);
        }

        public static Vrsta MapVrstaInsertUpdateToDTO(this VrstaDTOInsertUpdate entitet)
        {
            var mapper = VrstaMapper.InicijalizirajInsertUpdateFromDTO();
            return mapper.Map<Vrsta>(entitet);
        }

        public static Vrsta MapVrstaInsertUpdateFromDTO(this VrstaDTOInsertUpdate entitet)
        {
            var mapper = VrstaMapper.InicijalizirajInsertUpdateFromDTO();
            return mapper.Map<Vrsta>(entitet);
        } 
    }
}
