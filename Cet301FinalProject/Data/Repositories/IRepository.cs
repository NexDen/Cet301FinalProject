namespace Cet301FinalProject.Data.Repositories;

public interface IRepository<T>
{
    public Task<List<T>> GetAllAsync();

}