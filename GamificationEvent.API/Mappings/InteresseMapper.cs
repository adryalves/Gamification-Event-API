using GamificationEvent.API.DTOs.Interesse;
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

        public static ListaInteresseResponseDTO ConverterListaParaResponse(this List<Interesse> interesses, Guid idEvento)
        {
        
            if (interesses != null && interesses.Count != 0)
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
            return new ListaInteresseResponseDTO { IdEvento = idEvento};
        }

        public static List<InteresseResponseDTO> ConverterListaParaListaResponse(this List<Interesse> interesses)
        {
            var listaInteresseDTO = interesses.Select(i => new InteresseResponseDTO
            {
                Id = i.Id,
                Nome = i.Nome,
                Deletado = i.Deletado,
            }).ToList();

            return listaInteresseDTO;

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
