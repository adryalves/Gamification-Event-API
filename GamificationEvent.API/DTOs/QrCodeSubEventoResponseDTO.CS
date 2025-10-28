namespace GamificationEvent.API.DTOs
{
    public class QrCodeSubEventoResponseDTO
    {
        public Guid IdSubEvento { get; set; }
        public string CodigoSubEvento { get; set; }
        public string QrCode { get; set; }
    }
}
