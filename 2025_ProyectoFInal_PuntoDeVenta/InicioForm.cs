using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2025_ProyectoFInal_PuntoDeVenta
{
    public partial class InicioForm: Form
    {
        public InicioForm()
        {
            InitializeComponent();
        }

        private void InicioForm_Load(object sender, EventArgs e)
        {
            CentrarPictureBox();

        }

        private void InicioForm_Resize(object sender, EventArgs e)
        {
            CentrarPictureBox();

        }
        private void CentrarPictureBox()
        {
            // Centrar el PictureBox en el formulario
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;

            // Centrar CircularProgressBar
            circularProgressBar1.Left = (this.ClientSize.Width - circularProgressBar1.Width) / 2;
            circularProgressBar1.Top = (this.ClientSize.Height - circularProgressBar1.Height) / 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            circularProgressBar1.Value += 1;
            if (circularProgressBar1.Value >= 100)
            {
                timer1.Stop();
                // Aquí puedes mostrar tu formulario principal, por ejemplo
                LoginForm LoginForm = new LoginForm();
                LoginForm.Show();
                this.Hide();

            }
        }
    }
}
   
