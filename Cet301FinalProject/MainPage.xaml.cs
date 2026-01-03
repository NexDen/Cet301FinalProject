using Cet301FinalProject.Pages;
using CetTransportApp.Data;

namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        Navigation.PushAsync(new LoginPage());
        InitializeComponent();
    }

   

}