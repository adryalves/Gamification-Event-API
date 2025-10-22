namespace GamificationEvent.API.DTOs
{
    public class PremioUpdateDTO
    {

        public Guid? IdPatrocinador { get; set; }

        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        public string? Tipo { get; set; }

        public string? InfoResgate { get; set; }

    }
}
