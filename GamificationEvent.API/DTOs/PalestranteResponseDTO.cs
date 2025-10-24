namespace GamificationEvent.API.DTOs
{
    public class PalestranteResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public string Nome { get; set; } = null!;

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public string? Profissao { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string? Linkedin { get; set; }

        public bool Deletado { get; set; }
    }
}
