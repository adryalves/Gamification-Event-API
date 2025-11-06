using GamificationEvent.API.DTOs.Evento;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.EventoUseCases;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

                if (eventoDTO.IdPaleta == Guid.Empty ||
                    String.IsNullOrEmpty(eventoDTO.Titulo) || String.IsNullOrEmpty(eventoDTO.Descricao) ||
                    String.IsNullOrEmpty(eventoDTO.Objetivo) || String.IsNullOrEmpty(eventoDTO.Categoria) ||
                    String.IsNullOrEmpty(eventoDTO.PublicoAlvo) || eventoDTO.DataInicio == null ||
                    eventoDTO.DataFinal == null)
                {
                    return BadRequest("Todos os campos devem possuir valores válidos");

                }

                var evento = eventoDTO.ConverterParaEventoCore();
                var novoEvento = await _cadastrarEventoUseCase.CadastrarEvento(evento);

                if(novoEvento.Sucesso) return Ok(novoEvento.Valor);

                return BadRequest(new { Erro = novoEvento.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarEvento/{id}")]
        public async Task<IActionResult> AtualizarEvento([FromRoute]Guid id, [FromBody] EventoUpdateDTO eventoDTO)
        {
            try {

                if ( eventoDTO.IdPaleta == Guid.Empty ||
                    String.IsNullOrEmpty(eventoDTO.Titulo) || String.IsNullOrEmpty(eventoDTO.Descricao) ||
                    String.IsNullOrEmpty(eventoDTO.Objetivo) || String.IsNullOrEmpty(eventoDTO.Categoria) ||
                    String.IsNullOrEmpty(eventoDTO.PublicoAlvo) || eventoDTO.DataInicio == null ||
                    eventoDTO.DataFinal == null)
                {
                    return BadRequest("Todos os campos devem possuir valores válidos");

                }

                var evento = eventoDTO.ConverterUpdateParaEventoCore();
                evento.Id = id;
                evento.Deletado = false;

                var resultado = await _atualizarEventoUseCase.AtualizarEvento(evento);

                if(resultado.Sucesso) return Ok("Evento atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                {
                    return NotFound(new { Erro = resultado.MensagemDeErro });
                }
              
                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarEvento/{id}")]
        public async Task<IActionResult> DeletarEvento([FromRoute]Guid id)
        {
            try {

                if (id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var resultado = await _deletarEventoUseCase.DeletarEvento(id);

                if(resultado.Sucesso) return Ok("Evento deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });
                
               
                return BadRequest(new { Erro = resultado.MensagemDeErro });
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

                var eventosDTO = new List<EventoResponseDTO>();

                if (eventos.Sucesso)
                {
                    foreach (var evento in eventos.Valor)
                    {
                        var eventoDTO = evento.ConverterParaEventoResponse();
                        eventosDTO.Add(eventoDTO);
                    }

                    return Ok(eventosDTO);
                }
       
                return BadRequest(new { Erro = eventos.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetEventoPorId")]
        public async Task<IActionResult> GetEventoPorId([FromQuery]Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um Id válido");

                var evento = await _getEventoPorIdUseCase.GetEventoPorId(id);

                if (evento.Valor == null) return NotFound("Não foi encontrado um evento válido com esse Id");

                if (evento.Sucesso)
                {
                    var eventoDTO = evento.Valor.ConverterParaEventoResponse();
                    return Ok(eventoDTO);
                }


                return BadRequest(new { Erro = evento.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
}
