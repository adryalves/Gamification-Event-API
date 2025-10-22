﻿using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.ParticipantePremioUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                if (participantePremioDTO.IdParticipante == null || participantePremioDTO.IdParticipante == Guid.Empty
                    || participantePremioDTO.IdPremio == null || participantePremioDTO.IdPremio == Guid.Empty) return BadRequest("Preencha com Ids válidos");

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

        [HttpPut("AtualizarParticipantePremio")]
        public async Task<IActionResult> AtualizarParticipantePremio(Guid id, ParticipantePremioUpdateDTO participantePremioDTO)
        {
            try
            {
                if(id == null ||id == Guid.Empty) return BadRequest("Insira um Id Válido");

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
        public async Task<IActionResult> GetParticipantePremiosPorIdEvento(Guid idEvento)
        {
            try
            {
                if (idEvento == null || idEvento == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremios = await _getParticipantePremiosPorIdEventoUseCase.GetParticipantePremioPorIdEvento(idEvento);

                if (participantePremios.Sucesso)
                {
                    var participantePremiosResponse = participantePremios.Valor.ConverterListaPararesponse();
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
        public async Task<IActionResult> GetParticipantePremiosPorIdParticipante(Guid idParticipante)
        {
            try
            {
                if (idParticipante == null || idParticipante == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremios = await _getParticipantePremiosPorIdParticipanteUseCase.GetParticipantePremiosPorIdParticipante(idParticipante);

                if (participantePremios.Sucesso)
                {
                    var participantePremiosResponse = participantePremios.Valor.ConverterListaPararesponse();
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
        public async Task<IActionResult> GetParticipantesPremioPorIdPremio(Guid idPremio)
        {
            try
            {
                if (idPremio == null || idPremio == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantesPremio = await _getParticipantesPremioPorIdPremioUseCase.GetParticipantesPremioPorIdPremio(idPremio);

                if (participantesPremio.Sucesso)
                {
                    var participantePremiosResponse = participantesPremio.Valor.ConverterListaPararesponse();
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
        public async Task<IActionResult> GetParticipantePremioPorId(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty) return BadRequest("Insira um Id Válido");

                var participantePremio = await _getParticipantePremioPorIdUseCase.GetParticipantePremioPorId(id);

                if (participantePremio == null) return NotFound("Não foi encontrado um Participante Premio com esse Id");

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
