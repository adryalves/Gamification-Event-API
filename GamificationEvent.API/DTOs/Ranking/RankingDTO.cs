namespace GamificationEvent.API.DTOs.Ranking
{
    public class RankingDTO
    {
        public int Posicao { get; set; }
        public Guid IdParticipante { get; set; }
        public string? Foto { get; set; }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public string Email { get; set; } = null!;
    }
}
