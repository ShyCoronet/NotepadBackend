using Microsoft.EntityFrameworkCore;

namespace NotepadBackend.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        private DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserDbSettings

            modelBuilder.Entity<User>().Property(user => user.Login).HasMaxLength(20).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().HasIndex(user => user.Login).IsUnique();
            modelBuilder.Entity<User>().Property(user => user.Password).HasMaxLength(50).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Email).HasMaxLength(350).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.RegistrationDateTime).IsRequired();

            #endregion

            #region NoteDbSettings

            modelBuilder.Entity<Note>().Property(note => note.Name).IsUnicode().IsRequired();
            modelBuilder.Entity<Note>().Property(note => note.Content).IsUnicode().IsRequired();
            modelBuilder.Entity<Note>().Property(note => note.CreationDateTime).IsRequired();
            modelBuilder.Entity<Note>().Property(note => note.UserId).IsRequired();

            #endregion

            #region TokenDbSettings

            modelBuilder.Entity<RefreshToken>().HasKey(token => token.TokenId);
            modelBuilder.Entity<RefreshToken>().Property(token => token.Value).IsUnicode().IsRequired();
            modelBuilder.Entity<RefreshToken>().Property(token => token.DeathTime).IsRequired();

            #endregion
        }
    }
}
