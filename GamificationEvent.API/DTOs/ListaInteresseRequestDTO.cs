namespace GamificationEvent.API.DTOs
{
    public class ListaInteresseRequestDTO
    {
        public Guid IdEvento { get; set; }
        public List<InteresseRequestDTO> InteressesDTO { get; set; } = new ();
    }
    public class InteresseRequestDTO
    {
         public string Nome { get; set; } = null!;
    }
}
