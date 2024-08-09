namespace GeladeiraCodeRDIversity
{
    public class ItemGeladeiraModel
    {
        public int Andar { get; }
        public int Container { get; }
        public int Posicao { get; }
        public string Nome { get; }

        public ItemGeladeiraModel(int andar, int container, int posicao, string nome)
        {
            Andar = andar;
            Container = container;
            Posicao = posicao;
            Nome = nome;
        }
    }
}
