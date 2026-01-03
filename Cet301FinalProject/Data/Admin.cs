namespace Cet301FinalProject.Data;

public class Admin
{
    public string Id { get; set; }
    public Company Company { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}