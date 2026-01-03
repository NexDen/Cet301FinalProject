using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("admins")]
public class Admin
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
    [Column("username")]
    public string UserName { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
}