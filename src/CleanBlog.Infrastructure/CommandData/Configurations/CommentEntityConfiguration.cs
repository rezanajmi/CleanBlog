using CleanBlog.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanBlog.Infrastructure.CommandData.Configurations
{
    internal class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.Text)
                .HasMaxLength(5000)
                .IsRequired();

            builder.HasOne(e => e.Post)
                .WithMany(p => p.Comments);
        }
    }
}
