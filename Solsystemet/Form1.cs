using System.Windows.Forms;
using SpaceSim;
namespace Solsystemet
{
    public partial class Form1 : Form
    {
        private Point pos = new(0, 0);
        private Point retning = new(5, 5);
        public Form1()
        {
            InitializeComponent();
            //Interval og animation
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

            //Setter interval på 100 ms
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();

            //Tegne event
            this.Paint += Form1_Paint;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            pos.X = pos.X + retning.X;
            pos.Y = pos.Y + retning.Y;

            //Oppdatering av vindu
            Invalidate();
        }


        //En metode/event
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Brush b = new SolidBrush(Color.Blue);

            g.FillEllipse(b, 200, 200, 100, 100);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Tegneflaten
            Graphics g = e.Graphics;

            Brush b = new SolidBrush(Color.Red);

            g.FillEllipse(b, pos.X, pos.Y, 100, 100);

        





        }
    }
}