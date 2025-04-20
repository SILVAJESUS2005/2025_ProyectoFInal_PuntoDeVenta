using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2025_ProyectoFInal_PuntoDeVenta
{
    public partial class CircularProgressBar : UserControl
    {
        private int _value = 0;
        private int _max = 100;
        private Color _barColor = Color.Black;

        public CircularProgressBar()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
        }

        private void CircularProgressBar_Load(object sender, EventArgs e)
        {

        }

        [Category("Behavior")]
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Min(_max, Math.Max(0, value));
                this.Invalidate();
            }
        }

        [Category("Behavior")]
        public int MaxValue
        {
            get => _max;
            set
            {
                _max = value;
                this.Invalidate();
            }
        }

        [Category("Appearance")]
        public Color BarColor
        {
            get => _barColor;
            set
            {
                _barColor = value;
                this.Invalidate();
            }
        }



    

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(10, 10, this.Width - 20, this.Height - 20);
            using (Pen backgroundPen = new Pen(Color.LightGray, 10))
            using (Pen progressPen = new Pen(_barColor, 10))
            {
                e.Graphics.DrawArc(backgroundPen, rect, 0, 360);
                float sweepAngle = 360f * _value / _max;
                e.Graphics.DrawArc(progressPen, rect, -90, sweepAngle);
            }
        }

    }
}
