using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class CreateJobPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly Admin _admin;

    public CreateJobPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        VehiclePicker.ItemsSource = await _db.GetVehiclesAsync();
        DriverPicker.ItemsSource = await _db.GetDriversAsync();
    }

    private async void CreateJob_Clicked(object sender, EventArgs e)
    {
        if (VehiclePicker.SelectedItem == null ||
            DriverPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Select vehicle and driver.", "OK");
            return;
        }

        var job = new TransportationJob
        {
            Id = Guid.NewGuid().ToString(),
            VehicleId = ((Vehicle)VehiclePicker.SelectedItem).Id,
            DriverId = ((Driver)DriverPicker.SelectedItem).Id,
            CreatedById = _admin.Id,
            OrderDate = OrderDatePicker.Date
        };

        await _db.CreateJobAsync(job);

        await DisplayAlert("Success", "Job created.", "OK");
        await Navigation.PopAsync();
    }
}