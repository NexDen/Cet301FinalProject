using SQLite;
namespace Cet301FinalProject.Data;

public class DatabaseContext
{
    private SQLiteAsyncConnection? _db;

    private async Task InitAsync()
    {
        if (_db is not null)
        {
            return;
        }

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "maindatabase.db");
        _db = new SQLiteAsyncConnection(dbPath);
        await _db.CreateTableAsync<Address>();
        await _db.CreateTableAsync<Admin>();
        await _db.CreateTableAsync<Company>();
        await _db.CreateTableAsync<Document>();
        await _db.CreateTableAsync<Driver>();
        await _db.CreateTableAsync<TransportationJob>();
        await _db.CreateTableAsync<Vehicle>();
    }
}