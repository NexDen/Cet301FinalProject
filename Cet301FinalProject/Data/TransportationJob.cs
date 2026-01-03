namespace Cet301FinalProject.Data;

public class TransportationJob
{
    public string Id { get; set; }
    public Admin CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Vehicle Vehicle { get; set; }
    public Driver Driver { get; set; }
    public string CommodityType { get; set; }
    public int Tonnage { get; set; }
    public float SaleCost { get; set; }
    public Address LoadingAddress { get; set; }
    public Address UnloadingAddress { get; set; }
    public Document DeliveryDocument { get; set; }
    public bool IsActive { get; set; }
}