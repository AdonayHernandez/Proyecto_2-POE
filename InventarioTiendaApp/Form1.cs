using System;
using System.Windows.Forms;

namespace InventarioTiendaApp
{
    public partial class Form1 : Form
    {
        private Inventario inventario = new Inventario();

        public Form1()
        {
            InitializeComponent();

            // Suscribir eventos de inventario
            inventario.ProductoAgregado += Inventario_ProductoAgregado;
            inventario.ProductoActualizado += Inventario_ProductoActualizado;
            inventario.ProductoEliminado += Inventario_ProductoEliminado;

            // Configurar el ComboBox de categorías
            cmbCategoria.Items.Add("Electrónica");
            cmbCategoria.Items.Add("Ropa");
            cmbCategoria.Items.Add("Alimentos");
            cmbCategoria.Items.Add("Libros");
            cmbCategoria.SelectedIndex = 0;  // Seleccionar la primera categoría por defecto
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Opcional: Cargar datos al iniciar el formulario
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                var producto = new Producto(
                    txtNombre.Text,
                    cmbCategoria.SelectedItem.ToString(),
                    decimal.Parse(txtPrecio.Text),
                    (int)nudCantidad.Value);

                inventario.AgregarProducto(producto);
                ActualizarGrid();
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                var producto = new Producto(
                    txtNombre.Text,
                    cmbCategoria.SelectedItem.ToString(),
                    decimal.Parse(txtPrecio.Text),
                    (int)nudCantidad.Value);

                inventario.ActualizarProducto(producto);
                ActualizarGrid();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                inventario.EliminarProducto(txtNombre.Text);
                ActualizarGrid();
            }
        }

        private void ActualizarGrid()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = inventario.ObtenerInventario();
        }

        private void Inventario_ProductoAgregado(object sender, Producto e)
        {
            MessageBox.Show($"Producto {e.Nombre} agregado con éxito.", "Producto Agregado");
        }

        private void Inventario_ProductoActualizado(object sender, Producto e)
        {
            MessageBox.Show($"Producto {e.Nombre} actualizado con éxito.", "Producto Actualizado");
        }

        private void Inventario_ProductoEliminado(object sender, Producto e)
        {
            MessageBox.Show($"Producto {e.Nombre} eliminado con éxito.", "Producto Eliminado");
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Error");
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out _))
            {
                MessageBox.Show("El precio debe ser un número válido.", "Error");
                return false;
            }

            return true;
        }
    }
}
