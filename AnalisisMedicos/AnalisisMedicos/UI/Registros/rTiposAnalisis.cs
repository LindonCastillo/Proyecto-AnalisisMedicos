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
    public partial class rTiposAnalisis : Form
    {
        public rTiposAnalisis()
        {
            InitializeComponent();
        }

        private void Nuevo_button_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardar_button_Click(object sender, EventArgs e)
        {
            bool paso = false;
            TiposAnalisis tiposAnalisis;

            if (!Validar())
            {
                return;
            }

            tiposAnalisis = LlenarClase();

            if (TipoId_numericUpDown.Value == 0)
            {
                paso = TiposAnalisisBLL.Guardar(tiposAnalisis);
            }
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar un Tipo de analisis que no existe", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                paso = TiposAnalisisBLL.Modificar(tiposAnalisis);
                MessageBox.Show("Se modifico con Exito!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Eliminar_button_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(TipoId_numericUpDown.Text, out id);

            Limpiar();
            try
            {
                if (TiposAnalisisBLL.Eliminar(id))
                {
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puede eliminar esta ubicación", "No Hubo Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error eliminando");
            }
        }

        private void Buscar_button_Click(object sender, EventArgs e)
        {
            TiposAnalisis tiposAnalisis;
            int id = Convert.ToInt32(TipoId_numericUpDown.Value);

            Limpiar();
            try
            {
                tiposAnalisis = TiposAnalisisBLL.Buscar(id);
                if (tiposAnalisis != null)
                {
                    LlenarCampos(tiposAnalisis);
                    MessageBox.Show("Tipo Analisis Encontrada!", "Exito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tipo Analisis No Encontrada!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Hubo un error al buscar");
            }
        }

        private void Limpiar()
        {
            TipoId_numericUpDown.Value = 0;
            Descripcion_textBox.Text = string.Empty;
        }

        private bool Validar()
        {
            errorProvider.Clear();
            bool paso = true;

            if (string.IsNullOrWhiteSpace(Descripcion_textBox.Text))
            {
                errorProvider.SetError(Descripcion_textBox, "No se puede dejar este campo vacío");
                paso = false;
            }

            return paso;
        }

        private TiposAnalisis LlenarClase()
        {
            TiposAnalisis tiposAnalisis = new TiposAnalisis();

            tiposAnalisis.TipoId = Convert.ToInt32(TipoId_numericUpDown.Value);
            tiposAnalisis.Descripcion = Descripcion_textBox.Text.Trim();

            return tiposAnalisis;
        }

        private void LlenarCampos(TiposAnalisis tiposAnalisis)
        {
            TipoId_numericUpDown.Value = tiposAnalisis.TipoId;
            Descripcion_textBox.Text = tiposAnalisis.Descripcion;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            TiposAnalisis ubicaciones = TiposAnalisisBLL.Buscar((int)TipoId_numericUpDown.Value);
            return (ubicaciones != null);
        }
    }
}
