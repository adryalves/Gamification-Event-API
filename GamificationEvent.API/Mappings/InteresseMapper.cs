using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class InteresseMapper
    {
        public static List<Interesse> ConverterListaParaCore(this ListaInteresseRequestDTO interessesDTO)
        {
            var interesses = interessesDTO.InteressesDTO.Select(x => new Interesse
            {
                IdEvento = interessesDTO.IdEvento,
                Nome = x.Nome,
            }
                ).ToList();

            return interesses;
        }

        public static ListaInteresseResponseDTO ConverterListaParaResponse(this List<Interesse> interesses)
        {
            var ListaInteresseDTO = new ListaInteresseResponseDTO
            {
                IdEvento = interesses.First().IdEvento,
                InteressesDTO = interesses.Select(i => new InteresseResponseDTO
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Deletado = i.Deletado,
                }).ToList()
            };

            return ListaInteresseDTO;
        }

        public static InteresseDTO ConverterInteresseParaResponse(this Interesse interesse)
        {
            return new InteresseDTO
            {
                Id = interesse.Id,
                IdEvento = interesse.IdEvento,
                Nome = interesse.Nome,
                Deletado = interesse.Deletado,
            };
        }
    }
}
