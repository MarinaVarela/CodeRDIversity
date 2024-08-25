namespace ApiGeladeira.DTOs
{
    public class CreateItemGeladeiraDTO
    {
        public int Andar { get; set; }
        public int Container { get; set; }
        public int Posicao { get; set; }
        public required string Nome { get; set; }
    }

    public class UpdateItemGeladeiraDTO
    {
        public int Andar { get; set; }
        public int Container { get; set; }
        public int Posicao { get; set; }
        public required string Nome { get; set; }
    }
}

