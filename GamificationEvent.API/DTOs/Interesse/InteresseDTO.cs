namespace GamificationEvent.API.DTOs.Interesse
{
    public class InteresseDTO
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public string Nome { get; set; } = null!;

        public bool Deletado { get; set; }
    }
}
