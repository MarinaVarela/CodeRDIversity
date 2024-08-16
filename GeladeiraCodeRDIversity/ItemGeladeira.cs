namespace GeladeiraIoT
{
    class ItemGeladeira : ElementoGeladeira
    {
        public string Nome { get; set; }

        public ItemGeladeira(int andar, int container, int posicao, string nome)
        {
            Andar = andar;
            Container = container;
            Posicao = posicao;
            Nome = nome;
        }

        public override string Descricao()
        {
            return $"Andar {Andar}, Container {Container}, Posição {Posicao}: {Nome}";
        }
    }
}
