using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using CetTransportApp.Data;

namespace Cet301FinalProject.Pages;

public partial class JobsListPage : ContentPage
{
    private readonly AppDatabase _db = new();
    private readonly Admin _admin;
    private List<JobListItem> _allJobs = new();

    public JobsListPage(Admin admin)
    {
        InitializeComponent();
        _admin = admin;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadJobsAsync();
    }

    private async Task LoadJobsAsync()
    {
        _allJobs = await _db.GetJobListForAdminAsync(_admin.Id);
        JobsView.ItemsSource = _allJobs;
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            JobsView.ItemsSource = _allJobs;
            return;
        }

        JobsView.ItemsSource = _allJobs
            .Where(j => j.VehicleDisplay.Contains(e.NewTextValue, StringComparison.CurrentCultureIgnoreCase));
    }

    private async void JobItem_Tapped(object sender, EventArgs e)
    {
        if (sender is not BindableObject bindable ||
            bindable.BindingContext is not JobListItem item)
            return;

        var job = item.Job;

        var details =
            $"""
             Vehicle:
             {item.VehicleDisplay}

             Driver:
             {item.DriverDisplay}

             Route:
             {item.LoadingUnloadingAddressDisplay}
             
             Commodity:
             {job.CommodityType}

             Tonnage:
             {job.Tonnage} Tons

             Sale Cost:
             {job.SaleCost:C}

             Order Date:
             {job.OrderDate:d}

             Delivery Date:
             {job.DeliveryDate:d}

             Active:
             {(job.IsActive ? "Yes" : "No")}
             """;

        var edit = await DisplayAlert(
            "Job Details",
            details,
            "Edit",
            "Close"
        );

        if (edit)
        {
            await Navigation.PushAsync(new EditJobPage(job));
        }
    }
    
    
    private async void RefreshButton_Clicked(object sender, EventArgs e)
        => await LoadJobsAsync();
}