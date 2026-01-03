using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly Admin _admin;

    public SettingsPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
    }

    private async void ReloadData_Clicked(object sender, EventArgs e)
    {
        await _db.GetVehiclesAsync();
        await _db.GetDriversAsync();
        await DisplayAlert("OK", "Data reloaded.", "OK");
    }
}