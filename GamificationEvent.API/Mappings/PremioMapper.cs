using GamificationEvent.API.DTOs.Premio;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class PremioMapper
    {
        public static Premio ConverterRequestParaCore(this PremioRequestDTO premio)
        {
            return new Premio
            {
                IdEvento = premio.IdEvento,
                Nome = premio.Nome,
                Descricao = premio.Descricao,
                Tipo = premio.Tipo,
                InfoResgate = premio.InfoResgate,
            };
        }
        public static Premio ConverterUpdateParaCore(this PremioUpdateDTO premio)
        {
            return new Premio
            {               
                Nome = premio.Nome,
                Descricao = premio.Descricao,
                Tipo = premio.Tipo,
                InfoResgate = premio.InfoResgate,
            };
        }

        public static List<PremioResponseDTO> ConverterParaListaResponse(this List<Premio> premios)
        {
            return premios.Select(x => new PremioResponseDTO
            {
                Id = x.Id,
                IdEvento = x.IdEvento,
                IdPatrocinador = x.IdPatrocinador,
                Nome = x.Nome,
                Descricao = x.Descricao,
                Tipo = x.Tipo,
                InfoResgate = x.InfoResgate,
                Deletado = x.Deletado,
            }).ToList();
        }

        public static PremioResponseDTO ConverterParaResponse(this Premio premio)
        {
            return new PremioResponseDTO
            {
                Id = premio.Id,
                IdEvento = premio.IdEvento,
                IdPatrocinador = premio.IdPatrocinador,
                Nome = premio.Nome,
                Descricao = premio.Descricao,
                Tipo = premio.Tipo,
                InfoResgate = premio.InfoResgate,
                Deletado = premio.Deletado,
            };
        }
    }
}
