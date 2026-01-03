using SQLite;

namespace Cet301FinalProject.Data.Entities;

[Table("admins")]
public class Admin
{
    [PrimaryKey]
    public string Id { get; set; }
    public Company Company { get; set; }
    public string CompanyId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}