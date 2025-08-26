using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Action", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "History", DisplayOrder = 3 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { 
                    Id= 1, 
                    Name="Test",
                    NewPrice = 65,
                    OldPrice = 60,
                    CategoryId = 1
                },
                new Product {
                    Id = 2,
                    Name = "Test1",
                    NewPrice = 70,
                    OldPrice = 75,
                    CategoryId = 2
                },
                new Product {
                    Id = 3,
                    Name = "Test2",
                    NewPrice = 60,
                    OldPrice = 55,
                    CategoryId = 3
                }
                );


        }

    }
}
