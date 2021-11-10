using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.dominio
{
    public class Cuenta
    {
        public string CBU { get; set; }

        public double Saldo { get; set; }

        public TipoCuenta TipoCuenta { get; set; }

        public DateTime UltimoMovimiento { get; set; }


    }
}
