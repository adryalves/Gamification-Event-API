using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Interesse
{
    public class ListaInteresseRequestDTO
    {
        [Required]
        public Guid IdEvento { get; set; }
        public List<InteresseRequestDTO> InteressesDTO { get; set; } = new();
    }
    public class InteresseRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;
    }
}
