using SQLite;
namespace Cet301FinalProject.Data.Entities;
[Table("addresses")]
public class Address
{
    [PrimaryKey]
    public string Id { get; set; }
    public string LocationName { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}