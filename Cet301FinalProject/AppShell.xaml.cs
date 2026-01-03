using Cet301FinalProject.Pages;

namespace Cet301FinalProject;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
    }
}