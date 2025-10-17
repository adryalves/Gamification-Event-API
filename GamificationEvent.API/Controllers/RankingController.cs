using GamificationEvent.Application.UseCases.RankingUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly GetRankingGeralPorIdEventoUseCase _getRankingGeralPorIdEventoUseCase;
        private readonly GetRankingPersonalizadoUseCase _getRankingPersonalizadoUseCase;

        public RankingController(GetRankingGeralPorIdEventoUseCase getRankingGeralPorIdEventoUseCase, GetRankingPersonalizadoUseCase getRankingPersonalizadoUseCase)
        {
            _getRankingGeralPorIdEventoUseCase = getRankingGeralPorIdEventoUseCase;
            _getRankingPersonalizadoUseCase = getRankingPersonalizadoUseCase;
        }

        [HttpGet("GetRankingGeralPorIdEvento")]
        public async Task<IActionResult> GetRankingGeralPorIdEvento(Guid idEvento, [FromQuery] int quantidade = 10)
        {
            try
            {
                if (idEvento == Guid.Empty || idEvento == null) return BadRequest("Insira um id válido");

                var ranking = await _getRankingGeralPorIdEventoUseCase.GetRankingGeralPorIdEvento(idEvento, quantidade);
                return Ok(ranking);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetRakingPersonalizado")]
        public async Task<IActionResult> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, [FromQuery] int quantidade = 10)
        {
            try
            {
                if (idEvento == Guid.Empty || idEvento == null
                    || idParticipante == null || idParticipante == Guid.Empty) return BadRequest("Insira ids válido");


                var ranking = await _getRankingPersonalizadoUseCase.GetRankingPersonalizado(idEvento, idParticipante, quantidade);
                return Ok(ranking);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
