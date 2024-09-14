using ApiRefrigerator.Models;
using Domain.Interfaces;

namespace Application.Services
{
    public class RefrigeratorService : IRefrigeratorService
    {
        private readonly IRefrigeratorRepository _repository;
        public RefrigeratorService(IRefrigeratorRepository repository)

        {
            _repository = repository;
        }

        public async Task<IEnumerable<Refrigerator>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        } 

        public async Task<Refrigerator?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Refrigerator?> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<Refrigerator?> InsertItemAsync(Refrigerator item)
        {
            var checkItem = await GetByNameAsync(item.Name);

            if (checkItem is not null)
                throw new ApplicationException($"The item {item.Name} is already in the refrigerator.");

            var result = await _repository.InsertItemAsync(item);

            if (result is null)
                throw new ApplicationException("Failed to add item.");

            return item;
        }

        public async Task<IEnumerable<Refrigerator>?> InsertItemsAsync(IEnumerable<Refrigerator> items)
        {
            var result = await _repository.InsertItemsAsync(items);

            if (result is null)
                throw new ApplicationException("Failed to add items.");

            return items;
        }

        public async Task<Refrigerator?> UpdateItemAsync(Refrigerator item)
        {
            var checkItem = await _repository.GetByIdAsync(item.Id);
            if (checkItem is null)
                throw new ApplicationException($"Invalid item.");

            var result = await _repository.UpdateItemAsync(item);
            if (result is null)
                throw new ApplicationException("Failed to update item.");

            return item;
        }

        public async Task<Refrigerator?> RemoveItemAsync(int id)
        {
            var removedItem = await _repository.RemoveItemAsync(id);
            if (removedItem == null)
                throw new KeyNotFoundException();

            return removedItem;
        }

        public async Task<int> RemoveAllAsync()
        {
            var deleteItem = await _repository.RemoveAllAsync(); 
            if (deleteItem == 0)
                throw new ApplicationException("There are no items to delete.");

            return deleteItem;
        }
    }
}
