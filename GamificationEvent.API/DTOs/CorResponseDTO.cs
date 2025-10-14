namespace GamificationEvent.API.DTOs
{
    public class CorResponseDTO
    {
        public Guid Id { get; set; }
        public string HexCodigo { get; set; } = null!;
        public string? Nome { get; set; }
    }
}
