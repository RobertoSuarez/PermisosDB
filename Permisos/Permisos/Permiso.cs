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
using System.Data;


namespace Permisos
{
    public partial class Permiso : Form
    {
        ConexionBD con = new ConexionBD();
        Dictionary<string, List<string>> users;
        public IContrat contrato;

        public Permiso(Dictionary<string, List<string>> _users)
        {
            InitializeComponent();
            this.users = _users;
            llenardatos();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }

        public void llenardatos()
        {
            foreach (string key in users.Keys)
            {
                comboBox2.Items.Add(key);
            }

            DataTable table = con.GetdataTable("SELECT name FROM sys.databases");
            foreach (DataRow dbs in table.Rows)
            {
                comboBox3.Items.Add(dbs.ItemArray[0].ToString());
            }

            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void Permiso_Load(object sender, EventArgs e)
        {
          
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var value = con.ExecuteInstruction("use " + comboBox3.SelectedItem.ToString() + " create user " + comboBox2.SelectedItem.ToString() + " for login " + comboBox2.SelectedItem.ToString()); // modifique esto
            if (value != string.Empty) {
                MessageBox.Show(value);
                this.Close();
                return;
            }
            MessageBox.Show("Tarea completada");
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            var user = comboBox2.SelectedItem.ToString();
            foreach (string db in users[user])
            {
                comboBox1.Items.Add(db);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var value = con.ExecuteInstruction("use " + comboBox1.SelectedItem.ToString() + " drop user " + comboBox2.SelectedItem.ToString());
            if (value != string.Empty)
            {
                MessageBox.Show(value);
                this.Close();
                return;
            }
            MessageBox.Show("Tarea completada");
            this.Close();
        }

        private void Permiso_FormClosed(object sender, FormClosedEventArgs e)
        {
            contrato.RefrezcarForm();
        }
    }
}
