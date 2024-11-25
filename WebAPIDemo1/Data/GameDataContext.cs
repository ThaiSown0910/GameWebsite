using Microsoft.EntityFrameworkCore;

namespace WebAPIDemo1.Data
{
    public class GameDataContext : DbContext
    {
        public GameDataContext(DbContextOptions<GameDataContext> options):base(options)
        { 

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Login> Logins { get; set; } 
        public DbSet<AdminCreate> AdminCreates { get; set; } 
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thiết lập mối quan hệ giữa Movie và MovieCategory
            modelBuilder.Entity<Game>()
                .HasOne(m => m.GameCategory) 
                .WithMany(c => c.Games)       
                .HasForeignKey(m => m.categoryid);  


            modelBuilder.Entity<Login>()
               .HasOne(l => l.Register)
               .WithOne(r => r.Login)
               .HasForeignKey<Login>(l => l.customerid)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AdminLogin>()
              .HasOne(l => l.AdminCreate)
              .WithOne(r => r.AdminLogin)
              .HasForeignKey<AdminLogin>(l => l.adminid)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.cartid);

                entity.HasOne(e => e.Login)
                      .WithMany()
                      .HasForeignKey(e => e.customerid)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Game)
                      .WithMany()
                      .HasForeignKey(e => e.gameid)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Checkout>()
            .HasOne(c => c.Login)
            .WithMany()
            .HasForeignKey(c => c.customerid)
            .OnDelete(DeleteBehavior.Cascade);  // Or another behavior as required

        }
    }

   }
    

