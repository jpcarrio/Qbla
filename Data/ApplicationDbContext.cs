using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Qbla.Models;

namespace Qbla.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Qbla.Models.Customers> Customers { get; set; }
        public DbSet<Qbla.Models.Payments> Payments { get; set; }
        public DbSet<Qbla.Models.Services> Services { get; set; }
        public DbSet<Qbla.Models.PaymentView> PaymentView { get; set; }
    }
}
