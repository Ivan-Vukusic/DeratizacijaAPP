using System.ComponentModel.DataAnnotations.Schema;

namespace DeratizacijaAPP.Models
{
    /// <summary>
    /// Ovo mi je POCO koji je mapiran na bazu
    /// </summary>
    public class Termin : Entitet
    {
        /// <summary>
        /// Datum termina
        /// </summary>
        public DateTime? Datum { get; set; }

        /// <summary>
        /// Djelatnik koji obavlja deratizaciju
        /// </summary>
        [ForeignKey("djelatnik")]
        public Djelatnik? Djelatnik { get; set; }

        /// <summary>
        /// Objekt u kojem se obavlja deratizacija
        /// </summary>
        [ForeignKey("objekt")]
        public Objekt? Objekt { get; set; }

        /// <summary>
        /// Otrov koji se koristi
        /// </summary>
        [ForeignKey("otrov")]
        public Otrov? Otrov { get; set; }

        /// <summary>
        /// Napomena za dodatna zapažanja
        /// </summary>
        public string? Napomena { get; set; }
    }
}
