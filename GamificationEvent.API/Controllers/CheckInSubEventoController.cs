using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.CheckInSubEventoCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckInSubEventoController : ControllerBase
    {
       private readonly CadastrarCheckInSubEventoUseCase _cadastrarCheckInSubEventoUseCase;
       private readonly GerarQrCodeUseCaseSubEvento _gerarQrCodeUseCaseSubEvento;
       private readonly GetCheckInsSubEventoPorIdParticipanteUseCase _getCheckInsSubEventoPorIdParticipanteUseCase;
       private readonly GetCheckInsSubEventoPorIdSubEventoUseCase _getCheckInsSubEventoPorIdSubEventoUseCase;
       private readonly GetCheckInSubEventoPorIdUseCase _getCheckInSubEventoPorIdUseCase;

        public CheckInSubEventoController(CadastrarCheckInSubEventoUseCase cadastrarCheckInSubEventoUseCase, GerarQrCodeUseCaseSubEvento gerarQrCodeUseCaseSubEvento, GetCheckInsSubEventoPorIdParticipanteUseCase getCheckInsSubEventoPorIdParticipanteUseCase, GetCheckInsSubEventoPorIdSubEventoUseCase getCheckInsSubEventoPorIdSubEventoUseCase, GetCheckInSubEventoPorIdUseCase getCheckInSubEventoPorIdUseCase)
        {
            _cadastrarCheckInSubEventoUseCase = cadastrarCheckInSubEventoUseCase;
            _gerarQrCodeUseCaseSubEvento = gerarQrCodeUseCaseSubEvento;
            _getCheckInsSubEventoPorIdParticipanteUseCase = getCheckInsSubEventoPorIdParticipanteUseCase;
            _getCheckInsSubEventoPorIdSubEventoUseCase = getCheckInsSubEventoPorIdSubEventoUseCase;
            _getCheckInSubEventoPorIdUseCase = getCheckInSubEventoPorIdUseCase;
        }

        [HttpGet("GerarQrCodeSubEvento")]
        public async Task<IActionResult> GerarQrCode([FromQuery] Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == Guid.Empty) return BadRequest("Insira um código válido");

                var resultado = await _gerarQrCodeUseCaseSubEvento.GerarQrCodeDoSubEvento((Guid)idSubEvento);

                if(resultado.Sucesso)
                {
                    var QrCode = resultado.Valor.ConverterQrCodeParaCore();
                    return Ok(QrCode); 

                }

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("FazerCheckInSubEvento")]
        public async Task<IActionResult> FazerCheckInSubEvento([FromQuery] string? codigo, [FromQuery] Guid? idParticipante)
        {
            try
            {
                if (String.IsNullOrEmpty(codigo)  || idParticipante == Guid.Empty || idParticipante == null) return BadRequest("Insira valores válidos");

                var checkIn = await _cadastrarCheckInSubEventoUseCase.CadastrarCheckInSubEvento(codigo, (Guid)idParticipante);

                if (checkIn.Sucesso) return Ok(checkIn.Valor);

                return BadRequest(new { Erro = checkIn.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCheckInSubEventoPorId")]
        public async Task<IActionResult> GetCheckInSubEventoPorId([FromQuery] Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");

                var checkIn = await _getCheckInSubEventoPorIdUseCase.GetCheckInSubEventoPorId((Guid)id);

                if (checkIn.Valor == null) return NotFound("CheckIn não encontrado");

                if (checkIn.Sucesso) {

                    var checkInDTO = checkIn.Valor.ConverterCheckInParaResponse();
                    return Ok(checkInDTO);
                }

                return BadRequest(new { Erro = checkIn.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCheckInsPorIdParticipante")]
        public async Task<IActionResult> GetCheckInsPorIdParticipante([FromQuery] Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty) return BadRequest("Insira uma Id Válido");

                var checkIns = await _getCheckInsSubEventoPorIdParticipanteUseCase.GetCheckInsSubEventoPorIdParticipante((Guid)idParticipante);

                if (checkIns.Sucesso)
                {
                    var checkInsDTO = checkIns.Valor.ConverterCheckInListaParaResponse();
                    return Ok(checkInsDTO);
                }
                return BadRequest(new { Erro = checkIns.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCheckInsPorIdSubEvento")]
        public async Task<IActionResult> GetCheckInsPorIdSubEvento([FromQuery] Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == Guid.Empty) return BadRequest("Insira uma Id Válido");

                var checkIns = await _getCheckInsSubEventoPorIdSubEventoUseCase.GetCheckInsSubEventoPorIdSubEvento((Guid)idSubEvento);

                if (checkIns.Sucesso)
                {
                    var checkInsDTO = checkIns.Valor.ConverterCheckInListaParaResponse();
                    return Ok(checkInsDTO);
                }
                return BadRequest(new { Erro = checkIns.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
    }

