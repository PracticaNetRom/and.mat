using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.DataAccess
{
    public class SummerCampDbContext : DbContext
    {
        static SummerCampDbContext()
        {
            Database.SetInitializer<SummerCampDbContext>(null);
        }

        public SummerCampDbContext()
            : base("Name=SummerCampDbContext")
        {
        }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AnnouncementMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ReviewMap());
        }
    }
}