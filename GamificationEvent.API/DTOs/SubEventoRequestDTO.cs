using GamificationEvent.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs
{
    public class SubEventoRequestDTO
    {
       
        public Guid IdEvento { get; set; }

        public Guid? IdPontoMapa { get; set; }

        public string Nome { get; set; } = null!;

        public string? LocalSubEvento { get; set; }

        public string? Assunto { get; set; }

        public string? Tipo { get; set; }

        public string? Categoria { get; set; }

        public Modalidade Modalidade { get; set; }

        public DateTime DataSubEvento { get; set; }

        public TimeOnly HorarioInicio { get; set; }

        public TimeOnly? HorarioFim { get; set; }

        public List<PalestrantesSubEventoRequestDTO> Palestrantes { get; set; } = new();
    }
}

public class PalestrantesSubEventoRequestDTO
{
    public Guid IdPalestrante { get; set; }
}