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
        public Djelatnik? Djelatnik { get; set; }

        /// <summary>
        /// Objekt u kojem se obavlja deratizacija
        /// </summary>
        public Objekt? Objekt { get; set; }

        /// <summary>
        /// Otrov koji se koristi
        /// </summary>
        public Otrov? Otrov { get; set; }

        /// <summary>
        /// Napomena za dodatna zapažanja
        /// </summary>
        public string? Napomena { get; set; }
    }
}
