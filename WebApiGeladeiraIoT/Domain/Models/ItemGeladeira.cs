using System.ComponentModel.DataAnnotations;

namespace ApiGeladeira.Models
{
    public class ItemGeladeira
    {
        [Key]
        public int Id { get; set; }

        [Range(1, 3, ErrorMessage = "O andar deve ser um valor entre 1 e 3.")]
        public int Andar { get; set; }

        [Range(1, 3, ErrorMessage = "O container deve ser um valor entre 1 e 3.")]
        public int Container { get; set; }

        [Range(1, 4, ErrorMessage = "A posição deve ser um valor entre 1 e 4.")]
        public int Posicao { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public required string Nome { get; set; }
    }
}
