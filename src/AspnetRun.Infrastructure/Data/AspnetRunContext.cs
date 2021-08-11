using AspnetRun.Application.Models.Base;
using AspnetRun.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace AspnetRun.Infrastructure.Data
{
    public class AspnetRunContext : DbContext
    {
    
        public AspnetRunContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(ConfigureProduct);
            builder.Entity<Category>(ConfigureCategory);
        }

        public override int SaveChanges()
        {
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                // try and convert to an Auditable Entity
                var entity = entry.Entity as BaseModel;
                // call PrepareSave on the entity, telling it the state it is in
                entity?.PrepareSave(entry.State);
            }

            var result = base.SaveChanges();
            return result;
        }

        private void ConfigureProduct(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(ci => ci.Id);         

            builder.Property(cb => cb.ProductName)
                .IsRequired()
                .HasMaxLength(100);


            builder.HasOne<Category>(Category => Category.Category)
                .WithMany(product => product.Products)
                .HasForeignKey(product => product.CategoryId);
        }

        private void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(ci => ci.Id);            

            builder.Property(cb => cb.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
