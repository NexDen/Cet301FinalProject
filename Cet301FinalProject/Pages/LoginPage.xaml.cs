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

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var rememberedAdminId =
            Preferences.Get("remember_admin_id", null);

        if (!string.IsNullOrEmpty(rememberedAdminId))
        {
            var admin = await _db.GetAdminByIdAsync(rememberedAdminId);
            await Navigation.PushAsync(new MainLandingPage(admin));
        }

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
        
        
        var admin = await _db.LoginAsync(
            UsernameEntry.Text,
            passwordHash??""
        );

        if (admin == null)
        {
            await DisplayAlert("Error", "Username or Password is Wrong!", "OK");
            return;
        }

        if (RememberCheckbox.IsChecked)
        {
            Preferences.Set("remember_admin_id", admin.Id);
        }
        else
        {
            Preferences.Remove("remember_admin_id");
        }

        await Navigation.PushAsync(new MainLandingPage(admin));
    }
}



