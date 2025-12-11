using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(P => P.Id);
            builder.Property(P=>P.Title).IsRequired().HasMaxLength(255);
            builder.Property(P => P.Content).IsRequired();
            builder.Property(P => P.ImageUrl).IsRequired();
            builder.Property(P => P.CreatedAt).IsRequired();
            builder.Property(P => P.ModifiedAt).IsRequired();
        }
    }
}
