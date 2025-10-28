using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.CheckInSubEventoCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> GerarQrCode(Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == null || idSubEvento == Guid.Empty) return BadRequest("Insira um código válido");

                var resultado = await _gerarQrCodeUseCaseSubEvento.GerarQrCodeDoSubEvento(idSubEvento);

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
        public async Task<IActionResult> FazerCheckInSubEvento(string codigo, Guid idParticipante)
        {
            try
            {
                if (String.IsNullOrEmpty(codigo) || idParticipante == null || idParticipante == Guid.Empty) return BadRequest("Insira valores válidos");

                var checkIn = await _cadastrarCheckInSubEventoUseCase.CadastrarCheckInSubEvento(codigo, idParticipante);

                if (checkIn.Sucesso) return Ok(checkIn.Valor);

                return BadRequest(new { Erro = checkIn.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCheckInSubEventoPorId")]
        public async Task<IActionResult> GetCheckInSubEventoPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id válido");

                var checkIn = await _getCheckInSubEventoPorIdUseCase.GetCheckInSubEventoPorId(id);

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
        public async Task<IActionResult> GetCheckInsPorIdParticipante(Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty || idParticipante == null) return BadRequest("Insira uma Id Válido");

                var checkIns = await _getCheckInsSubEventoPorIdParticipanteUseCase.GetCheckInsSubEventoPorIdParticipante(idParticipante);

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
        public async Task<IActionResult> GetCheckInsPorIdSubEvento(Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == Guid.Empty || idSubEvento == null) return BadRequest("Insira uma Id Válido");

                var checkIns = await _getCheckInsSubEventoPorIdSubEventoUseCase.GetCheckInsSubEventoPorIdSubEvento(idSubEvento);

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

