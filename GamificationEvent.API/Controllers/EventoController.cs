using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.EventoUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly AtualizarEventoUseCase _atualizarEventoUseCase;
        private readonly CadastrarEventoUseCase _cadastrarEventoUseCase;
        private readonly DeletarEventoUseCase _deletarEventoUseCase;
        private readonly GetEventoPorIdUseCase _getEventoPorIdUseCase;
        private readonly GetEventosUseCase _getEventosUseCase;

        public EventoController(AtualizarEventoUseCase atualizarEventoUseCase, CadastrarEventoUseCase cadastrarEventoUseCase, DeletarEventoUseCase deletarEventoUseCase, GetEventoPorIdUseCase getEventoPorIdUseCase, GetEventosUseCase getEventosUseCase)
        {
            _atualizarEventoUseCase = atualizarEventoUseCase;
            _cadastrarEventoUseCase = cadastrarEventoUseCase;
            _deletarEventoUseCase = deletarEventoUseCase;
            _getEventoPorIdUseCase = getEventoPorIdUseCase;
            _getEventosUseCase = getEventosUseCase;
        }

        [HttpPost("CadastrarEvento")]
        public async Task<IActionResult> CadastrarEvento(EventoRequestDTO eventoDTO)
        {
            try {

                if (eventoDTO.IdPaleta == null || eventoDTO.IdPaleta == Guid.Empty ||
                    String.IsNullOrEmpty(eventoDTO.Titulo) || String.IsNullOrEmpty(eventoDTO.Descricao) ||
                    String.IsNullOrEmpty(eventoDTO.Objetivo) || String.IsNullOrEmpty(eventoDTO.Categoria) ||
                    String.IsNullOrEmpty(eventoDTO.PublicoAlvo) || eventoDTO.DataInicio == null ||
                    eventoDTO.DataFinal == null)
                {
                    return BadRequest("Todos os campos devem possuir valores válidos");

                }

                var evento = eventoDTO.ConverterParaEventoCore();
                var novoEvento = await _cadastrarEventoUseCase.CadastrarEvento(evento);

                return Ok(novoEvento.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarEvento")]
        public async Task<IActionResult> AtualizarEvento(Guid id, [FromBody] EventoRequestDTO eventoDTO)
        {
            try {

                if (eventoDTO.IdPaleta == null || eventoDTO.IdPaleta == Guid.Empty ||
                    String.IsNullOrEmpty(eventoDTO.Titulo) || String.IsNullOrEmpty(eventoDTO.Descricao) ||
                    String.IsNullOrEmpty(eventoDTO.Objetivo) || String.IsNullOrEmpty(eventoDTO.Categoria) ||
                    String.IsNullOrEmpty(eventoDTO.PublicoAlvo) || eventoDTO.DataInicio == null ||
                    eventoDTO.DataFinal == null)
                {
                    return BadRequest("Todos os campos devem possuir valores válidos");

                }

                var evento = eventoDTO.ConverterParaEventoCore();
                evento.Id = id;
                evento.Deletado = false;

                var sucesso = await _atualizarEventoUseCase.AtualizarEvento(evento);

                if (!sucesso)
                    return BadRequest("Algo deu errado na atualização");

                return Ok("Usuario atualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarEvento")]
        public async Task<IActionResult> DeletarEvento(Guid id)
        {
            try {

                if (id == Guid.Empty || id == null)
                    return BadRequest("Insira um id válido");

                var deleção = await _deletarEventoUseCase.DeletarEvento(id);

                if (!deleção)
                {
                    return NotFound("Evento não encontrado para ser deletado");
                }

                return Ok("Evento deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetEventos")]
        public async Task<IActionResult> GetEventos()
        {
            try
            {
                var eventos = await _getEventosUseCase.GetEventos();
                if (eventos == null || eventos.Count == 0)
                    return NotFound("Não há eventos cadastrados");

                var eventosDTO = new List<EventoResponseDTO>();

                foreach (var evento in eventos)
                {
                    var eventoDTO = evento.ConverterParaEventoResponse();
                    eventosDTO.Add(eventoDTO);
                }

                return Ok(eventosDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetEventoPorId")]
        public async Task<IActionResult> GetEventoPorId(Guid id)
        {
            try
            {
                var evento = await _getEventoPorIdUseCase.GetEventoPorId(id);

                if (evento == null)
                    return NotFound("Não há evento com esse Id");

                    var eventoDTO = evento.ConverterParaEventoResponse();

                return Ok(eventoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
}
