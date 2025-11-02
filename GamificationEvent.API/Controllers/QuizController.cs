using GamificationEvent.API.DTOs.Quiz;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.QuizUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class QuizController : ControllerBase
    {
        private readonly AdicionarAlternativasQuizUseCase _adicionarAlternativasQuizUseCase;
        private readonly AdicionarPerguntaQuizUseCase _adicionarPerguntaQuizUseCase;
        private readonly AtualizarQuizPerguntaUseCase _atualizarQuizPerguntaUseCase;
        private readonly AtualizarQuizUseCase _atualizarQuizUseCase;
        private readonly CadastrarQuizUseCase _cadastrarQuizUseCase;
        private readonly DeletarAlternativaQuizUseCase _deletarAlternativaQuizUseCase;
        private readonly DeletarPerguntaQuizUseCase _deletarPerguntaQuizUseCase;
        private readonly DeletarQuizUseCase _deletarQuizUseCase;
        private readonly GetQuizPorIdUseCase _getQuizPorIdUseCase;
        private readonly GetQuizzesPorIdEventoUseCase _getQuizzesPorIdEventoUseCase;
        private readonly GetTodasAsPerguntasPorIdQuizUseCase _getTodasAsPerguntasPorIdQuizUseCase;

        public QuizController(AdicionarAlternativasQuizUseCase adicionarAlternativasQuizUseCase, AdicionarPerguntaQuizUseCase adicionarPerguntaQuizUseCase, AtualizarQuizPerguntaUseCase atualizarQuizPerguntaUseCase, AtualizarQuizUseCase atualizarQuizUseCase, CadastrarQuizUseCase cadastrarQuizUseCase, DeletarAlternativaQuizUseCase deletarAlternativaQuizUseCase, DeletarPerguntaQuizUseCase deletarPerguntaQuizUseCase, DeletarQuizUseCase deletarQuizUseCase, GetQuizPorIdUseCase getQuizPorIdUseCase, GetQuizzesPorIdEventoUseCase getQuizzesPorIdEventoUseCase, GetTodasAsPerguntasPorIdQuizUseCase getTodasAsPerguntasPorIdQuizUseCase)
        {
            _adicionarAlternativasQuizUseCase = adicionarAlternativasQuizUseCase;
            _adicionarPerguntaQuizUseCase = adicionarPerguntaQuizUseCase;
            _atualizarQuizPerguntaUseCase = atualizarQuizPerguntaUseCase;
            _atualizarQuizUseCase = atualizarQuizUseCase;
            _cadastrarQuizUseCase = cadastrarQuizUseCase;
            _deletarAlternativaQuizUseCase = deletarAlternativaQuizUseCase;
            _deletarPerguntaQuizUseCase = deletarPerguntaQuizUseCase;
            _deletarQuizUseCase = deletarQuizUseCase;
            _getQuizPorIdUseCase = getQuizPorIdUseCase;
            _getQuizzesPorIdEventoUseCase = getQuizzesPorIdEventoUseCase;
            _getTodasAsPerguntasPorIdQuizUseCase = getTodasAsPerguntasPorIdQuizUseCase;
        }

        [HttpPost("CadastrarQuiz")]
        public async Task<IActionResult> CadastrarQuiz(QuizRequestDTO quizDto)
        {
            try
            {
                if (quizDto.IdEvento == null || quizDto.IdEvento == Guid.Empty) return BadRequest("Insira um Id Evento válido");

                var quiz = quizDto.ConverterQuizParaCore();

                var resultado = await _cadastrarQuizUseCase.CadastrarQuiz(quiz);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("CadastrarPerguntaQuiz")]
        public async Task<IActionResult> CadastrarPergunta(QuizPerguntaRequestDTO perguntaDTO)
        {
            try
            {
                if (perguntaDTO.IdQuiz == null || perguntaDTO.IdQuiz == Guid.Empty) return BadRequest("Insira um Id Quiz válido");

                var pergunta = perguntaDTO.ConverterQuizPerguntaParaCore();

                var resultado = await _adicionarPerguntaQuizUseCase.AdicionarPerguntaQuiz(pergunta);
                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("CadastrarAlternativasPerguntaQuiz")]
        public async Task<IActionResult> CadastrarAlternativasPerguntaQuiz(AlternativasPerguntasQuizDTO alternativasDTO)
        {
            try
            {
                if (alternativasDTO.IdPerguntaQuiz == Guid.Empty || alternativasDTO.IdPerguntaQuiz == null) return BadRequest("Insirs um Id válido");

                var alternativas = alternativasDTO.ConverterAlternativasParaCore();

                var resultado = await _adicionarAlternativasQuizUseCase.AdicionarAlternativasQuiz(alternativasDTO.IdPerguntaQuiz, alternativas);
                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarQuiz")]
        public async Task<IActionResult> DeletarQuiz(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");

                var resultado = await _deletarQuizUseCase.DeletarQuiz(id);

                if (resultado.Sucesso) return Ok("Quiz deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarPergunta")]
        public async Task<IActionResult> DeletarPergunta(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");

                var resultado = await _deletarPerguntaQuizUseCase.DeletarPerguntaQuiz(id);

                if (resultado.Sucesso) return Ok("Pergunta deletada");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarAlternativa")]
        public async Task<IActionResult> DeletarAlternativa(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");

                var resultado = await _deletarAlternativaQuizUseCase.DeletarAlternativaQuiz(id);

                if (resultado.Sucesso) return Ok("Alternativa deletada");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetQuizPorId")]
        public async Task<IActionResult> GetQuizPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");
                var quiz = await _getQuizPorIdUseCase.GetQuizPorId(id);

                if (quiz.Valor == null) return NotFound("Quiz não encontrado");

                if(quiz.Sucesso)
                {
                    var quizResponse = quiz.Valor.ConverterQuizParaResponse();
                    return Ok(quizResponse);
                }

                return BadRequest(new { Erro = quiz.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetQuizzesPorIdEvento")]
        public async Task<IActionResult> GetQuizzesPorIdEvento(Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty || idEvento == null) return BadRequest("Insira um Id válido");

                var quizzes = await _getQuizzesPorIdEventoUseCase.GetQuizzesPorIdEvento(idEvento);

                if (quizzes.Sucesso)
                {
                    var quizzesResponse = quizzes.Valor.ConverterListaQuizParaResponse();
                    return Ok(quizzesResponse);
                }

                return BadRequest(new { Erro = quizzes.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetTodasAsPerguntasComRespostaPorIdQuiz")]
        public async Task<IActionResult> GetTodasAsPerguntasComRespostaPorIdQuiz(Guid idQuiz)
        {
            try
            {
                if (idQuiz == Guid.Empty || idQuiz == null) return BadRequest("Insira um Id válido");

                var perguntas = await _getTodasAsPerguntasPorIdQuizUseCase.GetTodasAsPerguntasPorIdQuiz(idQuiz);

                if (perguntas.Sucesso)
                {
                    var perguntasResponse = perguntas.Valor.ConverterListaPerguntasEAlternativasComRespostaResponse();
                    return Ok(perguntasResponse);
                }
                return BadRequest(new { Erro = perguntas.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetTodasAsPerguntasPorIdQuiz")]
        public async Task<IActionResult> GetTodasAsPerguntasPorIdQuiz(Guid idQuiz)
        {
            try
            {
                if (idQuiz == Guid.Empty || idQuiz == null) return BadRequest("Insira um Id válido");

                var perguntas = await _getTodasAsPerguntasPorIdQuizUseCase.GetTodasAsPerguntasPorIdQuiz(idQuiz);

                if (perguntas.Sucesso)
                {
                    var perguntasResponse = perguntas.Valor.ConverterListaPerguntasEAlternativasResponse();
                    return Ok(perguntasResponse);
                }
                return BadRequest(new { Erro = perguntas.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarQuiz")]
        public async Task<IActionResult> AtualizarQuiz(Guid id, QuizUpdateDTO quizDTO)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");

                var quiz = quizDTO.ConverterQuizUpdateParaCore();
                var resultado = await _atualizarQuizUseCase.AtualizarQuiz(id, quiz);

                if (resultado.Sucesso) return Ok("Quiz Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarQuizPergunta")]
        public async Task<IActionResult> AtualizarQuizPergunta(Guid id, QuizPerguntaUpdateDTO perguntaDTO)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um Id válido");

                var pergunta = perguntaDTO.ConverterQuizPerguntaUpdateParaCore();
                var resultado = await _atualizarQuizPerguntaUseCase.AtualizarQuizPergunta(id, pergunta);

                if (resultado.Sucesso) return Ok("Pergunta Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
    }

        

        

    

