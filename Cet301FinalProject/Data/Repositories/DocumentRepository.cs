using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Security;
using SQLite;

namespace Cet301FinalProject.Data.Repositories;

public class DocumentRepository : IRepository<Document>
{
    private readonly SQLiteAsyncConnection _db;
    private readonly SessionContext _session;

    public Task<List<Document>> GetAllAsync()
    {
        return _db.Table<Document>()
            .ToListAsync();
    }
}