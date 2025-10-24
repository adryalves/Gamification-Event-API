using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;


namespace GamificationEvent.API.Mappings
{
    public static class SubEventoMapper
    {
        public static SubEvento ConverterSubEventoRequestParaCore(this SubEventoRequestDTO subEventoDTO)
        {
            return new SubEvento
            {
                IdEvento = subEventoDTO.IdEvento,
                IdPontoMapa = subEventoDTO.IdPontoMapa,
                Nome = subEventoDTO.Nome,
                LocalSubEvento = subEventoDTO.LocalSubEvento,
                Assunto = subEventoDTO.Assunto,
                Tipo = subEventoDTO.Tipo,
                Categoria = subEventoDTO.Categoria,
                Modalidade = subEventoDTO.Modalidade,
                DataSubEvento = subEventoDTO.DataSubEvento,
                HorarioInicio = subEventoDTO.HorarioInicio,
                HorarioFim = subEventoDTO.HorarioFim,
                CodigoCheckin = subEventoDTO.CodigoCheckin,
                Palestrantes = subEventoDTO.Palestrantes?.Select(p => new PalestrantesSubEvento
                {
                    IdPalestrante = p.IdPalestrante
                }).ToList() ?? new()
            };
        }

        public static SubEvento ConverterSubEventoUpdateParaCore(this SubEventoUpdateDTO subEventoDTO)
        {
            return new SubEvento
            {
                IdPontoMapa = subEventoDTO.IdPontoMapa,
                Nome = subEventoDTO.Nome,
                LocalSubEvento = subEventoDTO.LocalSubEvento,
                Assunto = subEventoDTO.Assunto,
                Tipo = subEventoDTO.Tipo,
                Categoria = subEventoDTO.Categoria,
                Modalidade = subEventoDTO.Modalidade,
                DataSubEvento = subEventoDTO.DataSubEvento,
                HorarioInicio = subEventoDTO.HorarioInicio,
                HorarioFim = subEventoDTO.HorarioFim,
                CodigoCheckin = subEventoDTO.CodigoCheckin,
                Palestrantes = subEventoDTO.Palestrantes?.Select(p => new PalestrantesSubEvento
                {
                    Id = p.Id,
                    IdPalestrante = p.IdPalestrante
                }).ToList() ?? new()
            };
        }

        public static SubEventoResponseDTO ConverterSubEventoParaResponse(this SubEvento subEvento)
        {
            return new SubEventoResponseDTO
            {
                Id = subEvento.Id,
                IdEvento = subEvento.IdEvento,
                IdPontoMapa = subEvento.IdPontoMapa,
                Nome = subEvento.Nome,
                LocalSubEvento = subEvento.LocalSubEvento,
                Assunto = subEvento.Assunto,
                Tipo = subEvento.Tipo,
                Categoria = subEvento.Categoria,
                Modalidade = subEvento.Modalidade,
                DataSubEvento = subEvento.DataSubEvento,
                HorarioInicio = subEvento.HorarioInicio,
                HorarioFim = subEvento.HorarioFim,
                CodigoCheckin = subEvento.CodigoCheckin,
                Deletado = subEvento.Deletado,
                Palestrantes = subEvento.Palestrantes?.Select(p => new PalestrantesSubEventoDTO
                {
                    Id = p.Id,
                    IdPalestrante = p.IdPalestrante
                }).ToList() ?? new()
            };
        }

        public static List<SubEventoResponseDTO> ConverterParaSubEventoListaResponse(this List<SubEvento> subEventoLista)
        {
            if (subEventoLista == null || !subEventoLista.Any()) return new();
            return subEventoLista.Select(s => s.ConverterSubEventoParaResponse()).ToList();
        }

        public static PerguntasSubEventoResponseDTO ConverterPerguntasParaResponse(this PerguntasSubEvento pergunta)
        {
            if (pergunta == null) return null!;

            return new PerguntasSubEventoResponseDTO
            {
                Id = pergunta.Id,
                IdParticipante = pergunta.IdParticipante,
                IdSubEvento = pergunta.IdSubEvento,
                Assunto = pergunta.Assunto,
                Pergunta = pergunta.Pergunta,
                DataHora = pergunta.DataHora
            };
        }

        public static List<PerguntasSubEventoResponseDTO> ConverterParaListaResponse(this List<PerguntasSubEvento> perguntas)
        {
            if (perguntas == null || !perguntas.Any())
                return new List<PerguntasSubEventoResponseDTO>();

            return perguntas.Select(p => p.ConverterPerguntasParaResponse()).ToList();
        }

        public static PerguntasSubEvento ConverterPerguntaParaCore(this PerguntasSubEventoRequestDTO perguntaDTO)
        {
            return new PerguntasSubEvento
            {
                IdParticipante = perguntaDTO.IdParticipante,
                IdSubEvento = perguntaDTO.IdSubEvento,
                Assunto = perguntaDTO.Assunto,
                Pergunta = perguntaDTO.Pergunta,
                DataHora = perguntaDTO.DataHora
            };
        }
    }
}
