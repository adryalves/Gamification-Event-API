using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
        public class Usuario
        {
            public Guid Id { get; set; }
            public string Nome { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Cpf { get; set; } = null!;
            public string SenhaHash { get;  set; } = null!;
            public string? Telefone { get; set; }
            public DateOnly? DataDeNascimento { get; set; }
            public string? Foto { get; set; }
            public DateTime? DataHoraCriacao { get; set; } = DateTime.UtcNow;
            public bool Deletado { get; set; } = false;

            public List<UsuarioRedeSocial> RedesSociais { get; set; } = new();

        public Usuario(Guid id, string nome, string email, string cpf, string? telefone, DateOnly? dataDeNascimento, string? foto, DateTime dataHoraCriacao, bool deletado, List<UsuarioRedeSocial> redesSociais)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Telefone = telefone;
            DataDeNascimento = dataDeNascimento;
            Foto = foto;
            DataHoraCriacao = dataHoraCriacao;
            Deletado = deletado;
            RedesSociais = redesSociais;
        }

        public Usuario(string nome, string email, string cpf, string? telefone, DateOnly? dataDeNascimento, string? foto, DateTime dataHoraCriacao, bool deletado, List<UsuarioRedeSocial> redesSociais)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Telefone = telefone;
            DataDeNascimento = dataDeNascimento;
            Foto = foto;
            DataHoraCriacao = dataHoraCriacao;
            Deletado = deletado;
            RedesSociais = redesSociais;
        }

        public Usuario()
        {
        }
    }
    }


