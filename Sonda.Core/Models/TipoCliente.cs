using System;
using System.Collections.Generic;
using System.Text;

namespace Sonda.Core.Models
{
    public class TipoCliente
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public List<Cliente> Cliente { get; set; }
    }
}
