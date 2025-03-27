using Microsoft.EntityFrameworkCore;

namespace project.Models
{
    public class ChinookDbContext : DbContext
    {
        public ChinookDbContext(DbContextOptions<ChinookDbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasMany(a => a.Tracks)
                .WithOne(t => t.Album)
                .HasForeignKey(t => t.AlbumId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Artist>()
                .HasMany(a => a.Albums)
                .WithOne(a => a.Artist)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
