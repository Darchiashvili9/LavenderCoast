#nullable disable
using Microsoft.EntityFrameworkCore;
using LearnMathRu_0._1.Models;

public class LavandaDB : DbContext
{
    public LavandaDB(DbContextOptions<LavandaDB> options)
        : base(options)
    {
    }

    public DbSet<LearnMathRu_0._1.Models.Customer> Customer { get; set; }

    public DbSet<LearnMathRu_0._1.Models.Order> Order { get; set; }

    public DbSet<LearnMathRu_0._1.Models.Product> Product { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //    optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;Database=lavandaDbMigrations1.0;trusted_connection=true");
        optionsBuilder.UseSqlServer("Data Source=37.140.192.136;Initial Catalog=u1691672_LavenderCoast;Persist Security Info=True;User ID=u1691672_lavand;Password=999plmPLM(");
    }






    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Customer>().ToTable("CUSTOMER");
    //    modelBuilder.Entity<Order>().ToTable("ORDER");
    //    modelBuilder.Entity<Product>().ToTable("PRODUCT");


    //    //    modelBuilder.Entity<Product>()
    //    //.HasDiscriminator<string>("Product_type")
    //    //.HasValue<Product>("Product_base")
    //    //.HasValue<VisitOnSite>("Product_Visit");

    //}
}
