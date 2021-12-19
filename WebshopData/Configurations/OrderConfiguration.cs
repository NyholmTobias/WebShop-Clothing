using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebshopShared.Models;

namespace WebshopData.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder
                .HasKey(order => order.OrderId);

            modelBuilder
                .HasMany(order => order.LineItems)
                .WithOne(lineItem => lineItem.Order)
                .HasForeignKey(lineItem => lineItem.OrderId);
        }
    }
}
