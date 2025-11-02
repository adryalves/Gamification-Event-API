using GamificationEvent.API.DTOs.SubEvento;
using GamificationEvent.API.Mappings;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class CheckInSubEventoMapper
    {
        public static QrCodeSubEventoResponseDTO ConverterQrCodeParaCore(this QrCodeSubEvento qrCodeSubEvento)
        {
            return new QrCodeSubEventoResponseDTO
            {
                IdSubEvento = qrCodeSubEvento.IdSubEvento,
                CodigoSubEvento = qrCodeSubEvento.CodigoSubEvento,
                QrCode = qrCodeSubEvento.QrCode
            };
        }

        public static CheckInSubEvento ConverterCheckInSubEventoParaCore(this CheckInSubEventoRequestDTO checkInSubEventoRequestDTO)
        {
            return new CheckInSubEvento
            {
                IdSubEvento = checkInSubEventoRequestDTO.IdSubEvento,
                IdParticipante = checkInSubEventoRequestDTO.IdParticipante
            };
        }

        public static CheckInSubEventoResponseDTO ConverterCheckInParaResponse(this CheckInSubEvento checkInSubEvento)
        {
            return new CheckInSubEventoResponseDTO
            {
                Id = checkInSubEvento.Id,
                IdSubEvento = checkInSubEvento.IdSubEvento,
                IdParticipante = checkInSubEvento.IdParticipante,
                DataHora = checkInSubEvento.DataHora,
            };
        }

        public static List<CheckInSubEventoResponseDTO> ConverterCheckInListaParaResponse(this List<CheckInSubEvento> checkInsSubEvento)
        {
            return checkInsSubEvento.Select(c => c.ConverterCheckInParaResponse()).ToList();
        }
    }
}


