using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System;

namespace SpaceSim
{
    public class SpaceObject
    {

        public String name { get; }

        //In AU
        public double OrbitalRadius { get; set; }

        //In days
        public double OrbitalPeriod { get; set; }


        public double Velocity { get; set; }

        //In km
        public int ObjectRadius { get; set; }

        public Color ObjectColor { get; set; }

        //In AU
        public double X { get; set; }

        //In AU
        public double Y { get; set; }


        //Måner til space objects
        SpaceObject[] moons = new Moon[100];
        public SpaceObject this[int i]
        {
            get
            {
                return moons[i];
            }
            set
            {
                moons[i] = value;
            }
        }

        public SpaceObject(String name)
        {
            this.name = name;
        }

        //Metoden tar for seg at alle planetene starter på y-asken = 0 når tiden er 0
        //Dvs alle plantene ligger vertikalt på akkurat samme høyde på tidspunkt 0
        //Posisjonen til solen er (0, 0)
        //Posisjonene er gitt i AU
        // One unit of AU = 149.6 million kilometers
        //Regner OrbitalPeriod i AU, slik at earth sin Orbital Period = 1 AU
        public virtual void RegnUtPos(int day)
        {
            double newDay = day;
            //Så lenge tiden ikke er null
            if (newDay != 0)
            {
                //Sjekk om planeten har gått en runde rundt solen
                if (newDay > this.OrbitalPeriod)
                {
                    //Finne resten etter hele år
                    newDay = day % this.OrbitalPeriod;
                }
                //Regning av nye x og y kordinater

                //Radianer

                double theta = 2 * Math.PI * (newDay / this.OrbitalPeriod);
                this.X = this.OrbitalRadius * Math.Cos(theta);
                this.Y = this.OrbitalRadius * Math.Sin(theta);

                this.Draw();

                //Må regne på hvor månene flytter seg
                //Kjøre en løkke gjennom alle månene som finnes i objekter
                for (int i = 0; i < 100; i++)
                {
                    if (this.moons[i] != null)
                    {
                        this.moons[i].RegnUtPos(day);
                    }
                    else
                    {
                        break;
                    }
                }



            }
        }

        public virtual void Draw()
        {
            Console.WriteLine(name);
            Console.WriteLine(
                             "Orbital radius: " + this.OrbitalRadius + "\n"
                            + "Orbital period: " + this.OrbitalPeriod + "\n"
                            + "Object radius: " + this.ObjectRadius + "\n"
                            + "Object color: " + this.ObjectColor + "\n"
                            + "Current posistion: (" + this.X + ", " + this.Y + ")" + "\n"
                            );
            Console.WriteLine();
        }
    }

    public class Star : SpaceObject
    {
        public Star(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(String name) : base(name) { }

        //In hours
        public double RotationalPeriod { get; set; }

        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }
    }

    public class Moon : Planet
    {
        public Moon(String name) : base(name) { }

        public override void Draw()
        {
            Console.Write("Moon  : ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(String name) : base(name) { }

        public override void Draw()
        {
            Console.Write("Comet :");
            base.Draw();
        }
    }

    public class Asteroid : SpaceObject
    {
        public Asteroid(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid :");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid belt :");
            base.Draw();
        }
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Dwarf planet :");
            base.Draw();
        }
    }
}