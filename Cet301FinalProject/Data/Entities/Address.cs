using SQLite;
namespace Cet301FinalProject.Data.Entities;
[Table("addresses")]
public class Address
{
    [PrimaryKey]
    public string Id { get; set; }
    [Column("location_name")]
    public string LocationName { get; set; }
    [Column("latitude")]
    public double Latitude { get; set; }
    [Column("longitude")]
    public double Longitude { get; set; }
}