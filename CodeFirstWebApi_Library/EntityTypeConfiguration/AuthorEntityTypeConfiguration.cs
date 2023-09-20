using CodeFirstWebApi_Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirstWebApi_Library.EntityTypeConfiguration
{
    public class AuthorEntityTypeConfiguration:BaseEntityTypeConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            base.Configure(builder);
        }
    }
}
