using Cet301FinalProject.Pages;
using CetTransportApp.Data;

namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
    }

    private AppDatabase _db = new AppDatabase();
    
    public async void LoadValue(object? sender, EventArgs eventArgs)
    {
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }

}