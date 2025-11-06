using GamificationEvent.API.DTOs.QuizParticipante;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.QuizParticipanteUseCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuizParticipanteController : ControllerBase
    {
        private readonly CadastrarQuizParticipanteUseCase _cadastrarQuizParticipanteUseCase;
        private readonly CadastrarParticipanteQuizRespostaUseCase _cadastrarParticipanteQuizRespostaUseCase;
        private readonly GetParticipantesQuizPorIdQuizUseCase _getParticipantesQuizPorIdQuizUseCase;
        private readonly GetQuizzesPorIdParticipanteUseCase _getQuizzesPorIdParticipanteUseCase;
        private readonly GetResultadoParticipanteQuizUseCase _getResultadoParticipanteQuizUseCase;
        private readonly GetQuizRankingUseCase _getQuizRankingUseCase;
        private readonly DeletarTodasAsRespostasDoQuizUseCase _deletarTodasAsRespostasDoQuizUseCase;

        public QuizParticipanteController(CadastrarQuizParticipanteUseCase cadastrarQuizParticipanteUseCase, CadastrarParticipanteQuizRespostaUseCase cadastrarParticipanteQuizRespostaUseCase, GetParticipantesQuizPorIdQuizUseCase getParticipantesQuizPorIdQuizUseCase, GetQuizzesPorIdParticipanteUseCase getQuizzesPorIdParticipanteUseCase, GetResultadoParticipanteQuizUseCase getResultadoParticipanteQuizUseCase, GetQuizRankingUseCase getQuizRankingUseCase, DeletarTodasAsRespostasDoQuizUseCase deletarTodasAsRespostasDoQuizUseCase)
        {
            _cadastrarQuizParticipanteUseCase = cadastrarQuizParticipanteUseCase;
            _cadastrarParticipanteQuizRespostaUseCase = cadastrarParticipanteQuizRespostaUseCase;
            _getParticipantesQuizPorIdQuizUseCase = getParticipantesQuizPorIdQuizUseCase;
            _getQuizzesPorIdParticipanteUseCase = getQuizzesPorIdParticipanteUseCase;
            _getResultadoParticipanteQuizUseCase = getResultadoParticipanteQuizUseCase;
            _getQuizRankingUseCase = getQuizRankingUseCase;
            _deletarTodasAsRespostasDoQuizUseCase = deletarTodasAsRespostasDoQuizUseCase;
        }

        [HttpPost("CadastrarParticipanteQuizResposta")]
        public async Task<IActionResult> CadastrarParticipanteQuizResposta(ParticipanteQuizRespostaRequestDTO respostaDTO)
        {
            try
            {
                if (respostaDTO.IdParticipante == Guid.Empty || respostaDTO.IdQuizPergunta == Guid.Empty
                    || respostaDTO.IdQuizAlternativa == Guid.Empty) return BadRequest("Insira Ids válidos");

                var resposta = respostaDTO.ConverterParticipanteQuizRespostaParaCore();
                var resultado = await _cadastrarParticipanteQuizRespostaUseCase.CadastrarParticipanteQuizResposta(resposta);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("CadastrarQuizParticipante")]
        public async Task<IActionResult> CadastrarQuizParticipante(QuizParticipanteRequestDTO quizParticipante)
        {
            try
            {
                if (quizParticipante.IdParticipante == Guid.Empty || quizParticipante.IdQuiz == Guid.Empty) return BadRequest("Insira Ids válidos");

                var resposta = quizParticipante.ConveterQuizParticipante();
                var resultado = await _cadastrarQuizParticipanteUseCase.CadastrarQuizParticipante(resposta);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        
        [HttpGet("GetParticipantesQuizPorIdQuiz")]
        public async Task<ActionResult<List<QuizParticipanteResponseDTO>>> GetParticipantesQuizPorIdQuiz([FromQuery]Guid idQuiz)
        {
            try
            {
                if (idQuiz == Guid.Empty) return BadRequest("Insira Id válido");

                var participantes = await _getParticipantesQuizPorIdQuizUseCase.GetParticipantesQuizPorIdQuiz(idQuiz);

                if (participantes.Sucesso)
                {
                    var participantesDTO = participantes.Valor.ConverterQuizParticipanteListaParaResponse();
                    return Ok(participantesDTO);
                }
                return BadRequest(new { Erro = participantes.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetQuizzesPorIdParticipante")]
        public async Task<ActionResult<List<QuizParticipanteResponseDTO>>> GetQuizzesPorIdParticipante([FromQuery]Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty) return BadRequest("Insira Id válido");

                var participantes = await _getQuizzesPorIdParticipanteUseCase.GetQuizzesPorIdParticipante(idParticipante);

                if (participantes.Sucesso)
                {
                    var participantesDTO = participantes.Valor.ConverterQuizParticipanteListaParaResponse();
                    return Ok(participantesDTO);
                }
                return BadRequest(new { Erro = participantes.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetQuizParticipanteResultado")]
        public async Task<ActionResult<QuizParticipanteResultadoResponseDTO>> GetQuizParticipanteResultado([FromQuery] Guid IdQuiz, [FromQuery] Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty || IdQuiz == Guid.Empty) return BadRequest("Insira Id válido");

                var resultado = await _getResultadoParticipanteQuizUseCase.GetResultadoParticipanteQuiz(IdQuiz, idParticipante);

                if (resultado.Valor == null) return NotFound("Não foi encontrado um resultado referente a esse quiz e esse participante");

                if(resultado.Sucesso)
                {
                    var resultadoResponse = resultado.Valor.ConverterQuizParticipanteResultadoParaResponse();
                    return Ok(resultadoResponse);
                }
                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetQuizRanking")]
        public async Task<ActionResult<QuizRankingResponseDTO>> GetQuizRanking([FromQuery] Guid idQuiz, Guid? idParticipante = null, int top = 10)
        {
            try
            {
                if (idQuiz == Guid.Empty) return BadRequest("Insira um id válido");

                var ranking = await _getQuizRankingUseCase.GetQuizRanking(idQuiz, idParticipante, top);

                if (ranking.Valor == null) return NotFound("Não foi encontrado um raking referente a esse Quiz");

                if (ranking.Sucesso)
                {
                    var rankingResponse = ranking.Valor.QuizRankingParaResponse();
                    return Ok(rankingResponse);
                }
                return BadRequest(new { Erro = ranking.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarTodasAsRespostaDoQuiz/{idQuiz}")]
        public async Task<IActionResult> DeletarTodasAsRespostaDoQuizPorIdQuiz([FromRoute] Guid idQuiz)
        {
            try
            {
                if(idQuiz == Guid.Empty) return BadRequest("Insira um id válido");

                var resultado = await _deletarTodasAsRespostasDoQuizUseCase.DeletarTodasAsRespostaDoQuiz(idQuiz);

                if (resultado.Sucesso)
                {
                    if (resultado.Valor) return Ok("Respostas deletadas");

                    return Ok("Não foram encontradas respostas");
                }

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        }
        }
    

