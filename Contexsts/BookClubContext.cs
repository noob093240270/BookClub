using BookClub.Models;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Contexsts
{
    public class BookClubContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public BookClubContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  //DbContextOptionsBuilder устанавливает параметры подключения
        {
            optionsBuilder.UseSqlite("Data Source=bookclub.db");
        }

    }
}
