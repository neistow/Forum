using System.Data.Entity.ModelConfiguration;
using Forum.Core.Domain;

namespace Forum.Persistence.EntityConfigurations
{
    public class ReplyConfiguration : EntityTypeConfiguration<Reply>
    {
        public ReplyConfiguration()
        {
            Property(r => r.Text).IsRequired().HasColumnType("text").HasMaxLength(2500);
            Property(r => r.DateCreated).IsRequired();
            Property(r => r.AuthorId).IsRequired();
        }
    }
}