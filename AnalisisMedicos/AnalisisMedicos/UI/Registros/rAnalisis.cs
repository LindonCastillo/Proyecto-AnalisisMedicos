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
        public List<AnalisisDetalle> Detalle { get; set; }
        public rAnalisis()
        {
            InitializeComponent();
            this.Usuario_comboBox.SelectedItem = null;
            this.TipoAnalisis_comboBox.SelectedItem = null;
            this.Detalle = new List<AnalisisDetalle>();
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
            Limpiar();
        }

        private void Guardar_button_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Analisis analisis;

            if (!Validar())
            {
                return;
            }

            try
            {
                analisis = LlenarClase();

                if (Id_numericUpDown.Value == 0)
                {
                    paso = AnalisisBLL.Guardar(analisis);
                }
                else
                {
                    if (!ExisteEnLaBaseDeDatos())
                    {
                        MessageBox.Show("No se puede modificar un Analisis que no existe", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    paso = AnalisisBLL.Modificar(analisis);
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
            catch (Exception)
            {
                MessageBox.Show("No fue posible guardar!!");
            }
        }

        private void Eliminar_button_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(Id_numericUpDown.Text, out id);

            Limpiar();
            try
            {
                if (AnalisisBLL.Eliminar(id))
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
            Analisis analisis;
            int id = Convert.ToInt32(Id_numericUpDown.Value);

            Limpiar();
            try
            {
                analisis = AnalisisBLL.Buscar(id);
                if (analisis != null)
                {
                    LlenarCampos(analisis);
                    MessageBox.Show("Analisis Encontrado!", "Exito!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Analisis No Encontrado!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al buscar");
            }
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Analisis analisis = AnalisisBLL.Buscar((int)Id_numericUpDown.Value);
            return (analisis != null);
        }

        private void Limpiar()
        {
            errorProvider.Clear();

            Id_numericUpDown.Value = 0;
            Fecha_dateTimePicker.Value = DateTime.Now;
            Resultado_textBox.Text = string.Empty;

            this.Usuario_comboBox.SelectedItem = null;
            this.TipoAnalisis_comboBox.SelectedItem = null;

            this.Detalle = new List<AnalisisDetalle>();
            CargarGrid();

        }

        private Analisis LlenarClase()
        {
            Analisis analisis = new Analisis();
            analisis.AnalisisId = Convert.ToInt32(Id_numericUpDown.Value);
            analisis.Fecha = Fecha_dateTimePicker.Value;


            analisis.analisisDetalles = this.Detalle;

            return analisis;
        }

        private void LlenarCampos(Analisis analisis)
        {
            Id_numericUpDown.Value = analisis.AnalisisId;
            Fecha_dateTimePicker.Value = analisis.Fecha;

            this.Detalle = analisis.analisisDetalles;
            CargarGrid();
        }

        private bool Validar()
        {
            bool paso = true;
            errorProvider.Clear();
            if (Usuario_comboBox.Items.Count <= 0)
            {
                errorProvider.SetError(Usuario_comboBox, "Debe agregar un Usuario");
                paso = false;
            }

            if (TipoAnalisis_comboBox.Items.Count <= 0)
            {
                errorProvider.SetError(TipoAnalisis_comboBox, "Debe agregar un Tipo Analisis");
                paso = false;
            }

            if (this.Detalle.Count == 0)
            {
                errorProvider.SetError(Detalle_dataGridView, "Debe poner un Tipo de Analisis y un Resultado");
                Resultado_textBox.Focus();
                paso = false;
            }

            return paso;
        }




        private void CargarGrid()
        {
            Detalle_dataGridView.DataSource = null;
            Detalle_dataGridView.DataSource = this.Detalle;
        }

        private void Remover_button_Click(object sender, EventArgs e)
        {
            if (Detalle_dataGridView.Rows.Count > 0 && Detalle_dataGridView.CurrentRow != null)
            {
                Detalle.RemoveAt(Detalle_dataGridView.CurrentRow.Index);
                CargarGrid();
            }
        }

        private void Agregar_button_Click(object sender, EventArgs e)
        {
            if (Detalle_dataGridView.DataSource != null)
                this.Detalle = (List<AnalisisDetalle>)Detalle_dataGridView.DataSource;


            this.Detalle.Add(
                new AnalisisDetalle(
                    AnalisisId: (int)Id_numericUpDown.Value,
                    TipoId: (int)Id_numericUpDown.Value,
                    Resultado: Resultado_textBox.Text
                    )
                );

            CargarGrid();
            Resultado_textBox.Focus();
            Resultado_textBox.Clear();
            this.Usuario_comboBox.SelectedItem = null;
            this.TipoAnalisis_comboBox.SelectedItem = null;
        }

    }
}
