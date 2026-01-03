using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class EditJobPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly TransportationJob _job;

    private List<Vehicle> _vehicles = new();
    private List<Driver> _drivers = new();

    public EditJobPage(TransportationJob job)
    {
        InitializeComponent();
        _job = job;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await LoadPickersAsync();
        PopulateFields();
    }

    private async Task LoadPickersAsync()
    {
        _vehicles = await _db.GetVehiclesAsync();
        _drivers = await _db.GetDriversAsync();

        VehiclePicker.ItemsSource = _vehicles;
        DriverPicker.ItemsSource = _drivers;

        VehiclePicker.SelectedItem =
            _vehicles.FirstOrDefault(v => v.Id == _job.VehicleId);

        DriverPicker.SelectedItem =
            _drivers.FirstOrDefault(d => d.Id == _job.DriverId);
    }

    private void PopulateFields()
    {
        CommodityEntry.Text = _job.CommodityType;
        TonnageEntry.Text = _job.Tonnage.ToString();
        SaleCostEntry.Text = _job.SaleCost.ToString();
        OrderDatePicker.Date = _job.OrderDate;
        DeliveryDatePicker.Date = _job.DeliveryDate;
        IsActiveSwitch.IsToggled = _job.IsActive;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (VehiclePicker.SelectedItem is not Vehicle vehicle ||
            DriverPicker.SelectedItem is not Driver driver)
        {
            await DisplayAlert("Error", "No Driver/Vehicle Selected.", "OK");
            return;
        }

        if (!int.TryParse(TonnageEntry.Text, out var tonnage) ||
            !float.TryParse(SaleCostEntry.Text, out var saleCost))
        {
            await DisplayAlert("Error", "Invalid numeric values.", "OK");
            return;
        }

        _job.VehicleId = vehicle.Id;
        _job.DriverId = driver.Id;
        _job.CommodityType = CommodityEntry.Text;
        _job.Tonnage = tonnage;
        _job.SaleCost = saleCost;
        _job.OrderDate = OrderDatePicker.Date;
        _job.DeliveryDate = DeliveryDatePicker.Date;
        _job.IsActive = IsActiveSwitch.IsToggled;

        await _db.UpdateJobAsync(_job);

        await DisplayAlert("Saved", "Job Updated Successfully!", "OK");
        await Navigation.PopAsync();
    }
}
