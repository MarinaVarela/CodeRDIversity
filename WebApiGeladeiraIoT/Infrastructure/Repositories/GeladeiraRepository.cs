using ApiGeladeira.Models;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiGeladeira.Repository
{
    public class GeladeiraRepository : IGeladeiraRepository

    {
        private readonly GeladeiraContext _context;

        public GeladeiraRepository(GeladeiraContext context)
        {
            _context = context;
        }

        public async Task<List<ItemGeladeira>> ObterItens()
        {
            var itens = await _context.ItensGeladeira.ToListAsync();
            if (!itens.Any())
            {
                throw new InvalidDataException();
            }
            return itens;
        }

        public async Task<ItemGeladeira?> ObterItemPorId(int id)
        {
            var itemExistente = await _context.ItensGeladeira.FindAsync(id);
            if (itemExistente is null)
                return null;
            return itemExistente;
        }

        public async Task<ItemGeladeira?> ObterItemPorNome(string nome)
        {
            var procurarGeladeira = await _context.ItensGeladeira.FirstOrDefaultAsync(i => i.Nome.Equals(nome));
            if (procurarGeladeira is null)
                return null;
            return procurarGeladeira;
        }

        public async Task<ItemGeladeira?> AdicionarItem(ItemGeladeira item)
        {
            try
            {
                _context.ItensGeladeira.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ItemGeladeira?> AtualizarItem(ItemGeladeira item)
        {
            try
            {
                var itemExistente = await _context.ItensGeladeira.FindAsync(item.Id);
                if (itemExistente is null)
                    return null;

                _context.Entry(itemExistente).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();
                return itemExistente;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ItemGeladeira?> RemoverItem(int id)
        {
            try
            {
                var item = await ObterItemPorId(id);

                if (item is null)
                    return null;

                _context.ItensGeladeira.Remove(item);
                await _context.SaveChangesAsync();

                return item;
            }
            catch
            {
                throw new ApplicationException("Não foi possível remover o item.");
            }
        }

        public async Task<int> RemoverTodosItens()
        {
            try
            {
                var itens = await ObterItens();

                if (itens is not null)
                {
                    _context.ItensGeladeira.RemoveRange(itens);
                    await _context.SaveChangesAsync();
                }
                return itens.Count;
            }
            catch
            {
                throw new ApplicationException("Não foi possível remover todos os itens.");
            }
        }
    }
}
