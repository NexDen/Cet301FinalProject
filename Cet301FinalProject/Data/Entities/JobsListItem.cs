namespace Cet301FinalProject.Data.Entities;

public class JobListItem
{
    public TransportationJob Job { get; set; }

    public DateTime OrderDate => Job.OrderDate;

    public string VehicleDisplay { get; set; }
    public string DriverDisplay { get; set; }
    
    public string LoadingUnloadingAddressDisplay { get; set; }

    public string Commodity => Job.CommodityType;
    public int Tonnage => Job.Tonnage;
    public float SaleCost => Job.SaleCost;
    public bool IsActive => Job.IsActive;
}