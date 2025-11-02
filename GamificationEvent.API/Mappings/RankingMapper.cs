using GamificationEvent.API.DTOs.Ranking;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class RankingMapper
    {
        public static List<RankingDTO> ConverterParaDTO(this List<Ranking> rankings)
        {
            return rankings.Select(r => new RankingDTO
            {
                IdParticipante = r.IdParticipante,
                Foto = r.Foto,
                Nome = r.Nome,
                Pontuacao = r.Pontuacao,
                Email = r.Email,
                Posicao = r.Posicao
            }).ToList();

        }
    }
}
