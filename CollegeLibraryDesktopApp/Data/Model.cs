using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CollegeLibraryDesktopApp.Data.Model
{
    public partial class Model : DbContext
    {
        public Model()
            : base("name=DataModel")
        {
        }

        private static Model _context;

        public static Model GetContext()
        {
            if(_context == null)
                _context = new Model();
            return _context;
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<AuthorsInBook> AuthorsInBooks { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<PublishingCompany> PublishingCompanies { get; set; }
        public virtual DbSet<ReservedBook> ReservedBooks { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Author>()
                .HasMany(e => e.AuthorsInBooks)
                .WithRequired(e => e.Author)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.ISBNCode)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.AuthorsInBooks)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                .HasMany(e => e.Stocks)
                .WithRequired(e => e.Book)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.ColorHEX)
                .IsUnicode(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Books)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PublishingCompany>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingCompany>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingCompany>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<PublishingCompany>()
                .HasMany(e => e.Books)
                .WithOptional(e => e.PublishingCompany)
                .HasForeignKey(e => e.PublishingId);

            modelBuilder.Entity<Role>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Descripton)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.ReservedBooks)
                .WithRequired(e => e.Stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Patronymic)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.HashPassword)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ReservedBooks)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
