using GamificationEvent.API.DTOs.ParticipantePremio;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.ParticipantePremioUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ParticipantePremioController : ControllerBase
    {
        private readonly AtualizarParticipantePremioUseCase _atualizarParticipantePremioUseCase;
        private readonly CadastrarParticipantePremioUseCase _cadastrarParticipantePremioUseCase;
        private readonly GetParticipantePremioPorIdUseCase _getParticipantePremioPorIdUseCase;
        private readonly GetParticipantePremiosPorIdEventoUseCase _getParticipantePremiosPorIdEventoUseCase;
        private readonly GetParticipantePremiosPorIdParticipanteUseCase _getParticipantePremiosPorIdParticipanteUseCase;
        private readonly GetParticipantesPremioPorIdPremioUseCase _getParticipantesPremioPorIdPremioUseCase;

        public ParticipantePremioController(AtualizarParticipantePremioUseCase atualizarParticipantePremioUseCase, CadastrarParticipantePremioUseCase cadastrarParticipantePremioUseCase, GetParticipantePremioPorIdUseCase getParticipantePremioPorIdUseCase, GetParticipantePremiosPorIdEventoUseCase getParticipantePremiosPorIdEventoUseCase, GetParticipantePremiosPorIdParticipanteUseCase getParticipantePremiosPorIdParticipanteUseCase, GetParticipantesPremioPorIdPremioUseCase getParticipantesPremioPorIdPremioUseCase)
        {
            _atualizarParticipantePremioUseCase = atualizarParticipantePremioUseCase;
            _cadastrarParticipantePremioUseCase = cadastrarParticipantePremioUseCase;
            _getParticipantePremioPorIdUseCase = getParticipantePremioPorIdUseCase;
            _getParticipantePremiosPorIdEventoUseCase = getParticipantePremiosPorIdEventoUseCase;
            _getParticipantePremiosPorIdParticipanteUseCase = getParticipantePremiosPorIdParticipanteUseCase;
            _getParticipantesPremioPorIdPremioUseCase = getParticipantesPremioPorIdPremioUseCase;
        }

        [HttpPost("CadastrarParticipantePremio")]
        public async Task<IActionResult> CadastrarParticipantePremio(ParticipantePremioRequestDTO participantePremioDTO)
        {
            try
            {
                if ( participantePremioDTO.IdParticipante == Guid.Empty
                    || participantePremioDTO.IdPremio == Guid.Empty) return BadRequest("Preencha com Ids válidos");

                var participantePremio = participantePremioDTO.ConverterDeRequestParaCore();

                var resultado = await _cadastrarParticipantePremioUseCase.CadastrarParticipantePremio(participantePremio);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarParticipantePremio/{id}")]
        public async Task<IActionResult> AtualizarParticipantePremio([FromRoute]Guid id, ParticipantePremioUpdateDTO participantePremioDTO)
        {
            try
            {
                if(id == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremio = participantePremioDTO.ConverterDeUpdateParaCore();

                var resultado = await _atualizarParticipantePremioUseCase.AtualizarParticipantePremio(id, participantePremio);
              
                if (resultado.Sucesso) return Ok("Participante Premio Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantePremiosPorIdEvento")]
        public async Task<ActionResult<List<ParticipantePremioResponseDTO>>> GetParticipantePremiosPorIdEvento([FromQuery]Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremios = await _getParticipantePremiosPorIdEventoUseCase.GetParticipantePremioPorIdEvento(idEvento);

                if (participantePremios.Sucesso)
                {
                    var participantePremiosResponse = participantePremios.Valor.ConverterListaParaResponse();
                    return Ok(participantePremiosResponse);
                }

                return BadRequest(new { Erro = participantePremios.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantePremiosPorIdParticipante")]
        public async Task<ActionResult<List<ParticipantePremioResponseDTO>>> GetParticipantePremiosPorIdParticipante([FromQuery] Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremios = await _getParticipantePremiosPorIdParticipanteUseCase.GetParticipantePremiosPorIdParticipante(idParticipante);

                if (participantePremios.Sucesso)
                {
                    var participantePremiosResponse = participantePremios.Valor.ConverterListaParaResponse();
                    return Ok(participantePremiosResponse);
                }

                return BadRequest(new { Erro = participantePremios.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantesPremioPorIdPremio")]
        public async Task<ActionResult<List<ParticipantePremioResponseDTO>>> GetParticipantesPremioPorIdPremio([FromQuery] Guid idPremio)
        {
            try
            {
                if (idPremio == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantesPremio = await _getParticipantesPremioPorIdPremioUseCase.GetParticipantesPremioPorIdPremio(idPremio);

                if (participantesPremio.Sucesso)
                {
                    var participantePremiosResponse = participantesPremio.Valor.ConverterListaParaResponse();
                    return Ok(participantePremiosResponse);
                }

                return BadRequest(new { Erro = participantesPremio.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantePremioPorId")]
        public async Task<ActionResult<ParticipantePremioResponseDTO>> GetParticipantePremioPorId([FromQuery] Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremio = await _getParticipantePremioPorIdUseCase.GetParticipantePremioPorId(id);

                if (participantePremio.Valor == null) return NotFound("Não foi encontrado um Participante Premio com esse Id");

                if (participantePremio.Sucesso)
                {
                    var participantePremioresponse = participantePremio.Valor.ConverterParaResponse();
                    return Ok(participantePremioresponse);
                }

                return BadRequest(new { Erro = participantePremio.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
}
