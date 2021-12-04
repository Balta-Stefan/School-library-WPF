using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace School_library.Models
{
    public partial class mydbContext : DbContext
    {
        private readonly string connectionString;
        public mydbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accountant> Accountants { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCondition> BookConditions { get; set; }
        public virtual DbSet<BookCopy> BookCopies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Librarian> Librarians { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accountant>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("accountants");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Accountant)
                    .HasForeignKey<Accountant>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Accountants_userID_FK");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors");

                entity.Property(e => e.AuthorId).HasColumnName("authorID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("lastName");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.Isbn10, "ISBN10_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Isbn13, "ISBN13_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.AuthorId, "authorID_idx");

                entity.HasIndex(e => e.Genre, "genre_idx");

                entity.HasIndex(e => e.PublisherId, "publisherID_idx");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.AuthorId).HasColumnName("authorID");

                entity.Property(e => e.BookTitle)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("bookTitle");

                entity.Property(e => e.Edition).HasColumnName("edition");

                entity.Property(e => e.Genre).HasColumnName("genre");

                entity.Property(e => e.Isbn10)
                    .HasMaxLength(10)
                    .HasColumnName("ISBN10")
                    .IsFixedLength(true);

                entity.Property(e => e.Isbn13)
                    .HasMaxLength(13)
                    .HasColumnName("ISBN13")
                    .IsFixedLength(true);

                entity.Property(e => e.NumberOfCopies)
                    .HasColumnName("numberOfCopies")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PublisherId).HasColumnName("publisherID");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("authorID");

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Genre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("genre");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("publisherID");
            });

            modelBuilder.Entity<BookCondition>(entity =>
            {
                entity.HasKey(e => e.ConditionId)
                    .HasName("PRIMARY");

                entity.ToTable("book_conditions");

                entity.Property(e => e.ConditionId).HasColumnName("conditionID");

                entity.Property(e => e.Condition)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("condition");
            });

            modelBuilder.Entity<BookCopy>(entity =>
            {
                entity.ToTable("book_copies");

                entity.HasIndex(e => e.BookId, "bookID_idx");

                entity.HasIndex(e => e.ConditionId, "conditionID_idx");

                entity.Property(e => e.BookCopyId).HasColumnName("bookCopyID");

                entity.Property(e => e.Available)
                    .HasColumnType("tinyint")
                    .HasColumnName("available")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.BookId).HasColumnName("bookID");

                entity.Property(e => e.ConditionId).HasColumnName("conditionID");

                entity.Property(e => e.DeliveredAt).HasColumnName("deliveredAt");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookID");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.BookCopies)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("conditionID");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genres");

                entity.Property(e => e.GenreId).HasColumnName("genreID");

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("genreName");
            });

            modelBuilder.Entity<Librarian>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("librarians");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Librarian)
                    .HasForeignKey<Librarian>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Librarians_userID_FK");
            });

            modelBuilder.Entity<Loan>(entity =>
            {
                entity.ToTable("loans");

                entity.HasIndex(e => e.BookCopyId, "bookCopyID_idx");

                entity.HasIndex(e => e.BorrowedFromLibrarian, "borrowedFromLibrarian_idx");

                entity.HasIndex(e => e.BorrowerId, "borrowerID_idx");

                entity.HasIndex(e => e.ReturnedToLibrarian, "returnedToLibrarian_idx");

                entity.Property(e => e.LoanId).HasColumnName("loanID");

                entity.Property(e => e.BookCopyId).HasColumnName("bookCopyID");

                entity.Property(e => e.BorrowDateTime).HasColumnName("borrowDateTime");

                entity.Property(e => e.BorrowedFromLibrarian).HasColumnName("borrowedFromLibrarian");

                entity.Property(e => e.BorrowerId).HasColumnName("borrowerID");

                entity.Property(e => e.ReturnDateTime).HasColumnName("returnDateTime");

                entity.Property(e => e.ReturnedToLibrarian).HasColumnName("returnedToLibrarian");

                entity.HasOne(d => d.BookCopy)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.BookCopyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("bookCopyID");

                entity.HasOne(d => d.BorrowedFromLibrarianNavigation)
                    .WithMany(p => p.LoanBorrowedFromLibrarianNavigations)
                    .HasForeignKey(d => d.BorrowedFromLibrarian)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("borrowedFromLibrarian");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.Loans)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("borrowerID");

                entity.HasOne(d => d.ReturnedToLibrarianNavigation)
                    .WithMany(p => p.LoanReturnedToLibrarianNavigations)
                    .HasForeignKey(d => d.ReturnedToLibrarian)
                    .HasConstraintName("returnedToLibrarian");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PRIMARY");

                entity.ToTable("members");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Member)
                    .HasForeignKey<Member>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Members_userID_FK");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("publishers");

                entity.Property(e => e.PublisherId).HasColumnName("publisherID");

                entity.Property(e => e.PublisherName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("publisherName");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Active)
                    .HasColumnType("tinyint")
                    .HasColumnName("active")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("lastName");

                entity.Property(e => e.Localization)
                    .HasMaxLength(15)
                    .HasColumnName("localization");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.Theme)
                    .HasMaxLength(10)
                    .HasColumnName("theme");

                entity.Property(e => e.UserType)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("userType");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
