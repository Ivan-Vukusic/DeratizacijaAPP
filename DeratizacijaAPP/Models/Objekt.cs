using System.ComponentModel.DataAnnotations.Schema;
namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Objekt : Entitet
    {
        /// <summary>
        /// Mjesto u kojem se nalazi objekt
        /// </summary>
        public string? Mjesto { get; set; }

        /// <summary>
        /// Adresa objekta
        /// </summary>
        public string? Adresa { get; set; }

        /// <summary>
        /// Vrsta objekta
        /// </summary>
        [ForeignKey("vrsta")]
        public Vrsta? Vrsta { get; set; }        
    }
}
