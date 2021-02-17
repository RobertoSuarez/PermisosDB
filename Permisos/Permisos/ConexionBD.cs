using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Permisos
{
    class ConexionBD
    {

        SqlConnection connection;
        SqlCommand cmd;
        SqlDataReader dr;

        SqlDataAdapter da;

        public ConexionBD()
        {
            try
            {
                connection = new SqlConnection(@"Data Source=DESKTOP-1AS9A9N\SQLEXPRESS;Initial Catalog=Master;Integrated Security=True");
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo conectar a la base de datos: " + ex.Message.ToString());
            }
        }

        public void OpenConection()
        {
            connection.Close();
            connection.Open();
        }

        public DataTable GetdataTable(string query)
        {
            DataTable dt = new DataTable();

            connection.Open();
            cmd = new SqlCommand(query, connection);
            var da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (Exception e) {
                //throw new Exception("Mensaje: ", e);
                connection.Close();

                return null;
            }
            connection.Close();

            return dt;
        }

        public string ExecuteInstruction(string query)
        {
            connection.Open();
            cmd = new SqlCommand(query, connection);
            try {
                cmd.ExecuteNonQuery();
            } catch (Exception e) {
                connection.Close();

                return e.Message;
            }
            connection.Close();
            return "";
        }

        public void CloseConnection()
        {
            connection.Close();
        }


    }
}
