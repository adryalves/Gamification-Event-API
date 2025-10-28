using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs
{
    public class DesafioParticipanteResponseDTO
    {
        public Guid Id { get; set; }

        public Guid IdParticipante { get; set; }

        public Guid IdDesafio { get; set; }

        public Status_Desafio StatusDesafio { get; set; }

        public int QuantidadeRealizada { get; set; }

        public DateTime? DataHoraConclusao { get; set; }
    }
}
