using ApiGeladeira.DTOs;
using System.Xml.Linq;

namespace ApiGeladeira.Services
{
    public class Geladeira
    {
        private readonly List<ItemGeladeira> _itens = new();

        public List<string>? ObterItens()
        {
            var itens = _itens.Select(item => item.Descricao()).ToList();
            if (itens.Count == 0)
                return null;

            return itens;
        }

        public string? ObterItemPorNome(string nome)
        {
            var item = _itens.FirstOrDefault(i => i.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

            if (item == null)
                return null;

            return item.Descricao();
        }

        public string AdicionarElemento(ElementoGeladeira elemento)
        {
            if (elemento.Posicao < 1 || elemento.Posicao > 4)
                return $"A geladeira só tem quatro posições. E a {elemento.Posicao} não é válida.";

            var itemExistente = _itens
                .FirstOrDefault(i => i.Andar == elemento.Andar && i.Container == elemento.Container && i.Posicao == elemento.Posicao);

            if (itemExistente != null)
                return $"Essa posição já está ocupada.";

            _itens.Add((ItemGeladeira)elemento);
            var itemAdicionado = _itens.Last();

            return $"{itemAdicionado.Nome} foi adicionado(a) na geladeira.";
        }

        public string AtualizarElemento(UpdateItemGeladeiraDTO atualizarItem)
        {
            var itemExistente = _itens
                .FirstOrDefault(i => i.Andar == atualizarItem.Andar && i.Container == atualizarItem.Container && i.Posicao == atualizarItem.Posicao);

            if (itemExistente is null)
                return $"Não existe esse item na geladeira. Não foi possível atualizar a geladeira.";

            itemExistente.Nome = atualizarItem.Nome;
            return $"{itemExistente.Nome} foi atualizado para o item {atualizarItem.Nome} na geladeira.";
        }

        public string RemoverElemento(int andarNumero, int containerNumero, int posicao)
        {
            var item = _itens
                .FirstOrDefault(i => i.Andar == andarNumero && i.Container == containerNumero && i.Posicao == posicao);

            if (item == null)
                return $"Não existe esse item na geladeira.";

            _itens.Remove(item);
            return $"{item.Nome} foi removido da posição {posicao}, andar {andarNumero} e container {containerNumero} da geladeira.";
        }

        public string RemoverTodosElementos()
        {
            int quantidadeRemovida = _itens.Count;

            if (quantidadeRemovida == 0)
                return "Nenhum item para remover.";

            _itens.Clear();

            return $"Faxina feita. {quantidadeRemovida} item(ns) foram removido(s) da geladeira.";
        }
    }
}
