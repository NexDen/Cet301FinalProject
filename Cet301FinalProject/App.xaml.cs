using Cet301FinalProject.Pages;
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
        
    }

    
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(
            new NavigationPage(new LoginPage())
        );
    }
}