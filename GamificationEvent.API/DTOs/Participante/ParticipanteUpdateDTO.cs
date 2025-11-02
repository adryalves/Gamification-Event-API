using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs.Participante
{
    public class ParticipanteUpdateDTO
    {
        public Cargo Cargo { get; set; }
        public int Pontuacao { get; set; }
        public bool? PrimeiroParticipante { get; set; }
        public List<ParticipanteInteresseDTO> ParticipanteInteresses { get; set; } = new();
    }
}
