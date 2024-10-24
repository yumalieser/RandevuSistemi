﻿using Microsoft.EntityFrameworkCore;

namespace RandevuSistemi.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=ACER-NITRO-ALI\\MSSQLSERVER01; database=RandevuDB; integrated security=true; TrustServerCertificate=True;");
        }

        public DbSet<Randevu> Randevular {  get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
    }
}
