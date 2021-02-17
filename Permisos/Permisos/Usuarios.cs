using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Permisos
{
    public partial class Usuarios : Form
    {

        ConexionBD con = new ConexionBD();
        public Usuarios()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty) {
                MessageBox.Show("Tienes que llenar los campos");
                return;
            }
            // new login
            DataTable databases = con.GetdataTable("create login " + textBox1.Text + " with password= '" + textBox2.Text+ "'");
            if (databases == null) {
                MessageBox.Show("Error al crear el login");
                return;
            }
            
            // new user
            DataTable databases1 = con.GetdataTable("create user " + textBox1.Text + " for login " + textBox1.Text);
            if (databases1 == null)
            {
                MessageBox.Show("Error al crear el user");
                return;
            }

            MessageBox.Show("Usuario creado correctamente");
            this.Close();

        }
        
    }
}
