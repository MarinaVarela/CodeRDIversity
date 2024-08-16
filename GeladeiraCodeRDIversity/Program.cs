namespace GeladeiraIoT
{
    class Program
    {
        static void Main()
        {
            Geladeira geladeira = new();

            geladeira.Inicializar();

            // Teste de retirada duplicada
            geladeira.RemoverElemento(1, 1, 2);
            geladeira.RemoverElemento(1, 1, 2);

            // Adição de fruta no lugar removido
            geladeira.AdicionarElemento(new ItemGeladeira(1, 1, 2, "Jabuticaba"));

            // Teste de adição de espaço já ocupado
            geladeira.AdicionarElemento(new ItemGeladeira(1, 1, 4, "Melancia"));

            // Testes pra adicionar e remover em posições inválidas
            geladeira.AdicionarElemento(new ItemGeladeira(1, 1, 10, "Jamelão"));
            geladeira.RemoverElemento(1, 1, 8);

            // Remoção de todos itens de um contianer
            geladeira.RemoverItensContainer(2, 2);

            // Adicão de dois elementos, no container que antes foi todo removido
            geladeira.AdicionarItensContainer(2, 2, [
                new ItemGeladeira(2, 2, 1, "Cenoura"),
                new ItemGeladeira(2, 2, 2, "Beterraba"),
            ]);
        }
    }
}

/*
 Minha estrutura de dados foi alterada de Queue para List, com métodos mais usuais de Add e Remove.

 No Program, fiz algumas testes para forçar o erro, como adicionar e remover elementos em posições que não são válidas,
 remover todos de um container específico,; adicionar mais de um item, etc.
*/

// Exercício por Marina Varela