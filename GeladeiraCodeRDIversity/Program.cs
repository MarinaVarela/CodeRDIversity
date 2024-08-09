using GeladeiraCodeRDIversity;

namespace GeladeiraIoT
{
    class Program
    {
        static void Main()
        {
            Queue<ItemGeladeiraModel> geladeira = CriarGeladeira();
            ExibirItensGeladeira(geladeira);
        }

        static Queue<ItemGeladeiraModel> CriarGeladeira()
        {
            var geladeira = new Queue<ItemGeladeiraModel>();

            // Andar 1 - Hortifruti
            geladeira.Enqueue(new ItemGeladeiraModel(1, 1, 1, "Manga"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 1, 2, "Abacaxi"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 1, 3, "Laranja"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 1, 4, "Morango"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 2, 1, "Banana"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 2, 2, "Tomate"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 2, 3, "Manjerição"));
            geladeira.Enqueue(new ItemGeladeiraModel(1, 2, 4, "Alface"));

            // Andar 2 - Laticínios e enlatados
            geladeira.Enqueue(new ItemGeladeiraModel(2, 1, 1, "Leite"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 1, 2, "Queijo Coalho"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 1, 3, "Iogurte"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 1, 4, "Manteiga"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 2, 1, "Kitute"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 2, 2, "Milho Verde"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 2, 3, "Sardinha"));
            geladeira.Enqueue(new ItemGeladeiraModel(2, 2, 4, "Pêssego em Caldas"));

            // Andar 3 - Charcutaria, carnes e ovos
            geladeira.Enqueue(new ItemGeladeiraModel(3, 1, 1, "Maminha"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 1, 2, "Assinha"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 1, 3, "Linguiça"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 1, 4, "Bacon"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 2, 1, "Picanha"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 2, 2, "Peito de Frango"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 2, 3, "Alcatra"));
            geladeira.Enqueue(new ItemGeladeiraModel(3, 2, 4, "Ovos"));

            return geladeira;
        }

        static void ExibirItensGeladeira(Queue<ItemGeladeiraModel> geladeira)
        {
            while (geladeira.Count > 0)
            {
                var item = geladeira.Dequeue();
                
                Console.WriteLine($"Andar {item.Andar}, Container {item.Container}, Posição {item.Posicao}: {item.Nome}");
            }
        }
    }
}

/*Para a geladeira, usei a estrutura de dados genérica QUEUE(Fila). Já que itens na geladeira 
 são perecíveis, pensei que oprimeiro a entrar pode estragar mais rápido, 
 logo deve ser o primeiro a sair. :)
*/

// Exercício por Marina Varela