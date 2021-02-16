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
                connection = new SqlConnection(@"Data Source=LAPTOP-4ENICVGV;Initial Catalog=Master;Integrated Security=True");
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
            da.Fill(dt);
            connection.Close();

            return dt;
        }

        public void ExecuteInstruction(string query)
        {
            connection.Open();
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void CloseConnection()
        {
            connection.Close();
        }


    }
}
