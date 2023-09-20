using CodeFirstWebApi_Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeFirstWebApi_Library.EntityTypeConfiguration
{
    public class BookGenreEntityTypeConfiguration:BaseEntityTypeConfiguration<BookGenre>
    {
        public override void Configure(EntityTypeBuilder<BookGenre> builder)
        {
            builder.HasOne(x => x.Book).WithMany(x => x.BookGenres).HasForeignKey(x => x.BookID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Genre).WithMany(x => x.BookGenres).HasForeignKey(x => x.GenreID).OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
