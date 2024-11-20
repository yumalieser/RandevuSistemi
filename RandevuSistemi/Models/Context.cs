using Microsoft.EntityFrameworkCore;

namespace RandevuSistemi.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost; database=RandevuDB; User Id=SA; password=reallyStrongPwd123; TrustServerCertificate=True;");
        }

        public DbSet<Randevu> Randevular {  get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
