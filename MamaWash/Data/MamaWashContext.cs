using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;

namespace MamaWash.Models
{
    public class MamaWashContext : DbContext
    {
        public MamaWashContext(DbContextOptions<MamaWashContext> options)
            : base(options)
        {
        }

        public DbSet<MamaWash.Models.BankList> BankList { get; set; }

        public DbSet<MamaWash.Models.Beneficiary> Beneficiaries { get; set; }

        public DbSet<MamaWash.Models.Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankList>().ToTable("BankList");
            modelBuilder.Entity<Beneficiary>().ToTable("Beneficiaries");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }

    }


}
