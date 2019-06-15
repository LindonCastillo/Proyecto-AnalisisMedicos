using AnalisisMedicos.Entidades;
using AnalisisMedicos.BLL;
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
    public partial class rUsuarios : Form
    {
        public rUsuarios()
        {
            InitializeComponent();
        }

        private void Nuevo_button_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardar_button_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            Usuarios Usuario;
            bool paso = false;

            if (!Validar())
            {
                return;
            }

            try
            {

                Usuario = LlenaClase();

                if (UsuarioId_numericUpDown.Value == 0)
                {
                    paso = UsuariosBLL.Guardar(Usuario);
                }
                else
                {
                    if (!ExisteEnLaBaseDeDatos())
                    {
                        MessageBox.Show("No se puede modificar un usuario que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    paso = UsuariosBLL.Modificar(Usuario);
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
                MessageBox.Show("Hubo un error al guardar");
            }

        }

        private void Eliminar_button_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();
            int id;
            int.TryParse(UsuarioId_numericUpDown.Text, out id);

            Limpiar();

            try
            {
                if (UsuariosBLL.Eliminar(id))
                {
                    MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MyErrorProvider.SetError(UsuarioId_numericUpDown, "No se puede eliminar este Usuario");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al eliminar");
            }

        }

        private void Limpiar()
        {
            MyErrorProvider.Clear();

            UsuarioId_numericUpDown.Value = 0;
            Nombre_textBox.Text = string.Empty;
            Email_textBox.Text = string.Empty;
            Usuario_textBox.Text = string.Empty;
            Clave_textBox.Text = string.Empty;
            FechaIngreso_dateTimePicker.Value = DateTime.Now;
        }

        private Usuarios LlenaClase()
        {
            Usuarios Usuario = new Usuarios();
            Usuario.UsuarioId = Convert.ToInt32(UsuarioId_numericUpDown.Value);
            Usuario.Nombre = Nombre_textBox.Text.Trim();
            Usuario.Email = Email_textBox.Text.Trim();
            Usuario.Usuario = Usuario_textBox.Text.Trim();
            Usuario.Clave = Clave_textBox.Text.Trim();
            Usuario.FechaIngreso = FechaIngreso_dateTimePicker.Value;

            return Usuario;
        }

        private void LlenaCampo(Usuarios Usuario)
        {
            UsuarioId_numericUpDown.Value = Usuario.UsuarioId;
            Nombre_textBox.Text = Usuario.Nombre;
            Email_textBox.Text = Usuario.Email;
            Usuario_textBox.Text = Usuario.Usuario;
            Clave_textBox.Text = Usuario.Clave;
            FechaIngreso_dateTimePicker.Value = Usuario.FechaIngreso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Usuarios Usuario = UsuariosBLL.Buscar((int)UsuarioId_numericUpDown.Value);
            return (Usuario != null);
        }

        private bool Validar()
        {
            MyErrorProvider.Clear();
            bool paso = true;

            if(string.IsNullOrWhiteSpace(Nombre_textBox.Text))
            {
                MyErrorProvider.SetError(Nombre_textBox, "No Puede dejar el campo Nombre vacio");
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(Email_textBox.Text))
            {
                MyErrorProvider.SetError(Email_textBox, "No Puede dejar el campo E - Mail vacio");
                paso = false;
            }


            if (Usuario_textBox.Text == string.Empty)
            {
                MyErrorProvider.SetError(Usuario_textBox,"No Puede dejar el campo Usuario vacio");
                paso = false;
            }


            if (string.IsNullOrWhiteSpace(Clave_textBox.Text))
            {
                MyErrorProvider.SetError(Clave_textBox,"No Puede dejar el campo Clave vacio");
                paso = false;
            }
   
            return paso;
        }

        private void Buscar_button_Click(object sender, EventArgs e)
        {
            Usuarios Usuario;
            int id = Convert.ToInt32(UsuarioId_numericUpDown.Value);

            Limpiar();

            try
            {
                Usuario = UsuariosBLL.Buscar(id);
                if (Usuario != null)
                {
                    LlenaCampo(Usuario);
                    MessageBox.Show("Persona Encontrada");

                }
                else
                {
                    MessageBox.Show("Persona No Encontrada");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error al buscar");
            }
        }
    }
}
