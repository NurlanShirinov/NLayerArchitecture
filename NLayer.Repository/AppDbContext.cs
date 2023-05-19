using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
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




            //ProductFeatyre üçün yazılan bu kodu Seeds folderinin daxilində digər CatergorySeed və ProductSeed
            //kimi də ayrı klassda yaza bilərdik bu formada yazildigini gotmek ucun burda yazdiq.

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kirmizi",
                Height = 100,
                Width = 200,
                ProductId = 1
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
