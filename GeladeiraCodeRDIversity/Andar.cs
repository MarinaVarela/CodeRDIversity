namespace GeladeiraIoT
{
    class Andar
    {
        public int Numero { get; private set; }
        private List<Container> _containers;

        public Andar(int numero)
        {
            Numero = numero;
            _containers = new List<Container>();
        }

        public void AdicionarElemento(ElementoGeladeira elemento)
        {
            var container = _containers.Find(c => c.Numero == elemento.Container);
            if (container == null)
                container = new Container(Numero, elemento.Container);
            _containers.Add(container);

            container.AdicionarElemento(elemento);
        }

        public void RemoverElemento(int containerNumero, int posicao)
        {
            var container = _containers.Find(c => c.Numero == containerNumero);
            if (container != null)
                container.RemoverElemento(posicao);
        }

        public void RemoverTodosElementosContainer(int containerNumero)
        {
            var container = _containers.Find(c => c.Numero == containerNumero);
            if (container != null)
                container.RemoverTodosElementos();

        }
    }
}
