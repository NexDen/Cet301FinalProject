namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    
    public MainPage()
    {
        InitializeComponent();
    }

    public void LoadValue(object? sender, EventArgs eventArgs)
    {
        TestText.Text = "merhaba!";
    }

}