using System.Data.Entity;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryDB") { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public DbSet<Genre> Genres { get; set; }
    }
}