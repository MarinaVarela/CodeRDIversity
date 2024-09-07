using ApiGeladeira.Models;

namespace Domain.Interfaces
{
    public interface IGeladeiraRepository
    {
        Task<ItemGeladeira?> AdicionarItem(ItemGeladeira item);
        Task<ItemGeladeira?> AtualizarItem(ItemGeladeira item);
        Task<ItemGeladeira?> ObterItemPorId(int id);
        Task<ItemGeladeira?> ObterItemPorNome(string nome);
        Task<List<ItemGeladeira>> ObterItens();
        Task<ItemGeladeira?> RemoverItem(int id);
        Task<int> RemoverTodosItens();
    }
}
