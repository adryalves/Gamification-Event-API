using GamificationEvent.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.SubEvento
{
    public class SubEventoUpdateDTO
    {
        public Guid? IdPontoMapa { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        public string? LocalSubEvento { get; set; }

        public string? Assunto { get; set; }

        public string? Tipo { get; set; }

        public string? Categoria { get; set; }

        [Required]
        public Modalidade Modalidade { get; set; }

        [Required]
        public DateTime DataSubEvento { get; set; }

        [Required]
        public TimeOnly HorarioInicio { get; set; }

        public TimeOnly? HorarioFim { get; set; }

        public List<PalestrantesSubEventoDTO> Palestrantes { get; set; } = new();
    }
}

public class PalestrantesSubEventoDTO
{
    public Guid Id { get; set; }
    [Required]
    public Guid IdPalestrante { get; set; }

}


