using GamificationEvent.API.DTOs.SubEvento;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.PremioUseCases;
using GamificationEvent.Application.UseCases.SubEventoUseCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubEventoController : ControllerBase
    {
        private readonly AdicionarPerguntaProSubEventoUseCase _adicionarPerguntaProSubEventoUseCase;
        private readonly AtualizarSubEventoUseCase _atualizarSubEventoUseCase;
        private readonly CadastrarSubEventoUseCase _cadastrarSubEventoUseCase;
        private readonly DeletarSubEventoUseCase _deletarSubEventoUseCase;
        private readonly GetPerguntasPorIdSubEventoUseCase _getPerguntasPorIdSubEventoUseCase;
        private readonly GetSubEventoPorIdUseCase _getSubEventoPorIdUseCase;
        private readonly GetSubEventosPorIdEventoUseCase _getSubEventosPorIdEventoUseCase;

        public SubEventoController(AdicionarPerguntaProSubEventoUseCase adicionarPerguntaProSubEventoUseCase, AtualizarSubEventoUseCase atualizarSubEventoUseCase, CadastrarSubEventoUseCase cadastrarSubEventoUseCase, DeletarSubEventoUseCase deletarSubEventoUseCase, GetPerguntasPorIdSubEventoUseCase getPerguntasPorIdSubEventoUseCase, GetSubEventoPorIdUseCase getSubEventoPorIdUseCase, GetSubEventosPorIdEventoUseCase getSubEventosPorIdEventoUseCase)
        {
            _adicionarPerguntaProSubEventoUseCase = adicionarPerguntaProSubEventoUseCase;
            _atualizarSubEventoUseCase = atualizarSubEventoUseCase;
            _cadastrarSubEventoUseCase = cadastrarSubEventoUseCase;
            _deletarSubEventoUseCase = deletarSubEventoUseCase;
            _getPerguntasPorIdSubEventoUseCase = getPerguntasPorIdSubEventoUseCase;
            _getSubEventoPorIdUseCase = getSubEventoPorIdUseCase;
            _getSubEventosPorIdEventoUseCase = getSubEventosPorIdEventoUseCase;
        }

        [HttpPost("CadastrarSubEvento")]
        public async Task<IActionResult> CadastrarSubEvento(SubEventoRequestDTO subEventoDTO)
        {
            try
            {
                if (subEventoDTO.IdEvento == Guid.Empty) return BadRequest("Insira um id válido");

                var subEvento = subEventoDTO.ConverterSubEventoRequestParaCore();

                var resultado = await _cadastrarSubEventoUseCase.CadastrarSubEvento(subEvento);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("CadastrarPerguntasSubEvento")]
        public async Task<IActionResult> CadastrarPerguntasSubEvento(PerguntasSubEventoRequestDTO perguntaDTO)
        {
            try
            {
                if (perguntaDTO.IdSubEvento == Guid.Empty || perguntaDTO.IdParticipante == Guid.Empty) return BadRequest("Insira valores válidos");

                var pergunta = perguntaDTO.ConverterPerguntaParaCore();
                var resultado = await _adicionarPerguntaProSubEventoUseCase.AdicionarPerguntaProSubEvento(pergunta);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarSubEvento")]
        public async Task<IActionResult> AtualizarSubEvento([FromRoute]Guid id, SubEventoUpdateDTO subEventoDTO)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido para atualização");

                var subEvento = subEventoDTO.ConverterSubEventoUpdateParaCore();
                var resultado = await _atualizarSubEventoUseCase.AtualizarSubEvento(id, subEvento);
                
                if(resultado.Sucesso) return Ok("SubEvento Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarSubEvento")]
        public async Task<IActionResult> DeletarSubEvento([FromRoute]Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido para deleção");
                var resultado = await _deletarSubEventoUseCase.DeletarSubEvento(id);

                if (resultado.Sucesso) return Ok("SubEvento deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPerguntasPorIdSubEvento")]
        public async Task<IActionResult> GetPerguntasPorIdSubEvento([FromQuery]Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == Guid.Empty) return BadRequest("Insira um id válido");

                var perguntas = await _getPerguntasPorIdSubEventoUseCase.GetPerguntasPorIdSubEvento(idSubEvento);

                if (perguntas.Sucesso)
                {
                    var perguntasDTO = perguntas.Valor.ConverterParaListaResponse();
                    return Ok(perguntasDTO);
                }

                return BadRequest(new { Erro = perguntas.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetSubEventoPorId")]
        public async Task<IActionResult> GetSubEventoPorId([FromQuery] Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");

                var subEvento = await _getSubEventoPorIdUseCase.GetSubEventoPorId(id);

                if (subEvento.Valor == null) return NotFound("SubEvento não existe");

                if (subEvento.Sucesso)
                {
                    var subEventoDTO = subEvento.Valor.ConverterSubEventoParaResponse();
                    return Ok(subEventoDTO);
                }

                return BadRequest(new { Erro = subEvento.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpGet("GetSubEventosPorIdEvento")]
        public async Task<IActionResult> GetSubEventosPorIdEvento([FromQuery] Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty) return BadRequest("Insira um id válido para deleção");

                var subEventos = await _getSubEventosPorIdEventoUseCase.GetSubEventosPorIdEvento(idEvento);

                if (subEventos.Sucesso)
                {
                    var subEventosDTO = subEventos.Valor.ConverterParaSubEventoListaResponse();
                    return Ok(subEventosDTO);
                }

                return BadRequest(new { Erro = subEventos.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
