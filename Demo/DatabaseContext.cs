using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Features> Features { get; set; }
    public DbSet<Substation> Substations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Features>().ToTable("Features");
        modelBuilder.Entity<Features>().HasKey(x => x.Id);
        modelBuilder.Entity<Features>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Substation>().ToTable("Substations");
        modelBuilder.Entity<Substation>().HasKey(x => x.Id);
        modelBuilder.Entity<Substation>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Substation>().HasMany(x => x.VoltageMeasurement).WithOne(x => x.Substation);


        modelBuilder.Entity<VoltageMeasurement>().ToTable("VoltageMeasurements");
        modelBuilder.Entity<VoltageMeasurement>().HasKey(x => x.Id);
        modelBuilder.Entity<VoltageMeasurement>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<VoltageMeasurement>().HasOne(x => x.Substation).WithMany(x => x.VoltageMeasurement);
    }
}