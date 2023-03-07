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
                    ObjectRadius = 2000,
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

            //Henter X og Y center kordinater
            centerX = 925;
            centerY = 500;

 

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


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Tegneflaten
            Graphics g = e.Graphics;
            
            
            Brush b;
            PointF point;
            float size;
            foreach (SpaceObject spaceObject in solarSystem)
            {
                point = regnUtPos(spaceObject);
                b = new SolidBrush(spaceObject.ObjectColor);
                size = regnUtStr(spaceObject);
                g.FillEllipse(b, point.X, point.Y - size / 2, size, size);

                //Loop for måner
                for (int i = 0; i < 100; i++)
                {
                    //Sjekk om planet har måne
                    if (spaceObject.moons[i] != null)
                    {
                        PointF pointM = regnUtPosMoon(spaceObject, spaceObject.moons[i]);
                        b = new SolidBrush(spaceObject.moons[i].ObjectColor);
                        float sizeM = regnUtStr(spaceObject.moons[i]);
                         g.FillEllipse(b, pointM.X, pointM.Y - sizeM / 2, sizeM, sizeM);
                    }
                    else
                    {
                        break;
                    }

                }

            }
            
        }

        public PointF regnUtPos(SpaceObject s)
        {
            return new PointF(s.X * 500 + centerX, s.Y + centerY);
        }

        public PointF regnUtPosMoon(SpaceObject s, SpaceObject m)
        {

            return new PointF(m.X * 500 + s.X * 500 + centerX + (s.ObjectRadius/50), m.Y + s.Y + centerY);
        }

        public float regnUtStr(SpaceObject s)
        {
            return s.ObjectRadius / 100;
        }

       


    }
}