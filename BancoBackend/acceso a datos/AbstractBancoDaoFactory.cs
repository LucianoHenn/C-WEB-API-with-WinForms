using BancoBackend.datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.acceso_a_datos
{
    abstract class AbstractBancoDaoFactory
    {
        public abstract IBancoDao CrearDao();
    }
}
