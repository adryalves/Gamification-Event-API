namespace GamificationEvent.API.DTOs
{
    public class ListaInteresseResponseDTO
    {
        public Guid IdEvento { get; set; }
        public List<InteresseResponseDTO> InteressesDTO { get; set; } = new();
    }

    public class InteresseResponseDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = null!;
        public bool Deletado { get; set; }
    }
}
