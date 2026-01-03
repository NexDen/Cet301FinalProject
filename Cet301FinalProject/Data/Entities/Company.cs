namespace Cet301FinalProject.Data.Entities;
using SQLite;
[Table("companies")]
public class Company
{
    [PrimaryKey]
    public string Id { get; init; }
    [Column("name")]
    public string Name { get; init; }
}