using System.ComponentModel.DataAnnotations;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Otrov : Entitet
    {
        /// <summary>
        /// Naziv u bazi
        /// </summary>        
        public string? Naziv { get; set; }

        /// <summary>
        /// Vrsta aktivne tvari u otrovu
        /// </summary>        
        public string? AktivnaTvar { get; set; }

        /// <summary>
        /// Količina korištenog otrova
        /// </summary>
        public decimal? Kolicina { get; set; }

        /// <summary>
        /// Registarski broj otrova
        /// </summary>
        public string? CasBroj { get; set; }        
    }
}
