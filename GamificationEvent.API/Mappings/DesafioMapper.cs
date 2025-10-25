using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class DesafioMapper
    {
        public static Desafio ConverterRequestParaCore(this DesafioRequestDTO desafioDTO)
        {
            return new Desafio
            {
                IdEvento = desafioDTO.IdEvento,
                Nome = desafioDTO.Nome,
                Descricao = desafioDTO.Descricao,
                Regra = desafioDTO.Regra,
                Pontuacao = desafioDTO.Pontuacao,
                TipoDesafio = desafioDTO.TipoDesafio,
                QuantidadeDesafio = desafioDTO.QuantidadeDesafio,
                DataHoraInicio = desafioDTO.DataHoraInicio,
                DataHoraFim = desafioDTO.DataHoraFim,
            };
        }

        public static Desafio ConverterUpdateParaCore(this DesafioUpdateDTO desafioDTO) {

            return new Desafio
            {
                Nome = desafioDTO.Nome,
                Descricao = desafioDTO.Descricao,
                Regra = desafioDTO.Regra,
                Pontuacao = desafioDTO.Pontuacao,
                TipoDesafio = desafioDTO.TipoDesafio,
                QuantidadeDesafio = desafioDTO.QuantidadeDesafio,
                DataHoraInicio = desafioDTO.DataHoraInicio,
                DataHoraFim = desafioDTO.DataHoraFim
            };
        }

        public static DesafioResponseDTO ConverterDesafioParaResponse(this Desafio desafio) {

            return new DesafioResponseDTO
            {
                Id = desafio.Id,
                IdEvento = desafio.IdEvento,
                Nome = desafio.Nome,
                Descricao = desafio.Descricao,
                Regra = desafio.Regra,
                Pontuacao = desafio.Pontuacao,
                TipoDesafio = desafio.TipoDesafio,
                QuantidadeDesafio = desafio.QuantidadeDesafio,
                DataHoraInicio = desafio.DataHoraInicio,
                DataHoraFim = desafio.DataHoraFim,
                Deletado = desafio.Deletado
            };
        }

        public static List<DesafioResponseDTO> ConverterListaParaResponse(this List<Desafio> desafioLista) {
            return desafioLista.Select(d => d.ConverterDesafioParaResponse()).ToList();

        }


    }
}
