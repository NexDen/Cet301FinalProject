using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using Cet301FinalProject.Data.Entities;
using SQLite;

namespace CetTransportApp.Data;

public class AppDatabase
{
    private static SQLiteAsyncConnection _db;

    public static Admin ContextAdmin; 
    private static bool isInitialized = false;
    
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
            _db = new SQLiteAsyncConnection(dbPath, storeDateTimeAsTicks: false);
            await _db.CreateTableAsync<Address>();
            await _db.CreateTableAsync<Admin>();
            await _db.CreateTableAsync<Company>();
            await _db.CreateTableAsync<Document>();
            await _db.CreateTableAsync<Driver>();
            await _db.CreateTableAsync<TransportationJob>();
            await _db.CreateTableAsync<Vehicle>();
            isInitialized = true;
        }
        
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
    
    private static string GetAddressName(
        Dictionary<string, Address> map,
        string addressId)
    {
        return !string.IsNullOrWhiteSpace(addressId) &&
               map.TryGetValue(addressId, out var address)
            ? address.LocationName
            : "N/A";
    }
    
    public async Task<List<JobListItem>> GetJobListForAdminAsync(string adminId)
    {
        await InitAsync();

        var jobs = await _db.Table<TransportationJob>()
            .Where(j => j.CreatedById == adminId)
            .ToListAsync();

        var vehicles = await _db.Table<Vehicle>().ToListAsync();
        var drivers = await _db.Table<Driver>().ToListAsync();
        var addresses = await _db.Table<Address>().ToListAsync();
        
        
        var vehicleMap = vehicles.ToDictionary(v => v.Id);
        var driverMap = drivers.ToDictionary(d => d.Id);
        var addressMap = addresses.ToDictionary(a => a.Id);
        
        return jobs.Select(j => new JobListItem
        {
            Job = j,

            VehicleDisplay = vehicleMap.TryGetValue(j.VehicleId, out var v)
                ? $"{v.PlateNo} ({v.Model})"
                : "N/A",

            DriverDisplay = driverMap.TryGetValue(j.DriverId, out var d)
                ? $"{d.Name} {d.Surname}"
                : "N/A",
            
            LoadingUnloadingAddressDisplay =
                $"{GetAddressName(addressMap, j.LoadingAddressId)} -> {GetAddressName(addressMap, j.UnloadingAddressId)}"

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
        return await _db.Table<TransportationJob>().Where(tj => tj.IsActive).ToListAsync();
    }
    
    public async Task UpdateJobAsync(TransportationJob job)
    {
        await InitAsync();
        await _db.UpdateAsync(job);
    }
    
    public async Task CreateAddressAsync(Address address)
    {
        await InitAsync();
        await _db.InsertAsync(address);
    }

}