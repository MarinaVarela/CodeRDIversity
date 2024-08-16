using System;
using System.Collections.Generic;

namespace GeladeiraIoT
{
    class Geladeira
    {
        private List<Andar> _andares;

        public Geladeira()
        {
            _andares = new List<Andar>();
        }

        public void Inicializar()
        {
            AdicionarElemento(new ItemGeladeira(1, 1, 1, "Manga"));
            AdicionarElemento(new ItemGeladeira(1, 1, 2, "Abacaxi"));
            AdicionarElemento(new ItemGeladeira(1, 1, 3, "Laranja"));
            AdicionarElemento(new ItemGeladeira(1, 1, 4, "Morango"));
            AdicionarElemento(new ItemGeladeira(1, 2, 1, "Banana"));
            AdicionarElemento(new ItemGeladeira(1, 2, 2, "Tomate"));
            AdicionarElemento(new ItemGeladeira(1, 2, 3, "Manjericão"));
            AdicionarElemento(new ItemGeladeira(1, 2, 4, "Alface"));

            AdicionarElemento(new ItemGeladeira(2, 1, 1, "Leite"));
            AdicionarElemento(new ItemGeladeira(2, 1, 2, "Queijo Coalho"));
            AdicionarElemento(new ItemGeladeira(2, 1, 3, "Iogurte"));
            AdicionarElemento(new ItemGeladeira(2, 1, 4, "Manteiga"));
            AdicionarElemento(new ItemGeladeira(2, 2, 1, "Kitute"));
            AdicionarElemento(new ItemGeladeira(2, 2, 2, "Milho Verde"));
            AdicionarElemento(new ItemGeladeira(2, 2, 3, "Sardinha"));
            AdicionarElemento(new ItemGeladeira(2, 2, 4, "Pêssego em Caldas"));

            AdicionarElemento(new ItemGeladeira(3, 1, 1, "Maminha"));
            AdicionarElemento(new ItemGeladeira(3, 1, 2, "Assinha"));
            AdicionarElemento(new ItemGeladeira(3, 1, 3, "Linguiça"));
            AdicionarElemento(new ItemGeladeira(3, 1, 4, "Bacon"));
            AdicionarElemento(new ItemGeladeira(3, 2, 1, "Picanha"));
            AdicionarElemento(new ItemGeladeira(3, 2, 2, "Peito de Frango"));
            AdicionarElemento(new ItemGeladeira(3, 2, 3, "Alcatra"));
            AdicionarElemento(new ItemGeladeira(3, 2, 4, "Ovos"));
        }

        public void AdicionarElemento(ElementoGeladeira elemento)
        {
            var andar = _andares.Find(a => a.Numero == elemento.Andar);
            if (andar == null)
            {
                andar = new Andar(elemento.Andar);
                _andares.Add(andar);
            }

            andar.AdicionarElemento(elemento);
        }

        public void RemoverElemento(int andarNumero, int containerNumero, int posicao)
        {
            var andar = _andares.Find(a => a.Numero == andarNumero);
            if (andar != null)
                andar.RemoverElemento(containerNumero, posicao);

        }

        public void AdicionarItensContainer(int andarNumero, int containerNumero, List<ElementoGeladeira> elementos)
        {
            foreach (var elemento in elementos)
                AdicionarElemento(elemento);
        }

        public void RemoverItensContainer(int andarNumero, int containerNumero)
        {
            var andar = _andares.Find(a => a.Numero == andarNumero);
            if (andar != null)
                andar.RemoverTodosElementosContainer(containerNumero);

        }
    }
}
