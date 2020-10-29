using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka.Classes
{
    public class Fiok
    {
        public int ID { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Jelszo { get; set; }

        public Fiok(int id, string felhaszanloNev, string TeljesNev, string Jelszo)
        {
            this.ID = id;
            this.FelhasznaloNev = felhaszanloNev;
            this.TeljesNev = TeljesNev;
            this.Jelszo = Jelszo;
        }
    }

}
