using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn(); // UseIdentityColumn(...) bosh buraxildiqda auto bir bir artirir
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.ToTable("Products");

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            //yuxaridaki kodda ilk olaraq (HasOne(x => x.Category)) bildirilib ki bir product-in 1 categorysi ola biler. 
            // WithMany(x => x.Products).HasForeignKey(x => x.CategoryId) - bu kodda ise bir category-nin coxlu productu ola biler ve CategoryID ise foreign key-dir.


        }
    }
}
