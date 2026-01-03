using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("transportation_jobs")]
public class TransportationJob
{
    [PrimaryKey]
    public string Id { get; set; }
    public Admin CreatedBy { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Vehicle Vehicle { get; set; }
    public string VehicleId { get; set; }
    public Driver Driver { get; set; }
    public string DriverId { get; set; }
    public string CommodityType { get; set; }
    public int Tonnage { get; set; }
    public float SaleCost { get; set; }
    public Address LoadingAddress { get; set; }
    public string LoadingAddressId { get; set; }
    public Address UnloadingAddress { get; set; }
    public string UnloadingAddressId { get; set; }
    public Document DeliveryDocument { get; set; }
    public string DeliveryDocumentId { get; set; }
    public bool IsActive { get; set; }
}