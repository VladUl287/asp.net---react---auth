﻿using api.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo((string message) =>
            {
                Console.WriteLine(message);
                Debug.WriteLine(message);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                build.HasIndex(e => e.Email)
                    .IsUnique();

                build.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(150);

                build.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150);

                build.HasData(new User
                {
                    Id = 1,
                    Email = "ulyanovskiy.01@mail.ru",
                    Login = "fefrues",
                    Password = "rqhdJQb/Oi7AvOFUJsnFlo99n6F7ct0B+Sgudw7kNMM=",
                    RoleId = 1
                });
            });

            modelBuilder.Entity<Role>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                build.HasData(new Role[]
                {
                    new Role() { Id = 1, Name = "Admin" },
                    new Role() { Id = 2, Name = "User" }
                });
            });

            modelBuilder.Entity<Token>(build =>
            {
                build.HasKey(e => e.Id);

                build.Property(e => e.RefreshToken)
                    .IsRequired();

                build.HasIndex(e => e.RefreshToken);
            });
        }
    }
}