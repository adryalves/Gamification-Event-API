using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PontoMapa
{
    public Guid Id { get; set; }

    public Guid IdMapa { get; set; }

    public string Nome { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? DetalhesLocalizacao { get; set; }

    public int? CoordenadaX { get; set; }

    public int? CoordenadaY { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<Estande> Estandes { get; set; } = new List<Estande>();

    public virtual Mapa IdMapaNavigation { get; set; } = null!;

    public virtual ICollection<SubEvento> SubEventos { get; set; } = new List<SubEvento>();
}
