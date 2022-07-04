using ITCompanyCVManager.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace ITCompanyCVManager.Persistence.Context;

public class ApplicationDbContext :
    DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateAuditEntries();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditEntries()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(entry => entry.Entity is IAudit && entry.State == EntityState.Modified)
            .ToList();

        foreach (var entityEntry in entries)
        {
            var now = DateTime.UtcNow;
            entityEntry.Property(nameof(IAudit.Updated)).CurrentValue = now;
        }
    }
}