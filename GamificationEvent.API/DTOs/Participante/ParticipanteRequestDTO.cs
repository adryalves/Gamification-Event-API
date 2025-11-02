using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs.Participante
{
    public class ParticipanteRequestDTO
    {

        public Guid IdEvento { get; set; }

        public Guid IdUsuario { get; set; }

        public Cargo Cargo { get; set; }

        public int Pontuacao { get; set; }

        public bool? PrimeiroParticipante { get; set; }

        public List<ParticipanteInteresseDTO> ParticipanteInteresses { get; set; } = new();
    }

    public class ParticipanteInteresseDTO
    {
        public Guid IdInteresse { get; set; }

    }
}
