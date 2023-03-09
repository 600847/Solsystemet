using System.Windows.Forms;
using System.Windows;
using SpaceSim;
using System.Resources;

namespace Solsystemet
{
    public partial class Form1 : Form
    {
        private float centerX;
        private float centerY;

        private List<SpaceObject> solarSystem = new List<SpaceObject>
            {
                new Star("The sun")
                {
                    X = 0,
                    Y = 0,
                    ObjectRadius = 8000,
                    ObjectColor = Color.Yellow
                },
                new Planet("Mecury")
                {
                    OrbitalRadius = 0.39f,
                    OrbitalPeriod = 88,
                    ObjectRadius = 2440,
                    RotationalPeriod = 1416,
                    ObjectColor = Color.SlateGray,
                    X = 0.39f,
                    Y = 0
                },
                new Planet("Venus")
                {
                    OrbitalRadius = 0.73f,
                    OrbitalPeriod = 584,
                    ObjectRadius = 6052,
                    RotationalPeriod = 5832,
                    ObjectColor = Color.LightYellow,
                    X = 0.73f,
                    Y = 0
                },
                new Planet("Earth")
                {
                   OrbitalRadius = 1.00f,
                   OrbitalPeriod = 365.25,
                   ObjectRadius = 6371,
                   RotationalPeriod = 24,
                   ObjectColor = Color.Blue,
                   X = 1.00f,
                   Y = 0,
                   [0] = new Moon("The moon")
                   {
                       OrbitalRadius = 0.00257f,
                       OrbitalPeriod = 27,
                       ObjectRadius = 1740, //1740
                       RotationalPeriod = 27,
                       ObjectColor = Color.Gray,
                       X = 0.00257f, //forhold til Earth
                       Y = 0
                   }
                },
                new Planet("Mars")
                {
                    OrbitalRadius = 1.38f,
                    OrbitalPeriod = 686.98,
                    ObjectRadius = 3390,
                    RotationalPeriod = 24.62,
                    ObjectColor = Color.OrangeRed,
                    X = 1.38f,
                    Y = 0
                },
                 new Planet("Jupiter")
                {
                    OrbitalRadius = 5.20f,
                    OrbitalPeriod = 4333,
                    ObjectRadius = 69911,
                    RotationalPeriod = 9.93,
                    ObjectColor = Color.LightGoldenrodYellow,
                    X = 5.20f,
                    Y = 0
                },
                new Planet("Saturn")
                {
                    OrbitalRadius = 9.58f,
                    OrbitalPeriod = 10756,
                    ObjectRadius = 58232,
                    RotationalPeriod = 10.7,
                    ObjectColor = Color.SandyBrown,
                    X = 9.58f,
                    Y = 0
                },
                new Planet("Uranus")
                {
                    OrbitalRadius = 19.22f,
                    OrbitalPeriod = 30687,
                    ObjectRadius = 25362,
                    RotationalPeriod = 17,
                    ObjectColor = Color.DarkSeaGreen,
                    X = 19.22f,
                    Y = 0
                },
                new Planet("Neptun")
                {
                    OrbitalRadius = 30.10f,
                    OrbitalPeriod = 60182,
                    ObjectRadius = 24622,
                    RotationalPeriod = 19,
                    ObjectColor = Color.Blue,
                    X = 30.10f,
                    Y = 0
                }
        };


        private Point pos = new(0, 0);
        private Point retning = new(5, 5);
        public Form1()
        {
            InitializeComponent();

            //Setter fullscreen og bakgrunsfarge til svart
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;

            Screen primaryScreen = Screen.PrimaryScreen;
            int screenWidth = primaryScreen.Bounds.Width;
            int screenHeight = primaryScreen.Bounds.Height;
            centerX = screenWidth / 2;
            centerY = screenHeight / 2;



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
        }

        private void nullstillCenter()
        {
            Screen primaryScreen = Screen.PrimaryScreen;
            int screenWidth = primaryScreen.Bounds.Width;
            int screenHeight = primaryScreen.Bounds.Height;
            centerX = screenWidth / 2;
            centerY = screenHeight / 2;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Tegneflaten
            Graphics g = e.Graphics;

            //Variabler
            float width;
            PointF venstreEdgePunkt;
            SolidBrush b;

            //Størrelse forhold
            //Definerer at radiusen på objektene skal være 200 ganger mindre
            float str = 300;
            //Definere avstandsforholdet mellom planetene
            float skalering = 500;

            foreach (SpaceObject spaceObject in solarSystem)
            {
                //Regner ut bredden på planeten som skal tegnes
                width = regnUtBredde(spaceObject, str);
                //X er helt til venstre av objektet. Slik at x blir da (centerX - width / 2)
                venstreEdgePunkt = regnUtPos(spaceObject, width, skalering);
                //Henter farge 
                b = new SolidBrush(spaceObject.ObjectColor);
                g.FillEllipse(b, venstreEdgePunkt.X, venstreEdgePunkt.Y - width / 2, width, width);
                for (int i = 0; i < 100; i++)
                {
                    //Sjekk om planet har måne
                    if (spaceObject.moons[i] != null)
                    {
                        //Finner center av parent planet
                        centerX = venstreEdgePunkt.X + (width * 2);
                        width = regnUtBredde(spaceObject.moons[i], str);
                        venstreEdgePunkt = regnUtPos(spaceObject.moons[i], width, skalering);
                        b = new SolidBrush(spaceObject.moons[i].ObjectColor);
                        g.FillEllipse(b, venstreEdgePunkt.X, venstreEdgePunkt.Y - width / 2, width, width);
                    }
                    else
                    {
                        nullstillCenter();
                        break;
                    }
                }
            }

            //Skriver avstand og størrelse på planetene
            string text1 = "One pixel in width is " + str + "km";
            string text2 = "One pixel in distance from the center of planets is AU/" + skalering;
            Font font1 = new Font("Arial", 12, FontStyle.Bold);
            Font font2 = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = Brushes.White;

            // Get the size of the text elements
            SizeF size1 = e.Graphics.MeasureString(text1, font1);
            SizeF size2 = e.Graphics.MeasureString(text2, font2);

            // Calculate the position of the text elements
            float x1 = 10;
            float y1 = 10;
            float x2 = x1;
            float y2 = y1 + size1.Height;

            // Draw the text elements
            e.Graphics.DrawString(text1, font1, brush, x1, y1);
            e.Graphics.DrawString(text2, font2, brush, x2, y2);
        }
        public PointF regnUtPos(SpaceObject s, float width, float skalering)
        {
            return new PointF(centerX - (width / 2) + s.X * skalering, centerY);
        }


        public PointF regnUtPosMoon(SpaceObject s, float width, float skalering, SpaceObject m)
        {
            return new PointF();
        }

        public float regnUtBredde(SpaceObject s, float str)
        {
            //Gjør om til diameter / width
            return (s.ObjectRadius / str) * 2;
        }




    }
}