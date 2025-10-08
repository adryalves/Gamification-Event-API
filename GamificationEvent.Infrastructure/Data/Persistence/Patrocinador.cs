using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Patrocinador
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string? Categoria { get; set; }

    public string? Descricao { get; set; }

    public string? Contato { get; set; }

    public string Email { get; set; } = null!;

    public string? EnderecoLoja { get; set; }

    public string? EnderecoOnline { get; set; }

    public string? Produtos { get; set; }

    public string? InfoExtra { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<Estande> Estandes { get; set; } = new List<Estande>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<PatrocinadorSubEvento> PatrocinadorSubEventos { get; set; } = new List<PatrocinadorSubEvento>();

    public virtual ICollection<Pesquisa> Pesquisas { get; set; } = new List<Pesquisa>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
