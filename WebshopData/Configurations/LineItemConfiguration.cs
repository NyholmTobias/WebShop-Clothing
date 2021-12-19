using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebshopShared.Models;

namespace WebshopData.Configurations
{
    public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> modelBuilder)
        {
            modelBuilder
                .HasKey(li => new { li.ItemId, li.OrderId });

            modelBuilder
                .HasOne(lineItem => lineItem.Order)
                .WithMany(order => order.LineItems);

            modelBuilder
                .HasOne(lineItem => lineItem.Item)
                .WithMany(items => items.LineItems);
        }
    }
}
