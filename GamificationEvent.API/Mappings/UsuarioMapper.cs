using GamificationEvent.API.DTOs.Usuario;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.Mappings
{
    public static class UsuarioMapper
    {
        public static Usuario ConverterUsuarioCore(this UsuarioRequestDTO usuarioDTO)
        {
            return new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Cpf = usuarioDTO.Cpf,
                Telefone = usuarioDTO.Telefone,
                DataDeNascimento = usuarioDTO.DataDeNascimento,
                Foto = usuarioDTO.Foto,
                DataHoraCriacao = DateTime.UtcNow,
                Deletado = false,
                RedesSociais = usuarioDTO.RedesSociais.Select(r => new UsuarioRedeSocial
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
        }

        public static UsuarioResponseDTO ConverterUsuarioResponse(this Usuario usuario)
        {

            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                Telefone = usuario.Telefone,
                DataDeNascimento = usuario.DataDeNascimento,
                Foto = usuario.Foto,
                DataHoraCriacao = usuario.DataHoraCriacao,
                Deletado = usuario.Deletado,
                RedesSociais = usuario.RedesSociais.Select(r => new RedeSocialDTO
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
        }


        public static Usuario ConverterUpdateParaCore(this UsuarioUpdateDTO usuarioDTO)
        {

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Cpf = usuarioDTO.Cpf,
                Telefone = usuarioDTO.Telefone,
                DataDeNascimento = usuarioDTO.DataDeNascimento,
                RedesSociais = usuarioDTO.RedesSociais.Select(r => new UsuarioRedeSocial
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
            return usuario;
        }

        public static UsuarioLoginModel ConverterLoginParaCore(this UsuarioLoginRequestDTO usuarioLoginRequestDTO)
        {
            return new UsuarioLoginModel
            {
                Email = usuarioLoginRequestDTO.Email,
                Senha = usuarioLoginRequestDTO.Senha
            };
        }

    }
}

