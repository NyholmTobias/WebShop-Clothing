using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopShared.Models;

namespace WebshopData.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> modelBuilder)
        {
            modelBuilder
                .HasKey(category => category.CategoryId);

            modelBuilder
                .HasMany(category => category.Items)
                .WithMany(item => item.Categories);
        }
    }
}
