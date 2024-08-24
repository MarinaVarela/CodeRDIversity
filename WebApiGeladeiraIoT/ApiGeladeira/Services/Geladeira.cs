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

        public string AdicionarElemento(ElementoGeladeira elemento)
        {
            if (elemento.Posicao < 1 || elemento.Posicao > 4)
                return $"Adicionar na posição {elemento.Posicao} é inválido. As posições válidas são de 1 a 4.";

            var itemExistente = _itens
                .FirstOrDefault(i => i.Andar == elemento.Andar && i.Container == elemento.Container && i.Posicao == elemento.Posicao);

            if (itemExistente != null)
                return $"Posição {elemento.Posicao} no andar {elemento.Andar} e container {elemento.Container} já está ocupada.";

            _itens.Add((ItemGeladeira)elemento);
            return $"Item adicionado na posição {elemento.Posicao}, andar {elemento.Andar}, container {elemento.Container}.";
        }

        public string AtualizarElemento(int andarNumero, int containerNumero, int posicao, string novoNome)
        {
            var item = _itens
                .FirstOrDefault(i => i.Andar == andarNumero && i.Container == containerNumero && i.Posicao == posicao);

            if (item == null)
                return $"Não existe nenhum item na posição {posicao} do andar {andarNumero} e container {containerNumero}.";

            if (string.IsNullOrWhiteSpace(novoNome))
                return "O nome do item não pode ser vazio ou nulo.";

            item.Nome = novoNome;
            return $"Item na posição {posicao}, andar {andarNumero} e container {containerNumero} atualizado com sucesso.";
        }

        public string RemoverElemento(int andarNumero, int containerNumero, int posicao)
        {
            var item = _itens
                .FirstOrDefault(i => i.Andar == andarNumero && i.Container == containerNumero && i.Posicao == posicao);

            if (item == null)
                return $"Não existe nenhum item na posição {posicao} do andar {andarNumero} e container {containerNumero}.";

            _itens.Remove(item);
            return $"Item removido da posição {posicao}, andar {andarNumero} e container {containerNumero}.";
        }

        public string RemoverTodosElementos()
        {
            int quantidadeRemovida = _itens.Count;

            if (quantidadeRemovida == 0)
                return "Nenhum item para remover.";

            _itens.Clear();

            return $"{quantidadeRemovida} item(ns) removido(s) da geladeira.";
        }
    }
}
