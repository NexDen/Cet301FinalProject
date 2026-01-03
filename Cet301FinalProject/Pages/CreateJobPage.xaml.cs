using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class CreateJobPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly Admin _admin;

    private List<Vehicle> _vehicles = new();
    private List<Driver> _drivers = new();

    public CreateJobPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadPickersAsync();

        OrderDatePicker.Date = DateTime.Today;
        DeliveryDatePicker.Date = DateTime.Today.AddDays(1);
        IsActiveSwitch.IsToggled = true;
    }


    private async Task LoadPickersAsync()
    {
        _vehicles = await _db.GetVehiclesAsync();
        _drivers = await _db.GetDriversAsync();

        VehiclePicker.ItemsSource = _vehicles;
        DriverPicker.ItemsSource = _drivers;
    }

    private async void CreateButton_Clicked(object sender, EventArgs e)
    {
        if (VehiclePicker.SelectedItem is not Vehicle vehicle ||
            DriverPicker.SelectedItem is not Driver driver)
        {
            await DisplayAlert("Error", "Select vehicle and driver.", "OK");
            return;
        }

        if (!int.TryParse(TonnageEntry.Text, out var tonnage) ||
            !float.TryParse(SaleCostEntry.Text, out var saleCost))
        {
            await DisplayAlert("Error", "Invalid numeric values.", "OK");
            return;
        }

        var loadingAddress = new Address
        {
            Id = Guid.NewGuid().ToString(),
            Latitude = 0.0d, // Lat-Lon değerleri sadece göstermelik oldu...
            Longitude = 0.0d,
            LocationName = LoadingAddressEntry.Text,
        };
        
        var unloadingAddress = new Address
        {
            Id = Guid.NewGuid().ToString(),
            Latitude = 0.0d,
            Longitude = 0.0d,
            LocationName = UnloadingAddressEntry.Text,
        };

        await _db.CreateAddressAsync(loadingAddress);
        await _db.CreateAddressAsync(unloadingAddress);
        

        var job = new TransportationJob
        {
            Id = Guid.NewGuid().ToString(),
            CreatedById = _admin.Id,
            CreatedDate = DateTime.Now,
            VehicleId = vehicle.Id,
            DriverId = driver.Id,
            CommodityType = CommodityEntry.Text,
            Tonnage = tonnage,
            SaleCost = saleCost,
            OrderDate = OrderDatePicker.Date,
            DeliveryDate = DeliveryDatePicker.Date,
            IsActive = IsActiveSwitch.IsToggled,
            LoadingAddressId = loadingAddress.Id,
            UnloadingAddressId = unloadingAddress.Id,
        };

        await _db.CreateJobAsync(job);

        await DisplayAlert("Success", "Job created successfully.", "OK");
        await Navigation.PopAsync();
    }
}
