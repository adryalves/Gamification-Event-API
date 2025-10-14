namespace GamificationEvent.API.DTOs
{
    public class UsuarioResponseDTO
    {

        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string? Telefone { get; set; }
        public DateOnly? DataDeNascimento { get; set; }
        public string? Foto { get; set; }
        public DateTime? DataHoraCriacao { get; set; } = DateTime.UtcNow;
        public bool Deletado { get; set; } = false;

        public List<RedeSocialDTO> RedesSociais { get; set; } = new();

    }
}
