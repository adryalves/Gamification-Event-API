using GamificationEvent.API.DTOs;
using GamificationEvent.Application.Mappings;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly CadastrarUsuarioUseCase _cadastrarUsuarioUseCase;
        private readonly GetUsuariosUseCase _getUsuariosUseCase;
        private readonly GetUsuarioPorIdUseCase _getUsuarioPorIdUseCase;
        private readonly DeletarUsuarioUseCase _deletarUsuarioUseCase;
        private readonly AtualizarUsuarioUseCase _atualizarUsuarioUseCase;

        public UsuarioController(CadastrarUsuarioUseCase cadastrarUsuarioUseCase, GetUsuariosUseCase getUsuariosUseCase, GetUsuarioPorIdUseCase getUsuarioPorIdUseCase, DeletarUsuarioUseCase deletarUsuarioUseCase, AtualizarUsuarioUseCase atualizarUsuarioUseCase)
        {
            _cadastrarUsuarioUseCase = cadastrarUsuarioUseCase;
            _getUsuariosUseCase = getUsuariosUseCase;
            _getUsuarioPorIdUseCase = getUsuarioPorIdUseCase;
            _deletarUsuarioUseCase = deletarUsuarioUseCase;
            _atualizarUsuarioUseCase = atualizarUsuarioUseCase;
        }

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioRequestDTO usuarioDTO)
        {
            try
            {
                var usuario = usuarioDTO.ConverterUsuarioCore();

                var novoUsuario = await _cadastrarUsuarioUseCase.CadastrarUsuario(usuario, usuarioDTO.Senha);

                return Ok(novoUsuario.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {

                var usuarios = await _getUsuariosUseCase.GetUsuarios();

                if (usuarios == null || usuarios.Count == 0)
                {
                    return NotFound("Não há usuários");
                }

                List<UsuarioResponseDTO> usuariosResponse = new();

                foreach (var usuario in usuarios)
                {
                    var usuarioResponse = usuario.ConverterUsuarioResponse();
                    
                    usuariosResponse.Add(usuarioResponse);

                }
                return Ok(usuariosResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetUsuarioPorId")]
        public async Task<IActionResult> GetUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = await _getUsuarioPorIdUseCase.GetUsuario(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                var usuarioResponse = usuario.ConverterUsuarioResponse();

                return Ok(usuarioResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuarioPorId(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var deleção = await _deletarUsuarioUseCase.DeletarUsuario(id);
                if (!deleção)
                {
                    return NotFound("Usuário não encontrado para ser deletado");
                }

                return Ok("Uusário deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuarioPorId(Guid id, [FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            try
            {

                var usuario = usuarioDTO.ConverterUpdateParaCore();
                usuario.Id = id;

                await _atualizarUsuarioUseCase.AtualizarUsuario(usuario);
                return Ok("Usuario atualizado");
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
}

