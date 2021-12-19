using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebshopShared.Models;

namespace WebshopData.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> modelBuilder)
        {
            modelBuilder
                .HasKey(item => item.ItemId);

            modelBuilder
                .HasMany(item => item.LineItems)
                .WithOne(lineItem => lineItem.Item)
                .HasForeignKey(lineItem => lineItem.ItemId);

            modelBuilder
                .HasMany(item => item.Categories)
                .WithMany(category => category.Items);
        }
    }
}
