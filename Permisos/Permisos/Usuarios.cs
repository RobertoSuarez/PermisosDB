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
            DataTable databases = con.GetdataTable("create login " + textBox1.Text + " with password= '" + textBox2.Text+ "'");
            DataTable databases1 = con.GetdataTable("create user " + textBox1.Text + " for login " + textBox1.Text);
        }
        }
}
