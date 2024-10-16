namespace ToDoApp.Server.Data;
using Microsoft.EntityFrameworkCore;

using ToDoApp.Server.Models;


    public class ApplicationDbContext : DbContext
    {
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
    }
}

