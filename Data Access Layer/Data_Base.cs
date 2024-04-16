using BusinessObjects.Authentication;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data_Access_Layer
{
    public class Data_Base: IdentityDbContext<ApplicationUser>
    {
        public Data_Base(DbContextOptions<Data_Base> options):base(options) {
            
        }
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Branch> Branches { get; set; } = null!;
        public DbSet<Detail> Details { get; set; } = null!;
        public DbSet<Photo> Photos { get; set; } = null!;
        public DbSet<CarInBranch> CarInBranch { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CarInBranch>()

                      .HasKey(CB => new { CB.CarId, CB.BranchId });
            ////builder.Entity<Detail>()

            ////          .HasKey(CB => new {  CB.CarId});

        }
       

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json")
        //            .Build();
        //        var connectionString = configuration.GetConnectionString("ConnStr");
        //        optionsBuilder.UseSqlServer(connectionString);
        //    }
        //}

    }
}
