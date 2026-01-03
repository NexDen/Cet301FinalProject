using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class TransportationJobRepository : IRepository<TransportationJob>
{
    private readonly SQLiteAsyncConnection _db;
    private readonly SessionContext _session;

    public Task<List<TransportationJob>> GetAllAsync()
    {
        return _db.Table<TransportationJob>()
            .Where(v => v.CreatedBy.CompanyId == _session.CompanyId)
            .ToListAsync();
    }
}