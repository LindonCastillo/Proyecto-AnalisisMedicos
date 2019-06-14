using AnalisisMedicos.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedicos
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void AnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rUsuarios usuarios = new rUsuarios();
            usuarios.Show();
        }
    }
}
