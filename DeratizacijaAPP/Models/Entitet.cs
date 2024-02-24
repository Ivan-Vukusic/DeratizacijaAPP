using System.ComponentModel.DataAnnotations;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo je najgornja klasa koja služi za osnovne atribute
    /// tipa sifra, operater, datum unosa, promjene itd.
    /// </summary>
    public abstract class Entitet
    {
        /// <summary>
        /// Ovo svojstvo mi služi kao primarni ključ u bazi s
        /// generiranjem vrijednosti identity(1,1)
        /// </summary>
        [Key]
        public int Sifra { get; set; }
    }
}
