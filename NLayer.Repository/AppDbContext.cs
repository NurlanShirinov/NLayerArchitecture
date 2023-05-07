using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Repository.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Assembly-e muraciet et hansi ki execute etdiyim assembly-lere. onlarda Configuration folderinde yazdiqlarimdir. her birin ishledecek
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());    //Yalniz bir configuration ishletmek isdedikde bu formada yazilir. lakin configurationlarim coxlu olduguna gore yuxaridaki kod daha best practice dir.
            base.OnModelCreating(modelBuilder);
        }
    }
}
