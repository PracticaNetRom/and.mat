using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace API.DataAccess
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryId);

            // Table & Column Mappings
            this.ToTable("Category");
            this.Property(t => t.CategoryId).HasColumnName("CategoryId").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name).HasColumnName("Name").IsRequired().HasMaxLength(32);



        }
    }
}