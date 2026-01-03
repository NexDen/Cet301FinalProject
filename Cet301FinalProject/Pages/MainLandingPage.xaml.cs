using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class MainLandingPage : ContentPage
{
    private readonly Admin _admin;

    public MainLandingPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
        AdminNameLabel.Text = $"Logged in as {_admin.Name} {_admin.Surname}";
    }

    +
    public async void JobsButton_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new JobsListPage(_admin));

    public async void CreateJobButton_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new CreateJobPage(_admin));

    public async void SettingsButton_Clicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new SettingsPage(_admin));

    public async void LogoutButton_Clicked(object sender, EventArgs e)
        => await Navigation.PopToRootAsync();
}
