using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class DriverRepository : IRepository<Driver>
{
    private readonly SQLiteAsyncConnection _db;
    private readonly SessionContext _session;

    public Task<List<Driver>> GetAllAsync()
    {
        return _db.Table<Driver>()
            .Where(v => v.CompanyId == _session.CompanyId)
            .ToListAsync();
    }
}