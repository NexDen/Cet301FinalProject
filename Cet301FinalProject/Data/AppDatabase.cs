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
    private bool isInitialized = false;
    
    private async Task InitAsync()
    {
        if (_db != null)
        {
            return;
        }

        if (!isInitialized)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "transport.db");
            using var stream = await FileSystem.OpenAppPackageFileAsync("transport.db");
            using var fileStream = File.Create(dbPath);
            await stream.CopyToAsync(fileStream);
            _db = new SQLiteAsyncConnection(dbPath);
            isInitialized = true;
        }
        else
        {
            _db = new SQLiteAsyncConnection("transport.db");
        }




        await _db.CreateTableAsync<Address>();
        await _db.CreateTableAsync<Admin>();
        await _db.CreateTableAsync<Company>();
        await _db.CreateTableAsync<Document>();
        await _db.CreateTableAsync<Driver>();
        await _db.CreateTableAsync<TransportationJob>();
        await _db.CreateTableAsync<Vehicle>();
    }
    
    public async Task<bool> CheckDatabaseConnection()
    {
        await InitAsync();

        try
        {
            var count = await _db.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM admins"
            );

            return count >= 0;
        }
        catch
        {
            return false;
        }
    }
    

    public async Task<Admin?> LoginAsync(string username, string password)
    {
        await InitAsync();
        var admin = await _db.Table<Admin>()
            .Where(a => a.UserName == username && a.PasswordHash == password)
            .FirstOrDefaultAsync();
        ContextAdmin = admin;
        return admin;
    }
    
    public async Task<List<JobListItem>> GetJobListForAdminAsync(string adminId)
    {
        await InitAsync();

        var jobs = await _db.Table<TransportationJob>()
            .Where(j => j.CreatedById == adminId)
            .ToListAsync();

        var vehicles = await _db.Table<Vehicle>().ToListAsync();
        var drivers = await _db.Table<Driver>().ToListAsync();

        var vehicleMap = vehicles.ToDictionary(v => v.Id);
        var driverMap = drivers.ToDictionary(d => d.Id);

        return jobs.Select(j => new JobListItem
        {
            JobId = j.Id,
            OrderDate = j.OrderDate,

            VehicleDisplay = vehicleMap.TryGetValue(j.VehicleId, out var v)
                ? $"{v.PlateNo} ({v.Model})"
                : "N/A",

            DriverDisplay = driverMap.TryGetValue(j.DriverId, out var d)
                ? $"{d.Name} {d.Surname}"
                : "N/A"
        }).ToList();
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