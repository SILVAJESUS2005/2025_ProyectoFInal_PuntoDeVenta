using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace _2025_ProyectoFInal_PuntoDeVenta
{
    public partial class VentasForm: Form
    {
        public VentasForm()
        {
            InitializeComponent();
        }

        private void VentasForm_Load(object sender, EventArgs e)
        {
        
            cargarProductosDesdeDB();
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CellClick += dataGridView1_CellClick;


        }
        private void cargarProductosDesdeDB()
        {
            string connStr = "Data Source=C:\\Users\\Jesus\\source\\repos\\2025_ProyectoFInal_PuntoDeVenta\\2025_ProyectoFInal_PuntoDeVenta\\autocobro.db";
            using (var conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT nombre, precio FROM Productos"; // Ajusta según tu tabla
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader.GetString(0);
                        double precio = Convert.ToDouble(reader["precio"]); // usa el nombre de la columna

                        int rowIndex = dataGridView1.Rows.Add(nombre, precio, 1, precio); // Cantidad inicial 1, subtotal = precio
                        calcularSubtotal(rowIndex);
                    }
                }
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Cantidad"].Index)
            {
                calcularSubtotal(e.RowIndex);
            }
        }

        private void calcularSubtotal(int rowIndex)
        {
            var row = dataGridView1.Rows[rowIndex];
            if (row.Cells["Cantidad"].Value != null && row.Cells["Precio"].Value != null)
            {
                int cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value);
                decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value);
                row.Cells["Subtotal"].Value = cantidad * precio; // 
                CalcularTotalGeneral();

            }
        }

        private void CalcularTotalGeneral()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Subtotal"].Value != null &&
                    decimal.TryParse(row.Cells["Subtotal"].Value.ToString(), out decimal subtotal))
                {
                    total += subtotal;
                }
            }

            lblTotal.Text = $"${total:0.00}";
        }


        private void numericUpDownEditingControl1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    DialogResult resultado = MessageBox.Show("¿Deseas eliminar este producto de la lista?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                        CalcularTotalGeneral(); // <- Si ya tienes esta función, actualiza el total al eliminar
                    }
                }
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos para pagar.");
                return;
            }

            MessageBox.Show("Pago simulado realizado con éxito. ¡Gracias por su compra!", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Limpia la tabla después de pagar
            dataGridView1.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoginForm panel = new LoginForm(); // O el form que tengas
            panel.Show();
            this.Hide(); // Oculta el login
        }
    }

}
