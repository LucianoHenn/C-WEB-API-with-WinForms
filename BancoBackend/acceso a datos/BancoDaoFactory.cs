using BancoBackend.datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.acceso_a_datos
{
    class BancoDaoFactory : AbstractBancoDaoFactory
    {
        public override IBancoDao CrearDao()
        {
            return new BancoDao();
        }
    }
}
