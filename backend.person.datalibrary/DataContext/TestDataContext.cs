using backend.person.modellibrary.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.person.datalibrary.DataContext;

public class TestDataContext: DbContext, IPersonDataContext
{
    public DbSet<Person> Person { get; set; } = null!;

    private readonly string? _v = null;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(string.IsNullOrEmpty(_v) ? "dbTestes" : _v);
        optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    public IDbContextTransaction? CurrentTransaction()
    {
        return Database.CurrentTransaction;
    }

    public IDbContextTransaction? BeginTransaction()
    {
        if (Database.IsInMemory()) return null;

        if (Database.CurrentTransaction != null) return Database.CurrentTransaction;

        return Database.BeginTransaction();
    }

    public bool IsInMemory()
    {
        return Database.IsInMemory();
    }

    public void Commit(IDbContextTransaction transaction)
    {
        transaction.Commit();
    }

    public void RollBack(IDbContextTransaction transaction)
    {
        transaction.Rollback();
    }

    public void Migrate()
    {
    }

    public void LockTable(string tableName)
    {
    }
    
}