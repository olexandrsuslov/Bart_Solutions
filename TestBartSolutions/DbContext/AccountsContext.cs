using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using TestBartSolutions.Models;

namespace TestBartSolutions.DbContext;

public class AccountsContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    
    public override int SaveChanges()
    {
        // Custom validation before saving changes
        foreach (var incidentEntry in ChangeTracker.Entries<Incident>())
        {
            if (incidentEntry.State == EntityState.Added && incidentEntry.Entity.Accounts.Count == 0)
            {
                throw new InvalidOperationException("incident cannot be created without account");
            }
        }
        
        foreach (var accountEntry in ChangeTracker.Entries<Account>())
        {
            if (accountEntry.State == EntityState.Added && accountEntry.Entity.Contacts.Count == 0)
            {
                throw new InvalidOperationException("account cannot be created without contact");
            }
        }

        return base.SaveChanges();
    }
    
    public AccountsContext(DbContextOptions<AccountsContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public AccountsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AccountsContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=master;User=SA;Password=reallyStrongPwd123;TrustServerCertificate=true"); 
        return new AccountsContext(optionsBuilder.Options); 
    }
    

}