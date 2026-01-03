using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class AddressRepository : IRepository<Address>
{
    private readonly SQLiteAsyncConnection _db;

    public Task<List<Address>> GetAllAsync()
    {
        return _db.Table<Address>()
            .ToListAsync();
    }
}