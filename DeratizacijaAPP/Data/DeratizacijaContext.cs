using DeratizacijaAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace DeratizacijaAPP.Data
{
    /// <summary>
    /// Ovo mi je datoteka gdje ću navoditi datasetove i načine spajanja u bazi
    /// </summary>
    public class DeratizacijaContext : DbContext
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="options"></param>
        public DeratizacijaContext(DbContextOptions<DeratizacijaContext> options)
            :base(options)
        {

        }

        /// <summary>
        /// Vrste u bazi
        /// </summary>
        public DbSet<Vrsta> Vrste { get; set; }

        /// <summary>
        /// Djelatnici u bazi
        /// </summary>
        public DbSet<Djelatnik> Djelatnici { get; set; }

        /// <summary>
        /// Otrovi u bazi
        /// </summary>
        public DbSet<Otrov> Otrovi { get; set; }

        /// <summary>
        /// Objekti u bazi
        /// </summary>
        public DbSet<Objekt> Objekti { get; set; }

        /// <summary>
        /// Termini u bazi
        /// </summary>
        public DbSet<Termin> Termini { get; set; }

        /// <summary>
        /// Povezivanje tablica 1:N
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objekt>().HasOne(o => o.Vrsta);
            modelBuilder.Entity<Termin>().HasOne(t => t.Djelatnik);
            modelBuilder.Entity<Termin>().HasOne(t => t.Objekt);
            modelBuilder.Entity<Termin>().HasOne(t => t.Otrov);
        }
    }
}
