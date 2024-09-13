using ApiRefrigerator.Models;

namespace Domain.Interfaces
{
    public interface IRefrigeratorService
    {
        Task<List<Refrigerator>> GetAllAsync();
        Task<Refrigerator?> GetByIdAsync(int id);
        Task<Refrigerator?> GetByNameAsync(string name);
        Task<Refrigerator?> InsertItemAsync(Refrigerator item);
        Task<int> RemoveAllAsync();
        Task<Refrigerator?> RemoveItemAsync(int id);
        Task<Refrigerator?> UpdateItemAsync(Refrigerator item);
    }
}
