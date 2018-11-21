using Microsoft.EntityFrameworkCore;

namespace MovieApp.Model
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
        }

        #region DbSet
        public DbSet<Actor> Actors { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<MovieActorMapping> MovieActorMappings { get; set; }

        public DbSet<MovieProducerMapping> MovieProducerMappings { get; set; }
        #endregion DbSet

        #region ModelBuilder
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(a =>
            {
                a.ToTable("Actors");
            });

            modelBuilder.Entity<Producer>(p =>
            {
                p.ToTable("Producers");
            });

            modelBuilder.Entity<Movie>(m =>
            {
                m.ToTable("Movies");
            });

            modelBuilder.Entity<MovieActorMapping>(ma =>
            {
                ma.ToTable("MovieActorMappings");
            });

            modelBuilder.Entity<MovieProducerMapping>(mp =>
            {
                mp.ToTable("MovieProducerMappings");
            });
        }

        #endregion ModelBuilder
    }
}
