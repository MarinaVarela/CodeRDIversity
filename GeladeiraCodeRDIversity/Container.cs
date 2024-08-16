namespace GeladeiraIoT
{
    class Container
    {
        public int Andar { get; private set; }
        public int Numero { get; private set; }
        private List<ElementoGeladeira> _elementos;

        public Container(int andar, int numero)
        {
            Andar = andar;
            Numero = numero;
            _elementos = new List<ElementoGeladeira>();
        }

        public void AdicionarElemento(ElementoGeladeira elemento)
        {
            var itemExistente = _elementos.Find(e => e.Posicao == elemento.Posicao);
            if (elemento.Posicao < 1 || elemento.Posicao > 4)
            {
                Console.WriteLine($"Adicionar na posição {elemento.Posicao} é inválido. As posições válidas são de 1 a 4.");
            }
            else if (itemExistente != null)
            {
                Console.WriteLine($"Posição {elemento.Posicao} no Andar {Andar}, Container {Numero} já está ocupada.");
            }
            else
            {
                _elementos.Add(elemento);
                Console.WriteLine($"{elemento.Descricao()} adicionado.");
            }
        }

        public void RemoverElemento(int posicao)
        {
            var item = _elementos.Find(e => e.Posicao == posicao);

            if (posicao < 1 || posicao > 4)
            {
                Console.WriteLine($"Remover na posição {posicao} é inválido. As posições válidas são de 1 a 4.");
            }
            else if (item != null)
            {
                _elementos.Remove(item);
                Console.WriteLine($"Elemento removido da Posição {posicao}, Andar {Andar}, Container {Numero}.");
            }
            else
            {
                Console.WriteLine($"Posição {posicao} no Andar {Andar}, Container {Numero} já está vazia.");
            }
        }

        public void RemoverTodosElementos()
        {
            _elementos.Clear();
            Console.WriteLine($"Todos os elementos foram removidos do Container {Numero} no Andar {Andar}.");
        }
    }
}
