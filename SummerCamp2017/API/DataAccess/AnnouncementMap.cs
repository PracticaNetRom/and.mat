using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace API.DataAccess
{
    public class AnnouncementMap : EntityTypeConfiguration<Announcement>
    {

        public AnnouncementMap()
        {
            // Primary Key
            this.HasKey(t => t.AnnouncementId);

            // Table & Column Mappings
            this.ToTable("Announcement");
            this.Property(t => t.AnnouncementId).HasColumnName("AnnouncementId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.CategoryId).HasColumnName("CategoryId");
            this.Property(t => t.Phonenumber).HasColumnName("Phonenumber").IsRequired().HasMaxLength(64);
            this.Property(t => t.Email).HasColumnName("Email").IsRequired().HasMaxLength(32);
            this.Property(t => t.PostDate).HasColumnName("PostDate").IsRequired();
            this.Property(t => t.ExpirationDate).HasColumnName("ExpirationDate").IsRequired();
            this.Property(t => t.Description).HasColumnName("Description").IsRequired();
            this.Property(t => t.Title).HasColumnName("Title").IsRequired().HasMaxLength(128);
            this.Property(t => t.Closed).HasColumnName("Closed").IsRequired();

            // Relationships
            this.HasRequired(t => t.Category)
                .WithMany(t => t.Announcements)
                .HasForeignKey(d => d.CategoryId);
        }
    }
}
