using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp10;

public partial class BankClientsContext : DbContext
{
    public BankClientsContext()
    {
    }

    public BankClientsContext(DbContextOptions<BankClientsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source = C:\\\\\\\\Users\\\\\\\\Konstantin\\\\\\\\source\\\\\\\\repos\\\\\\\\CodeFirst_Bank\\\\\\\\CodeFirst\\\\\\\\bin\\\\\\\\Debug\\\\\\\\net7.0\\\\\\\\BankClients.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
