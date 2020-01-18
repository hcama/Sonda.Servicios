using System;
using System.Collections.Generic;
using System.Text;

namespace Sonda.Core.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int TipoClienteId { get; set; }
        public TipoCliente TipoCliente { get; set; }
    }
}
