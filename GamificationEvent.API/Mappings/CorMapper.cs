using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class CorMapper
    {
        public static Cor ConverterCorCore(this CorRequestDTO corRequestDTO)
        {
            return new Cor
            {
                HexCodigo = corRequestDTO.HexCodigo,
                Nome = corRequestDTO.Nome
            };
        }

        public static CorResponseDTO ConverterCorResponse(this Cor cor)
        {
            return new CorResponseDTO
            {
                Id = cor.Id,
                HexCodigo = cor.HexCodigo,
                Nome = cor.Nome
            };
        }
    }
}
