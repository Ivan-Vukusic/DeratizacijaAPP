using System.ComponentModel.DataAnnotations;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Vrsta : Entitet
    {
        /// <summary>
        /// Naziv u bazi
        /// </summary>        
        public string? Naziv { get; set; }
    }
}
