using BancoBackend.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.datos
{
    class HelperDao
    {
        private static HelperDao instance;
        private SqlConnection cnn;
        private SqlCommand cmd;

        private HelperDao()
        {
            cnn = new SqlConnection(@Properties.Resources.connection);
            cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            
        }

        public bool EliminarCliente(int id)
        {
            SqlTransaction t = null;
            int affected = 0;
            try
            {
                OpenConnection("SP_BAJA_CLIENTE");
                t = cnn.BeginTransaction();
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@nro_cliente", id);
                affected = cmd.ExecuteNonQuery();
                t.Commit();

            }
            catch (SqlException)
            {
                t.Rollback();
            }
            finally
            {
                CloseConnection(cnn);
            }

            return affected == 1;
        }

        public Cliente Login(int nro_cliente, string contrasenia)
        {

            Cliente C = new Cliente();
            bool esPrimerRegistro = true;
           
                OpenConnection("SP_LOGIN");
                cmd.Parameters.AddWithValue("@nro_cliente", nro_cliente);
                cmd.Parameters.AddWithValue("@contrasenia", contrasenia);
               
               
                SqlDataReader reader = cmd.ExecuteReader();

            //if (!reader.Read())
            //{
            //    return C;
            //}

            while (reader.Read())
                {
                    if (esPrimerRegistro)
                    {
                        //solo para el primer resultado recuperamos los datos del MAESTRO:
                        C.NroCliente = Convert.ToInt32(reader["nro_cliente"].ToString());
                        C.Nombre = reader["nombre"].ToString();
                        C.Apellido = reader["apellido"].ToString();
                        C.DNI = reader["dni"].ToString();
                        C.Contrasenia = reader["dni"].ToString();
                        C.EsAdmin = Boolean.Parse(reader["es_admin"].ToString());
                        esPrimerRegistro = false;
                    }

                    Cuenta cu = new Cuenta();
                    TipoCuenta ti = new TipoCuenta();
                    ti.Nombre = reader["nombre_tipo_cuenta"].ToString();
                    ti.IdTipoCuenta = int.Parse(reader["id_tipo_cuenta"].ToString());
                    cu.TipoCuenta = ti;
                    cu.CBU = reader["cbu"].ToString();
                    cu.Saldo = double.Parse(reader["saldo"].ToString());
                    cu.UltimoMovimiento = Convert.ToDateTime(reader["ultimo_movimiento"].ToString());
                    C.AgregarCuenta(cu);
                    esPrimerRegistro = false;
                    
                }

            
            
                
          
                CloseConnection(cnn);
            

            return C;
        }

        public DataTable CrearReporte(string desde, string hasta)
        {
            
            DataTable tabla = new DataTable();

           
                OpenConnection("SP_REPORT");
                cmd.Parameters.AddWithValue("@hasta",hasta);
                cmd.Parameters.AddWithValue("@desde", desde);
                tabla.Load(cmd.ExecuteReader());

            
          
            
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();

          
            return tabla;
        }

        internal bool EditarTipoCuenta(int id, string tipo)
        {
            SqlTransaction t = null;
            int affected = 0;
            //try
            //{
                OpenConnection("SP_EDITAR_TIPO_CUENTA");
                t = cnn.BeginTransaction();
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                affected = cmd.ExecuteNonQuery();
                t.Commit();

            //}
            //catch (SqlException)
            //{
            //    t.Rollback();
            //    return false;
            //}
            //finally
            //{
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            //}

            return true;
        }

        internal Cliente TraerUnCliente(int nro_cliente)
        {
            Cliente C = new Cliente();
            bool esPrimerRegistro = true;

            OpenConnection("SP_TRAER_UN_CLIENTE");
            cmd.Parameters.AddWithValue("@nro_cliente", nro_cliente);
            

            SqlDataReader reader = cmd.ExecuteReader();

           
            while (reader.Read())
            {
                if (esPrimerRegistro)
                {
                    //solo para el primer resultado recuperamos los datos del MAESTRO:
                    C.NroCliente = Convert.ToInt32(reader["nro_cliente"].ToString());
                    C.Nombre = reader["nombre"].ToString();
                    C.Apellido = reader["apellido"].ToString();
                    C.DNI = reader["dni"].ToString();
                    C.Contrasenia = reader["contrasenia"].ToString();
                    C.EsAdmin = Boolean.Parse(reader["es_admin"].ToString());
                    esPrimerRegistro = false;
                }

                Cuenta cu = new Cuenta();
                TipoCuenta ti = new TipoCuenta();
                ti.Nombre = reader["nombre_tipo_cuenta"].ToString();
                ti.IdTipoCuenta = int.Parse(reader["id_tipo_cuenta"].ToString());
                cu.TipoCuenta = ti;
                cu.CBU = reader["cbu"].ToString();
                cu.Saldo = double.Parse(reader["saldo"].ToString());
                cu.UltimoMovimiento = Convert.ToDateTime(reader["ultimo_movimiento"].ToString());
                C.AgregarCuenta(cu);
                esPrimerRegistro = false;

            }





            CloseConnection(cnn);


            return C;
        }

        internal bool Transferir(string cbu, double monto)
        {
            bool flag = false;
            int affected;
            OpenConnection("SP_TRANSFERIR");
            cmd.Parameters.AddWithValue("@cbu", cbu);
            cmd.Parameters.AddWithValue("@monto", monto);
            affected = cmd.ExecuteNonQuery();
            
            CloseConnection(cnn);
            if (affected > 1) return true;
            return flag;
        }

        public bool EliminarTipoCuenta(int id)
        {
            SqlTransaction t = null;
            int affected = 0;
            try
            {
                OpenConnection("SP_ELIMINAR_TIPO_CUENTA");
                t = cnn.BeginTransaction();
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@id_tipo_cuenta", id);
                affected = cmd.ExecuteNonQuery();
                t.Commit();

        }
            catch (SqlException)
            {
                t.Rollback();
                return false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return true;
        }

        public bool EliminarCuenta(string cbu)
        {
            SqlTransaction t = null;
            int affected = 0;
            try
            {
                OpenConnection("SP_ELIMINAR_CUENTA");
                t = cnn.BeginTransaction();
                cmd.Transaction = t;
                cmd.Parameters.AddWithValue("@cbu", cbu);
                affected = cmd.ExecuteNonQuery();
                t.Commit();

            }
            catch (SqlException)
            {
                t.Rollback();
                return false;
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return true;
        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open && connection != null)
            {
                connection.Close();
            }
        }

        private void OpenConnection(string NombreDelSP)
        {
            cnn.Open();
            cmd.CommandText = NombreDelSP;
            cmd.Parameters.Clear();
        }
        public static HelperDao GetInstance()
        {

            if (instance == null)
            {
                instance = new HelperDao();
            }
            return instance;
        }

        public bool AltaCliente(Cliente Cliente)
        {
            bool ok = true;
            int cod_cliente;
            
            SqlTransaction transaction = null;
            //try
            //{
            cnn.Open();
            transaction = cnn.BeginTransaction();
            //Se inserta Cuenta
            SqlCommand cmdMaestro = new SqlCommand("SP_INSERTAR_CLIENTE", cnn, transaction);
            cmdMaestro.CommandType = CommandType.StoredProcedure;

            cmdMaestro.Parameters.AddWithValue("@nombre", Cliente.Nombre);
            cmdMaestro.Parameters.AddWithValue("@apellido", Cliente.Apellido);
            cmdMaestro.Parameters.AddWithValue("@dni", Cliente.DNI);
            cmdMaestro.Parameters.AddWithValue("@contrasenia", Cliente.Contrasenia);
            cmdMaestro.Parameters.AddWithValue("@es_admin", Cliente.EsAdmin ? 1 : 0);

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@nro_cliente";
            param.SqlDbType = SqlDbType.Int;
            param.Direction = ParameterDirection.Output;

            cmdMaestro.Parameters.Add(param);

            cmdMaestro.ExecuteNonQuery();


            cod_cliente = (int)param.Value;

            //Se inserta Detalle Receta 
            foreach (Cuenta cuenta in Cliente.Cuentas)
            {
                SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_CUENTA", cnn, transaction);
                cmdDetalle.CommandType = CommandType.StoredProcedure;
                cmdDetalle.Parameters.AddWithValue("@nro_cliente", cod_cliente);
                cmdDetalle.Parameters.AddWithValue("@cbu", cuenta.CBU);
                cmdDetalle.Parameters.AddWithValue("@saldo", cuenta.Saldo);
                cmdDetalle.Parameters.AddWithValue("@id_tipo_cuenta", cuenta.TipoCuenta.IdTipoCuenta);

                cmdDetalle.ExecuteNonQuery();

            }

            transaction.Commit();
            //}
            //catch (Exception)
            //{
            //     transaction.Rollback();
            //     ok = false;

            //}
            //finally
            //{
            CloseConnection(cnn);
            // }
            return ok;
        }

        public bool ModificarCliente(Cliente Cliente)
        {
            bool ok = true;
            

            SqlTransaction transaction = null;
            //try
            //{
            cnn.Open();
            transaction = cnn.BeginTransaction();
            //Se inserta Cuenta
            SqlCommand cmdMaestro = new SqlCommand("SP_MODIFICAR_CLIENTE", cnn, transaction);
            cmdMaestro.CommandType = CommandType.StoredProcedure;

            cmdMaestro.Parameters.AddWithValue("@nro_cliente", Cliente.NroCliente);
            cmdMaestro.Parameters.AddWithValue("@nombre", Cliente.Nombre);
            cmdMaestro.Parameters.AddWithValue("@apellido", Cliente.Apellido);
            cmdMaestro.Parameters.AddWithValue("@dni", Cliente.DNI);
            cmdMaestro.Parameters.AddWithValue("@contrasenia", Cliente.Contrasenia);
            cmdMaestro.Parameters.AddWithValue("@es_admin", Cliente.EsAdmin ? 1 : 0);


            cmdMaestro.ExecuteNonQuery();

            SqlCommand cmdBorrar = new SqlCommand("SP_ELIMINAR_CUENTAS", cnn, transaction);
            cmdBorrar.CommandType = CommandType.StoredProcedure;

            cmdBorrar.Parameters.AddWithValue("@nro_cliente", Cliente.NroCliente);

            cmdBorrar.ExecuteNonQuery();


            //Se inserta Detalle Receta 
            foreach (Cuenta cuenta in Cliente.Cuentas)
            {
                SqlCommand cmdDetalle = new SqlCommand("SP_INSERTAR_CUENTA", cnn, transaction);
                cmdDetalle.CommandType = CommandType.StoredProcedure;
                cmdDetalle.Parameters.AddWithValue("@nro_cliente", Cliente.NroCliente);
                cmdDetalle.Parameters.AddWithValue("@cbu", cuenta.CBU);
                cmdDetalle.Parameters.AddWithValue("@saldo", cuenta.Saldo);
                cmdDetalle.Parameters.AddWithValue("@id_tipo_cuenta", cuenta.TipoCuenta.IdTipoCuenta);

                cmdDetalle.ExecuteNonQuery();

            }

            transaction.Commit();
            //}
            //catch (Exception)
            //{
            //     transaction.Rollback();
            //     ok = false;

            //}
            //finally
            //{
            CloseConnection(cnn);
            // }
            return ok;
        }

        public List<TipoCuenta> TraerTipoCuentas()
        {
            List<TipoCuenta> lst = new List<TipoCuenta>();

            OpenConnection("SP_TRAER_TIPO_CUENTAS");

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            CloseConnection(cnn);

            foreach (DataRow row in table.Rows)
            {
                TipoCuenta tipoCuenta = new TipoCuenta();
                tipoCuenta.IdTipoCuenta = Convert.ToInt32(row["id_tipo_cuenta"].ToString());
                tipoCuenta.Nombre = row["nombre"].ToString();


                lst.Add(tipoCuenta);
            }

            return lst;
        }

        public bool CrearTipoCuentas(string tipo)
        {
            bool flag = true;
            int affected;
            OpenConnection("SP_CREAR_TIPO_CUENTA");
            try
            {
                cmd.Parameters.AddWithValue("@tipo_cuenta", tipo);
                affected = cmd.ExecuteNonQuery();
            }
            catch
            {
                flag = false;
            }

            CloseConnection(cnn);
            
            return flag;
        }

        public List<string> TraerClientes()
        {
            List<string> lst = new List<string>();

            OpenConnection("SP_TRAER_CLIENTES");

            DataTable table = new DataTable();
            table.Load(cmd.ExecuteReader());

            CloseConnection(cnn);

            foreach (DataRow row in table.Rows)
            {

                lst.Add(row["cliente"].ToString());
            }

            return lst;
        }
    }
}
