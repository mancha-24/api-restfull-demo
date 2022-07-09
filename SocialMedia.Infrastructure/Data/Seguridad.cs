using System;
using System.Collections.Generic;

namespace SocialMedia.Infrastructure.Data
{
    public partial class Seguridad
    {
        public int IdSeguridad { get; set; }
        public string Usuario { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
