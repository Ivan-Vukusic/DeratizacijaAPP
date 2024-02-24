using System.ComponentModel.DataAnnotations;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Djelatnik : Entitet
    {
        /// <summary>
        /// Ime djelatnika
        /// </summary>
        [Required(ErrorMessage = "Ime obavezno")]
        public string? Ime { get; set; }

        /// <summary>
        /// Prezime djelatnika
        /// </summary>
        [Required(ErrorMessage = "Prezime obavezno")]
        public string? Prezime { get; set; }

        /// <summary>
        /// Broj mobitela djelatnika
        /// </summary>
        public string? BrojMobitela { get; set; }

        /// <summary>
        /// Oib djelatnika
        /// </summary>
        [StringLength(11, MinimumLength = 11, ErrorMessage ="Neispravan unos! OIB mora imati 11 znamenki")]
        public string? Oib { get; set; }

        /// <summary>
        /// Stručna sprema djelatnika
        /// </summary>
        public string? Struka { get; set; }
    }
}
