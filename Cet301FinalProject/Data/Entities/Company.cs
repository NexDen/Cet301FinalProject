namespace Cet301FinalProject.Data.Entities;
using SQLite;
[Table("companies")]
public class Company
{
    [PrimaryKey]
    public string Id { get; init; }
    public string Name { get; init; }
}