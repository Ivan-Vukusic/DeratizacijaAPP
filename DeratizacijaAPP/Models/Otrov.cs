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
        [Required(ErrorMessage = "Naziv obavezan")]
        public string? Naziv { get; set; }

        /// <summary>
        /// Vrsta aktivne tvari u otrovu
        /// </summary>
        [Required(ErrorMessage = "Aktivna tvar obavezna")]
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
