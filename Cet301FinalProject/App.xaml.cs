using CetTransportApp.Data;

namespace Cet301FinalProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override async void OnStart()
    {
        var db = new AppDatabase();
        var ok = await db.CheckDatabaseConnection();

        if (!ok)
        {
            MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Database connection error",
                    TextColor = Colors.Red,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }
            };
        }
    }

    
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}