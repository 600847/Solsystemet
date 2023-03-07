namespace Solsystemet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //Test push
            base.OnPaint(e);

            //Tegneflaten
            Graphics g = e.Graphics;

            Brush b = new SolidBrush(Color.Red);

            g.FillEllipse(b, 100, 100, 100, 100);





        }
    }
}