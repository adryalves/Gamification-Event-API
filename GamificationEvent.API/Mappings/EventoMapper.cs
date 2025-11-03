using GamificationEvent.API.DTOs.Evento;
using GamificationEvent.Core.Entidades;
using System.Runtime.CompilerServices;

namespace GamificationEvent.API.Mappings
{
    public static class EventoMapper
    {
        public static Evento ConverterParaEventoCore(this EventoRequestDTO eventoDTO)
        {
            return new Evento
            {
                IdPaleta = eventoDTO.IdPaleta,
                Titulo = eventoDTO.Titulo,
                Descricao = eventoDTO.Descricao,
                Objetivo = eventoDTO.Objetivo,
                Categoria = eventoDTO.Categoria,
                PublicoAlvo = eventoDTO.PublicoAlvo,
                DataInicio = eventoDTO.DataInicio,
                DataFinal = eventoDTO.DataFinal,
            };
        }

        public static Evento ConverterUpdateParaEventoCore(this EventoUpdateDTO eventoDTO)
        {
            return new Evento
            {
                IdPaleta = eventoDTO.IdPaleta,
                Titulo = eventoDTO.Titulo,
                Descricao = eventoDTO.Descricao,
                Objetivo = eventoDTO.Objetivo,
                Categoria = eventoDTO.Categoria,
                PublicoAlvo = eventoDTO.PublicoAlvo,
                DataInicio = eventoDTO.DataInicio,
                DataFinal = eventoDTO.DataFinal,
            };
        }


        public static EventoResponseDTO ConverterParaEventoResponse(this Evento evento)
        {
            return new EventoResponseDTO
            {
                Id = evento.Id,
                IdPaleta = evento.IdPaleta,
                Titulo = evento.Titulo,
                Descricao = evento.Descricao,
                Objetivo = evento.Objetivo,
                Categoria = evento.Categoria,
                PublicoAlvo = evento.PublicoAlvo,
                DataInicio = evento.DataInicio,
                DataFinal = evento.DataFinal,
                Deletado = evento.Deletado,
            };
        }
    }
}
