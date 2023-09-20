using CodeFirstWebApi_Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirstWebApi_Library.EntityTypeConfiguration
{
    public class AuthorBookEntityTypeConfiguration:BaseEntityTypeConfiguration<AuthorBook>
    {
        public override void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            //yazarla iliskisi
            builder.HasOne(x => x.Author).WithMany(x => x.AuthorBooks).HasForeignKey(x => x.AuthorID).OnDelete(DeleteBehavior.Restrict);

            //kitapla iliskisi
            builder.HasOne(x => x.Book).WithMany(x => x.AuthorBooks).HasForeignKey(x => x.BookID).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
