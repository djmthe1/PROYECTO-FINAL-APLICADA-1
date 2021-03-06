﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace Management_System_Pc_Repair_Shop.Registros
{
    public partial class MarcasForm : Form
    {
        public MarcasForm()
        {
            InitializeComponent();
        }

        Marcas marcas = new Marcas();
        Validaciones validar = new Validaciones();

        private void MarcasForm_Load(object sender, EventArgs e)
        {

        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Registro de Marcas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Registro de Marcas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Registro de Marcas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void idTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.Numeros_KeyPress(e);
        }

        private void descripcionTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.LetrasNumeros_KeyPress(e);
        }

        private void Limpiar()
        {
            idTextBox.Clear();
            descripcionTextBox.Clear();
        }

        private void ObtenerValores()
        {
            int id = 0;
            int.TryParse(idTextBox.Text, out id);
            marcas.MarcaId = id;
            marcas.Descripcion = descripcionTextBox.Text;
        }

        private void DevolverValores()
        {
            idTextBox.Text = marcas.MarcaId.ToString();
            descripcionTextBox.Text = marcas.Descripcion.ToString();
        }

        private void buscarButton_Click(object sender, EventArgs e)
        {
            ObtenerValores();
            if (idTextBox.Text.Length == 0)
            {
                MessageBox.Show("Debe insertar un Id", "Error al Buscar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (marcas.Buscar(marcas.MarcaId))
                {
                    DevolverValores();
                }
                else
                {
                    MensajeAdvertencia("Id no encontrado");
                    Limpiar();
                }
            }
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            ObtenerValores();
            if (idTextBox.Text == "")
            {
                if (descripcionTextBox.Text != "")
                {
                    if (marcas.Insertar())
                    {
                        Limpiar();
                        MensajeOk("Insertado correctamente");
                    }
                    else
                    {
                        MensajeError("Error al insertar");
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar la descripción", "Error al insertar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (descripcionTextBox.Text != "")
                {
                    if (marcas.Editar())
                    {
                        Limpiar();
                        MensajeOk("Modificado correctamente");
                    }
                    else
                    {
                        MensajeError("Error al modificar");
                    }
                }
                else
                {
                    MessageBox.Show("Debe llenar la descripción", "Error al modificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            ObtenerValores();
            if (idTextBox.Text.Length == 0)
            {
                MessageBox.Show("Debe insertar un Id", "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (marcas.Buscar(marcas.MarcaId))
                {
                    if (marcas.Eliminar())
                    {
                        MensajeOk("Eliminado correctamente");
                        Limpiar();
                    }
                    else
                    {
                        MensajeError("Error al eliminar");
                    }
                }
                else
                {
                    MensajeAdvertencia("Este Id no existe");
                    Limpiar();
                }
            }
        }
    }
}
