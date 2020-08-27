﻿using Microsoft.EntityFrameworkCore;

namespace NotepadBackend.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserCreatingPropertys

            modelBuilder.Entity<User>().Property(user => user.Login).HasMaxLength(20).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Password).HasMaxLength(50).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Email).HasMaxLength(350).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.Role).HasMaxLength(20).IsUnicode().IsRequired();
            modelBuilder.Entity<User>().Property(user => user.RegistrationDateTime).IsRequired();

            #endregion

            #region NoteCreatingPropertys

            modelBuilder.Entity<Note>().Property(user => user.Name).IsUnicode().IsRequired();
            modelBuilder.Entity<Note>().Property(user => user.Content).IsUnicode().IsRequired();
            modelBuilder.Entity<Note>().Property(user => user.CreationDateTime).IsRequired();

            #endregion
        }
    }
}
