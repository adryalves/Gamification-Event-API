using GamificationEvent.API.DTOs.ParticipantePremio;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class ParticipantePremioMapper
    {
        public static ParticipantePremio ConverterDeRequestParaCore(this ParticipantePremioRequestDTO requestDTO)
        {
            return new ParticipantePremio
            {
                IdParticipante = requestDTO.IdParticipante,
                IdPremio = requestDTO.IdPremio,
                Motivo = requestDTO.Motivo,
                DataConcessao = requestDTO.DataConcessao,
            };
        }

        public static ParticipantePremio ConverterDeUpdateParaCore(this ParticipantePremioUpdateDTO updateDTO)
        {
            return new ParticipantePremio
            {
                Motivo = updateDTO.Motivo,
                DataConcessao = updateDTO.DataConcessao,
            };
        }
        public static List<ParticipantePremioResponseDTO> ConverterListaPararesponse(this List<ParticipantePremio> participantePremios)
        {
            return participantePremios.Select(x => new ParticipantePremioResponseDTO
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdPremio = x.IdPremio,
                Motivo = x.Motivo,
                DataConcessao = x.DataConcessao
            }).ToList();
        }
        public static ParticipantePremioResponseDTO ConverterParaResponse(this ParticipantePremio participantePremio)
        {
            return new ParticipantePremioResponseDTO
            {
                Id = participantePremio.Id,
                IdParticipante = participantePremio.IdParticipante,
                IdPremio = participantePremio.IdPremio,
                Motivo = participantePremio.Motivo,
                DataConcessao = participantePremio.DataConcessao
            };
        }
    }
}
