using Cet301FinalProject.Data;
using Cet301FinalProject.Data.Entities;
using Cet301FinalProject.Data.Repositories;

namespace Cet301FinalProject;

public partial class MainPage : ContentPage
{
    
    private IRepository<Address> _addressRepository;
    private IRepository<Admin> _adminRepository;
    private IRepository<Company> _companyRepository;
    private IRepository<Document> _documentRepository;
    private IRepository<Driver> _driverRepository;
    private IRepository<TransportationJob> _transportationJobRepository;
    private IRepository<Vehicle> _vehicleRepository;
    public MainPage(
        IRepository<Address> addressRepository, IRepository<Admin> adminRepository, IRepository<Company> companyRepository,
        IRepository<Document> documentRepository, IRepository<Driver> driverRepository, IRepository<TransportationJob> transportationJobRepository,
        IRepository<Vehicle> vehicleRepository
        )
    {
        _addressRepository = addressRepository;
        _adminRepository = adminRepository;
        _companyRepository = companyRepository;
        _documentRepository = documentRepository;
        _driverRepository = driverRepository;
        _transportationJobRepository = transportationJobRepository;
        _vehicleRepository = vehicleRepository;
        InitializeComponent();
    }

    public void LoadValue(object? sender, EventArgs eventArgs)
    {
        var selected = _adminRepository.GetAllAsync().Result.First();
        TestText.Text = selected?.Name ?? "No admin";
    }

}