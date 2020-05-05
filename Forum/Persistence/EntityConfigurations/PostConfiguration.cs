using System.Data.Entity.ModelConfiguration;
using Forum.Core.Domain;

namespace Forum.Persistence.EntityConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            Property(p => p.Title).IsRequired().HasMaxLength(255);
            Property(p => p.Text).IsRequired().HasColumnType("text").HasMaxLength(10000);
            Property(p => p.DateCreated).IsRequired();
            Property(p => p.AuthorId).IsRequired();
        }
    }
}