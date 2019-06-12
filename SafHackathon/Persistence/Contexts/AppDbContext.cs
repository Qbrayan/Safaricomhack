using Microsoft.EntityFrameworkCore;
using SafHackathon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<Employee> employee { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Card>().ToTable("ScratchCards");
            builder.Entity<Card>().HasKey(p => p.VoucherNumber);
            builder.Entity<Card>().Property(p => p.VoucherNumber).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Card>().Property(p => p.SerialNumber).IsRequired().HasMaxLength(30);
            builder.Entity<Card>().Property(p => p.ExpiryDate).IsRequired();
            builder.Entity<Card>().Property(p => p.VoucherAmount).IsRequired();
            builder.Entity<Card>().Property(p => p.Status).IsRequired();
            builder.Entity<Card>().Property(p => p.DateCreated).IsRequired();
            builder.Entity<Card>().Property(p => p.DateUpdated).IsRequired();

            //builder.Entity<Card>().HasMany(p => p.Products).WithOne(p => p.Card).HasForeignKey(p => p.CardId);

            builder.Entity<Card>().HasData
            (
                new Card { VoucherNumber = 100, SerialNumber = "AB51QW34E",ExpiryDate = new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc), VoucherAmount=200m,
                   Status="Expired",DateCreated=new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc),DateUpdated=new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                }, // Id set manually due to in-memory provider
                new Card
                {
                    VoucherNumber = 101,
                    SerialNumber = "AB121QW34E",
                    ExpiryDate = new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                    VoucherAmount = 200m,
                    Status = "Active",
                    DateCreated = new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                    DateUpdated = new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            builder.Entity<Employee>().ToTable("Employees");
            builder.Entity<Employee>().HasKey(p => p.Id);
            builder.Entity<Employee>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Employee>().Property(p => p.employee_name).IsRequired();
            builder.Entity<Employee>().Property(p => p.employee_salary).IsRequired();
            builder.Entity<Employee>().Property(p => p.employee_age).IsRequired();
            builder.Entity<Employee>().Property(p => p.profile_image).IsRequired();

            //builder.Entity<Product>().ToTable("Products");
            //builder.Entity<Product>().HasKey(p => p.Id);
            //builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            //builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
            //builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();
        }
    }
}
