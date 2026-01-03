using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Cet301FinalProject.Data.Entities;
using SQLite;

namespace CetTransportApp.Data;

public class AppDatabase
{
    private SQLiteAsyncConnection _db;

    public static Admin ContextAdmin; 
    
    private async Task InitAsync()
    {
        if (_db != null)
        {
            return;
        }
        

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "transport.db");
        using var stream = await FileSystem.OpenAppPackageFileAsync("transport.db");
        using var fileStream = File.Create(dbPath);
        await stream.CopyToAsync(fileStream);
        

        _db = new SQLiteAsyncConnection(dbPath);
        
        await _db.CreateTableAsync<Address>();
        await _db.CreateTableAsync<Admin>();
        await _db.CreateTableAsync<Company>();
        await _db.CreateTableAsync<Document>();
        await _db.CreateTableAsync<Driver>();
        await _db.CreateTableAsync<TransportationJob>();
        await _db.CreateTableAsync<Vehicle>();
    }

    public async Task<Admin?> LoginAsync(string username, string password)
    {
        await InitAsync();
        return await _db.Table<Admin>()
            .Where(a => a.UserName == username && a.PasswordHash == password)
            .FirstOrDefaultAsync();
    }
    public async Task<List<Vehicle>> GetVehiclesAsync()
    {
        await InitAsync();
        return await _db.Table<Vehicle>().ToListAsync();
    }
    public async Task<List<Driver>> GetDriversAsync()
    {
        await InitAsync();
        return await _db.Table<Driver>().ToListAsync();
    }
    public async Task CreateJobAsync(TransportationJob job)
    {
        await InitAsync();
        await _db.InsertAsync(job);
    }
    public async Task<List<TransportationJob>> GetJobsAsync()
    {
        await InitAsync();
        return await _db.Table<TransportationJob>().ToListAsync();
    }
}