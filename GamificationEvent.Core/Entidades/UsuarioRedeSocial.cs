using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Entidades
{
    public class UsuarioRedeSocial
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string Plataforma { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public Usuario Usuario { get; set; }
    }
}
