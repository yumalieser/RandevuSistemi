using Microsoft.EntityFrameworkCore;
using RandevuSistemi.Models;

namespace RandevuSistemi.Data
{
    public class RandevuDBContext : DbContext
    {
        public RandevuDBContext(DbContextOptions<RandevuDBContext> options) : base(options)
        {
        }

        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<AlinanRandevu> AlinanRandevular { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<RandevuTur> RandevuTurleri { get; set; }

    }
}
