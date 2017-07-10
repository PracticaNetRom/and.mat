using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace API.DataAccess
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            this.HasKey(t => t.ReviewId);

            this.ToTable("Review");
            this.Property(t => t.ReviewId).HasColumnName("ReviewId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Rating).HasColumnName("Rating");
            this.Property(t => t.Comment).HasColumnName("Comment").IsRequired().HasMaxLength(512);
            this.Property(t => t.Username).HasColumnName("Username").IsRequired().HasMaxLength(64);
            this.Property(t => t.AnnouncementId).HasColumnName("AnnouncementId");
            this.Property(t => t.Postdate).HasColumnName("Postdate");
            this.HasRequired(t => t.Announcement)
                .WithMany(t => t.Reviews)
                .HasForeignKey(d => d.AnnouncementId);
        }
    }
}