using AnalisisMedicos.BLL;
using AnalisisMedicos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedicos.UI.Registros
{
    public partial class rAnalisis : Form
    {
        public rAnalisis()
        {
            InitializeComponent();
            LlenarComboBox();
            LlenarComboBox2();
        }

        private void LlenarComboBox()
        {
            var listado = new List<Usuarios>();

            listado = UsuariosBLL.GetList(p => true);

            Usuario_comboBox.DataSource = listado;
            Usuario_comboBox.ValueMember = "UsuarioId";
            Usuario_comboBox.DisplayMember = "Nombre";

        }

        private void LlenarComboBox2()
        {
            var listado = new List<TiposAnalisis>();

            listado = TiposAnalisisBLL.GetList(p => true);

            TipoAnalisis_comboBox.DataSource = listado;
            TipoAnalisis_comboBox.ValueMember = "TipoId";
            TipoAnalisis_comboBox.DisplayMember = "Descripcion";

        }

        private void Nuevo_button_Click(object sender, EventArgs e)
        {

        }

        private void Guardar_button_Click(object sender, EventArgs e)
        {

        }

        private void Eliminar_button_Click(object sender, EventArgs e)
        {

        }

        private void Buscar_button_Click(object sender, EventArgs e)
        {

        }
    }
}
