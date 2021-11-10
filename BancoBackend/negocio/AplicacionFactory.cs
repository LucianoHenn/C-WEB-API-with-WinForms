using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.negocio
{
    class AplicacionFactory : AbstractAplicacionFactory
    {
        public override IAplicacion CrearAplicacion()
        {
            return new Aplicacion();
        }
    }
}
