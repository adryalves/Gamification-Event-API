using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.InteresseUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteresseController : ControllerBase
    {
        private readonly CadastrarInteresseUseCase _cadastrarInteresseUseCase;
        private readonly DeletarInteresseUseCase _deletarInteresseUseCase;
        private readonly GetInteressePorIdUseCase _getInteressePorIdUseCase;
        private readonly GetInteressesPorIdEventoUseCase _getInteressesPorIdEventoUseCase;

        public InteresseController(CadastrarInteresseUseCase cadastrarInteresseUseCase, DeletarInteresseUseCase deletarInteresseUseCase, GetInteressePorIdUseCase getInteressePorIdUseCase, GetInteressesPorIdEventoUseCase getInteressesPorIdEventoUseCase)
        {
            _cadastrarInteresseUseCase = cadastrarInteresseUseCase;
            _deletarInteresseUseCase = deletarInteresseUseCase;
            _getInteressePorIdUseCase = getInteressePorIdUseCase;
            _getInteressesPorIdEventoUseCase = getInteressesPorIdEventoUseCase;
        }

        [HttpPost("CadastrarInteresses")]
        public async Task<IActionResult> CadastrarInteresses(ListaInteresseRequestDTO interessesRequest)
        {
            try
            {
                if (interessesRequest == null || interessesRequest.InteressesDTO == null) return BadRequest("Insira valores válidos");
                if (interessesRequest.IdEvento == null || interessesRequest.IdEvento == Guid.Empty) return BadRequest("Insira uma Id evento válido");

                var interesses = interessesRequest.ConverterListaParaCore();

                var cadastrados = await _cadastrarInteresseUseCase.CadastrarInteresses(interessesRequest.IdEvento, interesses);

                return Ok($"Foram cadastrados {cadastrados} interesses");
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarInteresse")]
        public async Task<IActionResult> DeletarInteresse(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id  válido");

                var deleção = await _deletarInteresseUseCase.DeletarInteresse(id);

                if (!deleção)
                {
                    return NotFound("Cadastro de Interesse nesse evento não encontrado para ser deletado");
                }

                return Ok("Interesse deletado");

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInteressesPorIdEvento")]
        public async Task<IActionResult> GetInteressesPorIdEvento(Guid idEvento)
        {
            try
            {
                var interesses = await _getInteressesPorIdEventoUseCase.GetInteressesPorIdEvento(idEvento);

                if (interesses == null || interesses.Count == 0) return NotFound("Não foram encontrados interesses para esse evento");

                var interessesDTO = interesses.ConverterListaParaResponse();
                return Ok(interessesDTO);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInteressePorId")]
        public async Task<IActionResult> GetInteressePorId(Guid id)
        {
            try
            {
                var interesse = await _getInteressePorIdUseCase.GerInteressePorId(id);

                if (interesse == null) return NotFound("Esse interesse não foi encontrado");

                var interesseDTO = interesse.ConverterInteresseParaResponse();

                return Ok(interesseDTO);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
