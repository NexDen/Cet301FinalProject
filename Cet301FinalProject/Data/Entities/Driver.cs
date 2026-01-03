namespace Cet301FinalProject.Data.Entities;
using SQLite;
[Table("drivers")]

public class Driver
{
    [PrimaryKey]
    public string Id { get; set; }
    [Ignore]
    public Company Company { get; set; }
    [Column("company_id")]
    public string CompanyId { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("surname")]
    public string Surname { get; set; }
    [Ignore]
    public Document License { get; set; }
    [Column("license_id")]
    public string LicenseId { get; set; }
    [Column("username")]
    public string UserName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    
}