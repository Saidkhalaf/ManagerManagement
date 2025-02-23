using System.Diagnostics;
using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ManagerManagement.DAL.EF;
public class ManagerDbContext : IdentityDbContext<IdentityUser>
{
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<DeveloperManager> DeveloperManagers { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<DeveloperTask> DeveloperTasks { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<IdentityUser> Users { get; set; }
    
    
    public ManagerDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<DeveloperTask>()
            .HasKey(tdp => tdp.Id);

        modelBuilder.Entity<DeveloperTask>()
            .HasOne(tdp => tdp.Developer)
            .WithMany(d => d.AssignedTasks)
            .HasForeignKey(tdp => tdp.DeveloperId)
            .IsRequired(); 

        modelBuilder.Entity<DeveloperTask>()
            .HasOne(tdp => tdp.Project)
            .WithMany(p => p.ProjectTasks)
            .HasForeignKey(tdp => tdp.ProjectId)
            .IsRequired();
        
        modelBuilder.Entity<Project>()
            .HasOne(p => p.DeveloperManager)
            .WithMany(m => m.ManagedProjects)
            .HasForeignKey(p => p.ManagerId)
            .IsRequired();
        
        modelBuilder.Entity<Client>()
            .HasKey(c => c.Id);
        
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Manager)
            .WithMany(dm => dm.ManagedClients)
            .HasForeignKey(c => c.ManagerId)
            .IsRequired();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //base.OnConfiguring(optionsBuilder)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=ManagerManagementDb.db");
        }

        optionsBuilder.LogTo(message =>
            Debug.WriteLine(message), LogLevel.Information);
    }

    public bool CreateDatabase(bool dropDatabase)
    {
        if (dropDatabase)
        {
            Database.EnsureDeleted();
        }
        return Database.EnsureCreated();
    }
    
}