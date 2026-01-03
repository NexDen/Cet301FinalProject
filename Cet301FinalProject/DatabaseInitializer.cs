using Cet301FinalProject;
using Cet301FinalProject.Data.Entities;
using SQLite;

public class DatabaseInitializer
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseInitializer(SQLiteAsyncConnection db)
    {
        _db = db;
    }

    public async Task InitializeAsync()
    {
        await _db.CreateTablesAsync(
            CreateFlags.FullTextSearch3,
            typeof(Address),
            typeof(Admin),
            typeof(Company),
            typeof(Document),
            typeof(Driver),
            typeof(TransportationJob),
            typeof(Vehicle)
        );
    }
}