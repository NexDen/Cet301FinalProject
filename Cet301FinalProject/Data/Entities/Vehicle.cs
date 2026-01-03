using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("vehicles")]
public class Vehicle
{
    [PrimaryKey]
    public string Id {get; set; }
    public Company Company {get; set; }
    public string CompanyId {get; set; }
    public string PlateNo {get; set; }
    public string Model {get; set; }
    public string ProductionYear {get; set; }
    public Document VehicleCertification {get; set; }
    public string VehicleCertificationId {get; set; }
    public float CurrentLatitude {get; set; }
    public float CurrentLongitude {get; set; }
}