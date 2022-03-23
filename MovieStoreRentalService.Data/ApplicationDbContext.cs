﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieStoreRentalService.Data.Models;

namespace MovieStoreRentalService.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public new DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Addresses> Addresses { get; set; }

        public DbSet<Rentals> Rentals { get; set; }

        public DbSet<UserRentals> UsersRentals { get; set; }

        public DbSet<ShoppingCarts> ShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString,
                b => b.MigrationsAssembly("MovieStoreRentalService"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRentals>()
                .HasKey(m => new { m.UserId, m.RentalId });

            modelBuilder.Entity<ShoppingCartsRentals>()
                .HasKey(m => new { m.ShoppingCartsId, m.RentalsId });

            base.OnModelCreating(modelBuilder);
        }
    }
}