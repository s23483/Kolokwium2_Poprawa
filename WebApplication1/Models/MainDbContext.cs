using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
   public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        protected MainDbContext()
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<File>(a =>
            {
                a.HasKey(e => new {e.FileID, e.TeamID});
                a.Property(e => e.FileName).IsRequired().HasMaxLength(100);
                a.Property(e => e.FileExtension).IsRequired().HasMaxLength(4);
                a.Property(e => e.FileSize).IsRequired();

                a.HasOne(e => e.Team).WithMany(f => f.Files).HasForeignKey(e => e.TeamID);

                a.HasData(new File{});
            });

            modelBuilder.Entity<Member>(m =>
            {
                m.HasKey(e => new {e.MemberID, e.OrganizationID});
                m.Property(e => e.MemberName).IsRequired().HasMaxLength(20);
                m.Property(e => e.MemberNickName).IsRequired().HasMaxLength(20);
                m.Property(e => e.MemberSurname).IsRequired().HasMaxLength(50);
                m.HasOne(e => e.Organization).WithMany(e => e.Members).HasForeignKey(e => e.OrganizationID);
                m.HasData(new Member{});
            });

            modelBuilder.Entity<Membership>(t =>
            {
                t.HasKey(e => new {e.MemberID,e.TeamID});
                t.Property(e => e.MembershipDate).IsRequired();

                t.HasOne(e => e.Member).WithMany(e => e.Memberships).HasForeignKey(e => e.MemberID);
                t.HasOne(e => e.Team).WithMany(e => e.Memberships).HasForeignKey(e => e.TeamID);
                t.HasData(new Membership{});
            });

            modelBuilder.Entity<Organization>(m =>
            {
                m.HasKey(e => e.OrganizationID);
                m.Property(e => e.OrganizationName).IsRequired().HasMaxLength(100);
                m.Property(e => e.OrganizationDomain).IsRequired().HasMaxLength(50);
                

                m.HasData(new Organization{});
            });

            modelBuilder.Entity<Team>(m =>
            {
                m.HasKey(e => new {e.TeamID, e.OrganizationID});
                m.Property(e => e.TeamName).IsRequired().HasMaxLength(50);
                m.Property(e => e.TeamDescription).IsRequired(false).HasMaxLength(500);
                m.HasOne(e => e.Organization).WithMany(e => e.Teams).HasForeignKey(e => e.OrganizationID);

                m.HasData(new Team{});
            });
        }
    }
}