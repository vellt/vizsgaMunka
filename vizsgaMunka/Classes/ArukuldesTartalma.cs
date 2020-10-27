using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka.Classes
{
    public class ArukuldesTartalma
    {
        public int ID { get; set; }
        public int Szallitolevel_ID { get; set; }
        public string Nev { get; set; }
        public double Mennyiseg { get; set; }
        public string MennyisegiEgyseg { get; set; }
        public int Egysegar { get; set; }
        public int AFA { get; set; }
        public int BruttoAr { get; set; }

        public ArukuldesTartalma(int id, int szallitolevel_ID, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int afa, int bruttoAr)
        {
            this.ID = id;
            this.Szallitolevel_ID = szallitolevel_ID;
            this.Nev = nev;
            this.Mennyiseg = mennyiseg;
            this.MennyisegiEgyseg = mennyisegiEgyseg;
            this.Egysegar = egysegar;
            this.AFA = afa;
            this.BruttoAr = bruttoAr;
        }
    }
}
