using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.negocio
{
    abstract class AbstractAplicacionFactory
    {
        public abstract IAplicacion CrearAplicacion();
    }
}
