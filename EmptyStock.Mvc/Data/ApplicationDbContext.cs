using EmptyStock.Domain.Models.Identity;
using EmptyStock.Domain.Models.Stock;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmptyStock.Mvc.Data;
public class ApplicationDbContext : IdentityDbContext<StockUser, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAction> ProductActions { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Request> Requests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Shipment>()
            .HasOne(sh => sh.Request)
            .WithOne(r => r.Shipment)
            .HasForeignKey<Request>()
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Entity<Request>()
            .HasOne(r => r.Shipment)
            .WithOne(sh => sh.Request)
            .IsRequired(false)
            .HasForeignKey<Shipment>()
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(builder);
    }
}
