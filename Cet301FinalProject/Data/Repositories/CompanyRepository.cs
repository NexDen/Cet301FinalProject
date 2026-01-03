using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class CompanyRepository : IRepository<Company>
{
    private readonly SQLiteAsyncConnection _db;

    public Task<List<Company>> GetAllAsync()
    {
        return _db.Table<Company>()
            .ToListAsync();
    }
}