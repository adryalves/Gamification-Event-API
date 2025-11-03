using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.PaletaCor
{
    public class PaletaCorUpdateDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        [Required]
        public Guid IdCor1 { get; set; }

        [Required]
        public Guid IdCor2 { get; set; }

        [Required]
        public Guid IdCor3 { get; set; }

        [Required]
        public Guid IdCor4 { get; set; }
    }
}
