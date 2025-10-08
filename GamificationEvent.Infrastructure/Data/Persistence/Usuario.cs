using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string SenhaHash { get; set; } = null!;

    public string? Telefone { get; set; }

    public DateTime? DataDeNascimento { get; set; }

    public string? Foto { get; set; }

    public DateTime? DataHoraCriacao { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();

    public virtual ICollection<UsuarioRedeSocial> UsuarioRedeSocials { get; set; } = new List<UsuarioRedeSocial>();
}
