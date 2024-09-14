using ApiRefrigerator.Models;

namespace Domain.Interfaces
{
    public interface IRefrigeratorRepository
    {
        Task<IEnumerable<Refrigerator>> GetAllAsync();
        Task<Refrigerator?> GetByIdAsync(int id);
        Task<Refrigerator?> GetByNameAsync(string name);
        Task<Refrigerator?> InsertItemAsync(Refrigerator item);
        Task<IEnumerable<Refrigerator>?> InsertItemsAsync(IEnumerable<Refrigerator> items);
        Task<int> RemoveAllAsync();
        Task<Refrigerator?> RemoveItemAsync(int id);
        Task<Refrigerator?> UpdateItemAsync(Refrigerator item);
    }
}
