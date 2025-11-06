using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Evento
{
    public class EventoRequestDTO
    {
        [Required]
        public Guid IdPaleta { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Titulo { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Descricao { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Objetivo { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Categoria { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string PublicoAlvo { get; set; } = null!;

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFinal { get; set; }

    }
}
