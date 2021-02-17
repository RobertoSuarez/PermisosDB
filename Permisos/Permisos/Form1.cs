using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Permisos
{
    public partial class Form1 : Form, IContrat
    {
        ConexionBD con = new ConexionBD();

        // Colección de usuarios de la bases de datos.
        Dictionary<string, List<string>> users;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            llenarUsuarios();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1_CheckedChanged(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Permiso nueva = new Permiso(users);
            nueva.contrato = this;

            nueva.ShowDialog();
        }

        // list user and access
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                foreach (string key in users.Keys)
                {
                    var databasesUser = "";
                    foreach (string dbs in users[key])
                    {
                        databasesUser += " " + dbs;
                    }
                    dataGridView2.Rows.Add(key, databasesUser);
                }
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }

        private void llenarUsuarios()
        {
            users = new Dictionary<string, List<string>>();
            DataTable databases = con.GetdataTable("SELECT name FROM sys.databases"); // lista de bases de datos.
            foreach (DataRow db in databases.Rows)
            {
                // consulta en cada base de datos, listada en la anterior consulta.
                DataTable usersDB = con.GetdataTable("use " + db.ItemArray[0].ToString() + @" select name nombre 
                                    from sys.database_principals 
                                        where type_desc='SQL_USER' and name not in ('sa',
                                                                                    '##MS_PolicyEventProcessingLogin##',
                                                                                    '##MS_PolicyTsqlExecutionLogin##',
                                                                                    'guest',
                                                                                    'INFORMATION_SCHEMA','dbo',
																					'sys','MS_DataCollectorInternalUser')");
                foreach (DataRow usersAux in usersDB.Rows)
                {
                    if (users.Keys.Contains(usersAux.ItemArray[0].ToString()))
                    {
                        users[usersAux.ItemArray[0].ToString()].Add(db.ItemArray[0].ToString());
                    }
                    else
                    {
                        users.Add(usersAux.ItemArray[0].ToString(), new List<string>());
                        users[usersAux.ItemArray[0].ToString()].Add(db.ItemArray[0].ToString());
                    }
                }
            }
        }
    
        public void RefrezcarForm()
        {
            llenarUsuarios();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Lista_User_DB.Rows.Clear();
            // si no existe la columna la crea.
            if (!Lista_User_DB.Columns.Contains("Elemento")) {
                Lista_User_DB.Columns.Add("Elemento", "elemento");

            }

            // listar usuarios
            if (radioButton1.Checked) {
                var Coleccionuser = from u in users
                                    select u.Key;
                Lista_User_DB.Columns[0].HeaderText = "Usuarios";
                foreach (var i in Coleccionuser)
                {
                    Lista_User_DB.Rows.Add(i.ToString());
                }
            }
            
            // list data bases
            if (radioButton2.Checked) {
                Lista_User_DB.Columns[0].HeaderText = "Bases de datos";

                //users.GroupBy(x => x.Value).Select(x => x.FirstOrDefault());

                Dictionary<string, string> dbname = new Dictionary<string, string>();

                foreach (var user in users)
                {
                    foreach (var namedb in user.Value) {
                        
                        // no contiene la clave
                        if (!dbname.ContainsKey(namedb.ToString())) {
                            // Agregamos al datagridview
                            Lista_User_DB.Rows.Add(namedb.ToString());
                            dbname.Add(namedb.ToString(), namedb.ToString());
                            
                        }
                    }
                }
                

            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Usuarios nuevo = new Usuarios();
            nuevo.ShowDialog();
            nuevo.Refresh();
            this.Form1_Load(sender, e);
            Lista_User_DB.Rows.Clear();
            dataGridView2.Rows.Clear();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
