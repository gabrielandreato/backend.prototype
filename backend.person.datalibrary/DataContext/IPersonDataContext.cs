using backend.person.modellibrary.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.person.datalibrary.DataContext;

public interface IPersonDataContext
{
    public DbSet<Person> Person { get; set; }

    IDbContextTransaction? CurrentTransaction();
    IDbContextTransaction? BeginTransaction();
    bool IsInMemory();
    void Commit(IDbContextTransaction transaction);
    void RollBack(IDbContextTransaction transaction);
    void Migrate();
    void LockTable(string tableName);
    int SaveChanges();
}