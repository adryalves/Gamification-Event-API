namespace GamificationEvent.API.DTOs.Palestrante
{
    public class PalestranteUpdateDTO
    {

        public string Nome { get; set; } = null!;

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public string? Profissao { get; set; }

        public DateOnly? DataNascimento { get; set; }

        public string? Linkedin { get; set; }

    }
}
