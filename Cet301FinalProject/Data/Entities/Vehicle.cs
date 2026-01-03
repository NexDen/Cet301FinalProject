using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("vehicles")]
public class Vehicle
{
    [PrimaryKey]
    public string Id {get; set; }
    [Ignore]
    public Company Company {get; set; }
    [Column("company_id")]
    public string CompanyId {get; set; }
    [Column("plate_no")]
    public string PlateNo {get; set; }
    [Column("model")]
    public string Model {get; set; }
    [Column("production_year")]
    public string ProductionYear {get; set; }
    [Ignore]
    public Document VehicleCertification {get; set; }
    [Column("vehicle_certification_id")]
    public string VehicleCertificationId {get; set; }
    [Column("current_location_lat")]
    public float CurrentLatitude {get; set; }
    [Column("current_location_lon")]
    public float CurrentLongitude {get; set; }
}