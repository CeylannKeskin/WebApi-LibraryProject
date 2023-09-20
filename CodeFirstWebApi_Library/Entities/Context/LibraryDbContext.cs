using CodeFirstWebApi_Library.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeFirstWebApi_Library.Entities.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //tekseferde config tabloslarını eklemekiçin uzun uzun yazmaya gerek yok cınımmm:)
            //propgram.cs te builder.services ayarlarını yap
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
