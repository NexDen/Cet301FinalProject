using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("transportation_jobs")]
public class TransportationJob
{
    [PrimaryKey]
    public string Id { get; set; }
    [Ignore]
    public Admin CreatedBy { get; set; }
    [Column("created_by_id")]
    public string CreatedById { get; set; }
    [Column("created_date")]
    public DateTime CreatedDate { get; set; }
    [Column("order_date")]
    public DateTime OrderDate { get; set; }
    [Column("delivery_date")]
    public DateTime DeliveryDate { get; set; }
    [Ignore]
    public Vehicle Vehicle { get; set; }
    [Column("vehicle_id")]
    public string VehicleId { get; set; }
    [Ignore]
    public Driver Driver { get; set; }
    [Column("driver_id")]
    public string DriverId { get; set; }
    [Column("commodity_type")]
    public string CommodityType { get; set; }
    [Column("tonnage")]
    public int Tonnage { get; set; }
    [Column("sale_cost")]
    public float SaleCost { get; set; }
    [Ignore]
    public Address LoadingAddress { get; set; }
    [Column("loading_address_id")]
    public string LoadingAddressId { get; set; }
    [Ignore]
    public Address UnloadingAddress { get; set; }
    [Column("unloading_address_id")]
    public string UnloadingAddressId { get; set; }
    [Ignore]
    public Document DeliveryDocument { get; set; }
    [Column("delivery_document_id")]
    public string DeliveryDocumentId { get; set; }
    [Column("is_active")]
    public bool IsActive { get; set; }
}