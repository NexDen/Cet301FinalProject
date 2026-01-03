using System.Security.Cryptography;
using System.Text;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class LoginPage : ContentPage
{
    private AppDatabase _db = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private static string Sha256Hex(string input)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(input);
        byte[] hash = sha256.ComputeHash(bytes);

        return Convert.ToHexString(hash).ToLowerInvariant();
    }
    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        var passwordHash = Sha256Hex(PasswordEntry.Text);

        var burak = await _db.GetJobsAsync();
        
        var admin = await _db.LoginAsync(
            UsernameEntry.Text,
            passwordHash??""
        );

        if (admin == null)
        {
            await DisplayAlert("Error", "Invalid login", "OK");
            return;
        }

        await Navigation.PushAsync(new JobManagementPage());
    }
}



