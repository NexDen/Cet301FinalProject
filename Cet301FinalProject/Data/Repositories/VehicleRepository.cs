using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class VehicleRepository : IRepository<Vehicle>
{
    private readonly SQLiteAsyncConnection _db;
    private readonly SessionContext _session;

    public Task<List<Vehicle>> GetAllAsync()
    {
        return _db.Table<Vehicle>()
            .Where(v => v.CompanyId == _session.CompanyId)
            .ToListAsync();
    }
}