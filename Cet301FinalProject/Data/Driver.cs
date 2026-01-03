namespace Cet301FinalProject.Data;

public class Driver
{
    public string Id { get; set; }
    public Company Company { get; set; }
    public string CompanyId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Document License { get; set; }
    public string LicenseId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
}