using CodeFirstWebApi_Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirstWebApi_Library.EntityTypeConfiguration
{
    public class BookEntityTypeConfiguration:BaseEntityTypeConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            base.Configure(builder);
        }
    }
}
