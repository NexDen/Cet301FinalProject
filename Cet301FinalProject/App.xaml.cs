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
        var ok = await db.SanityCheckAsync();

        if (!ok)
        {
            MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Database failed to load.",
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