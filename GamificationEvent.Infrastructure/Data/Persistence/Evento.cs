using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Evento
{
    public Guid Id { get; set; }

    public Guid IdPaleta { get; set; }

    public string Titulo { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public string Objetivo { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string PublicoAlvo { get; set; } = null!;

    public DateTime DataInicio { get; set; }

    public DateTime DataFinal { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<Badge> Badges { get; set; } = new List<Badge>();

    public virtual ICollection<Desafio> Desafios { get; set; } = new List<Desafio>();

    public virtual ICollection<Estande> Estandes { get; set; } = new List<Estande>();

    public virtual Paletum IdPaletaNavigation { get; set; } = null!;

    public virtual ICollection<Inscrito> Inscritos { get; set; } = new List<Inscrito>();

    public virtual ICollection<Interesse> Interesses { get; set; } = new List<Interesse>();

    public virtual ICollection<Mapa> Mapas { get; set; } = new List<Mapa>();

    public virtual ICollection<Notificacao> Notificacaos { get; set; } = new List<Notificacao>();

    public virtual ICollection<Palestrante> Palestrantes { get; set; } = new List<Palestrante>();

    public virtual ICollection<Participante> Participantes { get; set; } = new List<Participante>();

    public virtual ICollection<Passaporte> Passaportes { get; set; } = new List<Passaporte>();

    public virtual ICollection<Patrocinador> Patrocinadors { get; set; } = new List<Patrocinador>();

    public virtual ICollection<Pesquisa> Pesquisas { get; set; } = new List<Pesquisa>();

    public virtual ICollection<Premio> Premios { get; set; } = new List<Premio>();

    public virtual ICollection<Publicacao> Publicacaos { get; set; } = new List<Publicacao>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<SubEvento> SubEventos { get; set; } = new List<SubEvento>();
}
