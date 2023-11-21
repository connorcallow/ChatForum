using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class MessageContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Message.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>().HasKey(message => message.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
    }
    
    
}