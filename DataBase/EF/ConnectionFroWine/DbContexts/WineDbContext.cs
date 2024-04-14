using Core.Models.WineRealizations;
using Microsoft.EntityFrameworkCore;

namespace DataBase.EF.ConnectionFroWine.DbContexts
{
    public class WineDbContext : DbContext
    {
        public DbSet<GrapeVariety> GrapeVarieties { get; set; } = null!;

        public DbSet<AreometrDefaultValue> AreometrDefaultValues { get; set; } = null!;

        public DbSet<WineReferenceInfo> WineReferenceInformations { get; set; } = null!;

        public DbSet<WineTypicalEvent> WineTypicalEvents { get; set; } = null!;

        public DbSet<WineEvent> WineEvents { get; set; } = null!;

        public DbSet<WineDay> WineDays { get; set; } = null!;

        public DbSet<WineIndicator> WineIndicators { get; set; } = null!;

        public DbSet<WineTimeLine> WineTimeLines { get; set; } = null!;

        public DbSet<WineUser> WineUsers { get; set; } = null!;

        public DbSet<WineAdministrator> WineAdministrators { get; set; } = null!;

        public DbSet<DifferenceAreometrDefaultValue> DifferenceAreometrDefaultValues { get; set; } = null!;

        public WineDbContext()
        {
            Database.EnsureCreated();
        }

        public WineDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WIWDB;Username=postgres;Password=1749;");
        }
    }
}
