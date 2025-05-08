using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2025_ProyectoFInal_PuntoDeVenta
{
    public partial class RegistroForm: Form
    {
        public RegistroForm()
        {
            InitializeComponent();
        }

        private void RegistroForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            LoginForm panel = new LoginForm(); // O el form que tengas
            panel.Show();
            this.Hide(); // Oculta el login
        }

        // Método para registrar un nuevo usuario
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string correo = txtGmail.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, llena todos los campos.");
                return;
            }

            // Ruta de la base de datos (ajústala según dónde esté tu archivo .db)
            string dbPath = @"C:\Users\Jesus\source\repos\2025_ProyectoFInal_PuntoDeVenta\2025_ProyectoFInal_PuntoDeVenta\autocobro.db";
            string connectionString = $"Data Source={dbPath};Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = "INSERT INTO usuarios (nombre, correo, contraseña) VALUES (@nombre, @correo, @contraseña)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contraseña", password);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("¡Usuario registrado con éxito!");

                        // Puedes limpiar los campos o cerrar el formulario aquí
                        txtNombre.Clear();
                        txtGmail.Clear();
                        txtPassword.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al registrar: " + ex.Message);
                    }
                }
            }
        }
    }
}
