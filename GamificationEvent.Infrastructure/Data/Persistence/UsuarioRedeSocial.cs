using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class UsuarioRedeSocial
{
    public Guid Id { get; set; }

    public Guid IdUsuario { get; set; }

    public string Plataforma { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
