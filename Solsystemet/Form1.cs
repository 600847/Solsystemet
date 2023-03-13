using System.Windows.Forms;
using System.Windows;
using SpaceSim;
using System.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Solsystemet
{
    public partial class Form1 : Form
    {
        private float centerX;
        private float centerY;
        private float centerXm;
        private float centerYm;
        private PointF venstreEdgePunkt;
        private List<RectangleF> plassering;
        private float currentX;
        private float str;
        private float skalering;
        private Boolean visInfo;

        private System.Windows.Forms.Label mouseCoordinatesLabel = new Label();


        private List<SpaceObject> solarSystem = new List<SpaceObject>
            {
                new Star("The sun")
                {
                    X = 0,
                    Y = 0,
                    ObjectRadius = 695700,
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


            // Add key event handlers
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.MouseClick += new MouseEventHandler(control_MouseClick);

            //Setter fullscreen og bakgrunsfarge til svart
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Black;



            Screen primaryScreen = Screen.PrimaryScreen;
            int screenWidth = primaryScreen.Bounds.Width;
            int screenHeight = primaryScreen.Bounds.Height;
            centerX = screenWidth / 2;
            centerY = screenHeight / 2;
            centerYm = centerY;
            currentX = centerX;

            //Størrelse forhold
            str = 1500;
            //Definere avstandsforholdet mellom planetene
            skalering = 15000;

            //Interval og animation
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

            //Setter interval på 100 ms
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();

            //Tegne event
            this.Paint += Form1_Paint;
        }

private void control_MouseClick(object sender, MouseEventArgs e)
        {
           foreach(RectangleF r in plassering)
            {
                if (r.Contains(e.Location))
                {
                    Screen primaryScreen = Screen.PrimaryScreen;
                    int screenWidth = primaryScreen.Bounds.Width;
                    int center = screenWidth / 2;
                    centerX -= (e.X - center);
                    centerXm += (e.Location.X);

                    if (str == 1500)
                    {
                        str = str / 6;             
                    }
                    else
                    {
                        str = str * 6;
                    }
                    Invalidate();
                }
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    // Move the solar system to the right by 10 pixels
                    centerX += 100;
                    centerXm += 100;
                    break;
                case Keys.Right:
                    // Move the solar system to the left by 10 pixels
                    centerX -= 100;
                    centerXm -= 100;
                    break;
                case Keys.Up:
                    // Move the solar system down by 10 pixels
                    centerY += 100;
                    centerYm += 100;
                    break;
                case Keys.Down:
                    // Move the solar system up by 10 pixels
                    centerY -= 100;
                    centerYm -= 100;
                    break;
            }

            Invalidate();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            pos.X = pos.X + retning.X;
            pos.Y = pos.Y + retning.Y;

            //Oppdatering av vindu
            
        }


        //En metode/event
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            plassering = new List<RectangleF>();
            base.OnPaint(e);
            //Tegneflaten
            Graphics g = e.Graphics;
            Font font1 = new Font("Arial", 12, FontStyle.Bold);
            Brush brush = Brushes.White;
            //Variabler
            float width;
            string info;
            SizeF size;
            SolidBrush b;
            RectangleF rect;

            foreach (SpaceObject spaceObject in solarSystem)
            {
                //Regner ut bredden på planeten som skal tegnes
                width = regnUtBredde(spaceObject, str);
                //X er helt til venstre av objektet. Slik at x blir da (centerX - width / 2)
                venstreEdgePunkt = regnUtPos(spaceObject, width, skalering);
                //Henter farge 
                b = new SolidBrush(spaceObject.ObjectColor);
                //Lager hit box
                rect = new RectangleF(venstreEdgePunkt.X, venstreEdgePunkt.Y - width / 2, width, width);
                plassering.Add(rect);
                info = "" + spaceObject.name;
                size = e.Graphics.MeasureString(info, font1);
                g.DrawString(info, font1, brush, venstreEdgePunkt.X, (venstreEdgePunkt.Y - width / 2)*0.9f);


                g.FillEllipse(b, rect);
                for (int i = 0; i < 100; i++)
                {
                    //Sjekk om planet har måne
                    if (spaceObject.moons[i] != null)
                    {
                        //Finner center av parent planet
                        centerXm = venstreEdgePunkt.X + (width / 2);
                        width = regnUtBredde(spaceObject.moons[i], str);
                        venstreEdgePunkt = regnUtPosMoon(spaceObject.moons[i], width, skalering);
                        b = new SolidBrush(spaceObject.moons[i].ObjectColor);
                        rect = new RectangleF(venstreEdgePunkt.X, venstreEdgePunkt.Y - width / 2, width, width);
                        plassering.Add(rect);
                        g.FillEllipse(b, rect);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Skriver avstand og størrelse på planetene
            string text1 = "One pixel in width is " + str + "km";
            string text2 = "One pixel in distance from the center of planets is AU/" + skalering;

            // Get the size of the text elements
            SizeF size1 = e.Graphics.MeasureString(text1, font1);
            SizeF size2 = e.Graphics.MeasureString(text2, font1);

            // Calculate the position of the text elements
            float x1 = 10;
            float y1 = 10;
            float x2 = x1;
            float y2 = y1 + size1.Height;

            // Draw the text elements
            g.DrawString(text1, font1, brush, x1, y1);
            g.DrawString(text2, font1, brush, x2, y2);
        }
        public PointF regnUtPos(SpaceObject s, float width, float skalering)
        {
            return new PointF(centerX - (width / 2) + s.X * skalering, centerY + s.Y);
        }

        public PointF regnUtPosMoon(SpaceObject s, float width, float skalering)
        {
            return new PointF(centerXm - (width / 2) + s.X * skalering, centerYm + s.Y);
        }

        public float regnUtBredde(SpaceObject s, float str)
        { //Gjør om fra radius til diameter = width
            return (s.ObjectRadius / str) * 2;
        }
    }
}