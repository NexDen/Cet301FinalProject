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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ThemeSwitch.IsToggled =
            Application.Current.UserAppTheme == AppTheme.Dark;
    }

    private void ThemeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        Application.Current.UserAppTheme =
            e.Value ? AppTheme.Dark : AppTheme.Light;
    }

    
    private async void ReloadData_Clicked(object sender, EventArgs e)
    {
        await _db.GetVehiclesAsync();
        await _db.GetDriversAsync();
        await DisplayAlert("OK", "Data reloaded.", "OK");
    }
}