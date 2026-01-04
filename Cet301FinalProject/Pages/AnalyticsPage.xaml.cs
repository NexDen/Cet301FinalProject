using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class AnalyticsPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly Admin _admin;

    public AnalyticsPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAnalyticsAsync();
    }

    private async Task LoadAnalyticsAsync()
    {
        var jobs = await _db.GetJobsAsync();
        jobs = jobs.Where(j => j.CreatedById == _admin.Id).ToList();
        var vehicles = await _db.GetVehiclesAsync();
        var drivers = await _db.GetDriversAsync();

        TotalJobsLabel.Text = jobs.Count.ToString();
        ActiveJobsLabel.Text = jobs.Count(j => j.IsActive).ToString();
        TonnageLabel.Text = jobs.Sum(j => j.Tonnage).ToString();
        RevenueLabel.Text = jobs.Sum(j => j.SaleCost).ToString("₺0.00");

        var vehicleStats =
            jobs.GroupBy(j => j.VehicleId)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Count()
                })
                .Join(vehicles, g => g.Id, v => v.Id,
                    (g, v) => new
                    {
                        Name = $"{v.PlateNo} ({v.Model})",
                        g.Count
                    })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

        VehicleStatsView.ItemsSource = vehicleStats;

        var driverStats =
            jobs.GroupBy(j => j.DriverId)
                .Select(g => new
                {
                    Id = g.Key,
                    Count = g.Count()
                })
                .Join(drivers, g => g.Id, d => d.Id,
                    (g, d) => new
                    {
                        Name = $"{d.Name} {d.Surname}",
                        g.Count
                    })
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

        DriverStatsView.ItemsSource = driverStats;
    }
}
