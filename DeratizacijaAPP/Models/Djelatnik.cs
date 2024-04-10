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
        public string? Ime { get; set; }

        /// <summary>
        /// Prezime djelatnika
        /// </summary>        
        public string? Prezime { get; set; }

        /// <summary>
        /// Broj mobitela djelatnika
        /// </summary>
        public string? BrojMobitela { get; set; }

        /// <summary>
        /// Oib djelatnika
        /// </summary>        
        public string? Oib { get; set; }

        /// <summary>
        /// Stručna sprema djelatnika
        /// </summary>
        public string? Struka { get; set; }        
    }
}
