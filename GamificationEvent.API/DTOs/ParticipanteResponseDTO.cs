using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs
{
    public class ParticipanteResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdEvento { get; set; }

        public Guid IdUsuario { get; set; }

        public Cargo Cargo { get; set; }

        public int Pontuacao { get; set; }

        public bool? PrimeiroParticipante { get; set; }

        public DateTime DataHoraCriacao { get; set; }

        public List<ParticipanteInteresseResponseDTO> ParticipanteInteresses { get; set; } = new();
    }
    public class ParticipanteInteresseResponseDTO
    {
        public Guid Id { get; set; }
        public Guid IdInteresse { get; set; }
    }
}
