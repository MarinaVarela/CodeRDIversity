using ApiGeladeira.Models;
using ApiGeladeira.Repository;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Application.Services
{
    public class GeladeiraService
    {
        private readonly GeladeiraRepository _repository;
        public GeladeiraService(GeladeiraRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ItemGeladeira>> ObterItens()
        {
           var itens= await _repository.ObterItens();
            if (!itens.Any()) {
                throw new InvalidDataException();
            }
            return itens;
        }

        public Task<ItemGeladeira?> ObterItemPorId(int id)
        {
            var itemExistente = _repository.ObterItemPorId(id);
            if (itemExistente is null)
                throw new KeyNotFoundException();
            return itemExistente;
        }

        public async Task<ItemGeladeira?> ObterItemPorNome(string nome)
        {
            return await _repository.ObterItemPorNome(nome);
        }

        public async Task<ItemGeladeira?> AdicionarItemAsync(ItemGeladeira item)
        {
            var checarItem = await ObterItemPorNome(item.Nome);
            if (checarItem is not null)
                throw new ApplicationException($"O item {item.Nome} já está na geladeira.");

            var resultado = await _repository.AdicionarItem(item);

            if (resultado is null)
                throw new ApplicationException("Falha ao adicionar o item.");

            return item;
        }

        public async Task<ItemGeladeira?> AtualizarItem(ItemGeladeira item)
        {
            var checarItem = await _repository.ObterItemPorId(item.Id);
            if (checarItem is null)
                throw new ApplicationException($"{item.Nome} não está na geladeira.");

            var resultado = await _repository.AtualizarItem(item);
            if (resultado is null)
                throw new ApplicationException("Falha ao atualizar o item.");

            return item;
        }

        public async Task<ItemGeladeira?> RemoverItem(int id)
        {
            var itemRemovido = await _repository.RemoverItem(id);

            if (itemRemovido == null)
                throw new KeyNotFoundException();

            return itemRemovido;
        }

        public async Task<int> RemoverTodosItens()
        {
            try
            {
                return await _repository.RemoverTodosItens();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("Não foi possível remover todos os itens.", ex);
            }
        }
    }
}
