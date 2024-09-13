using System.ComponentModel.DataAnnotations;

namespace ApiRefrigerator.DTOs
{
    public class UpdateRefrigeratorItemDTO
    {
        [Required]
        public int Id { get; set; }

        [Range(1, 3, ErrorMessage = "The floor must be a value between 1 and 3.")]
        public int Floor { get; set; }

        [Range(1, 3, ErrorMessage = "The container must be a value between 1 and 3.")]
        public int Container { get; set; }

        [Range(1, 4, ErrorMessage = "The position must be a value between 1 and 4.")]
        public int Position { get; set; }

        [Required(ErrorMessage = "The name is mandatory.")]
        [StringLength(100, ErrorMessage = "The name must have a maximum of 100 characters.")]
        public required string Name { get; set; }
    }
}
