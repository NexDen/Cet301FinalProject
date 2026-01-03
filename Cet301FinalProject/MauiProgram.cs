using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Data.Repositories;
using Cet301FinalProject.Security;
using Microsoft.Extensions.Logging;
using SQLite;

namespace Cet301FinalProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services.AddSingleton<SQLiteAsyncConnection>(sp =>
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "transport.db");
            var db = new SQLiteAsyncConnection(dbPath);
            return db;
        });

        builder.Services.AddSingleton<SessionContext>();
        builder.Services.AddSingleton<DatabaseInitializer>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<IRepository<Address>, AddressRepository>();
        builder.Services.AddTransient<IRepository<Admin>, AdminRepository>();
        builder.Services.AddTransient<IRepository<Company>, CompanyRepository>();
        builder.Services.AddTransient<IRepository<Document>, DocumentRepository>();
        builder.Services.AddTransient<IRepository<Driver>, DriverRepository>();
        builder.Services.AddTransient<IRepository<TransportationJob>, TransportationJobRepository>();
        builder.Services.AddTransient<IRepository<Vehicle>, VehicleRepository>();
        
        
        
        return builder.Build();
    }
}