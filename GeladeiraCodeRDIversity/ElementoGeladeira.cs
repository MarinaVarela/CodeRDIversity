namespace GeladeiraIoT
{
    abstract class ElementoGeladeira
    {
        public int Andar { get; protected set; }
        public int Container { get; protected set; }
        public int Posicao { get; protected set; }

        public abstract string Descricao();
    }
}
