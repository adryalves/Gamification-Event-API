namespace GamificationEvent.API.DTOs.Evento
{
    public class EventoRequestDTO
    {
        public Guid IdPaleta { get; set; }

        public string Titulo { get; set; } = null!;

        public string Descricao { get; set; } = null!;

        public string Objetivo { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public string PublicoAlvo { get; set; } = null!;

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

    }
}
