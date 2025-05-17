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
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace _2025_ProyectoFInal_PuntoDeVenta
{
    public partial class LoginForm: Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CentrarPictureBox();
        }

        private void CentrarPictureBox()
        {

        }

        private void LoginForm_Resize(object sender, EventArgs e)
        {
            CentrarPictureBox();
        }

   
     
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RegistroForm registro = new RegistroForm(); // Instancia del form de registro
            registro.Show(); // Mostrar el form de registro

            this.Hide(); // Ocultar el form actual si lo deseas
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // Ruta de tu base de datos SQLite
            string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "autocobro.db")}";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM usuarios WHERE nombre = @nombre AND contraseña = @contraseña";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    int count = Convert.ToInt32(cmd.ExecuteScalar()); //hola

                    if (count > 0)
                    {
                        // Usuario válido
                        
                        VentasForm panel = new VentasForm(); // O el form que tengas
                        panel.Show();
                        this.Hide(); // Oculta el login
                    }
                    else
                    {
                        // Datos incorrectos
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
            }
        }
    }
    }

