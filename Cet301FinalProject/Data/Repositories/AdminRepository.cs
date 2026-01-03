using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class AdminRepository : IRepository<Admin>
{
    private readonly SQLiteAsyncConnection _db;
    private readonly SessionContext _session;
    
    public AdminRepository(
        SQLiteAsyncConnection db,
        SessionContext session)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _session = session ?? throw new ArgumentNullException(nameof(session));
    }
    
    public Task<List<Admin>> GetAllAsync()
    {
        return _db.Table<Admin>()
            .Where(v => v.CompanyId == _session.CompanyId)
            .ToListAsync();
    }
}