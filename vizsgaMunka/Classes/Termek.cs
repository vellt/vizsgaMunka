using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka
{
    public class Termek
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public int AFA { get; set; }
        public string MennyisegiEgyseg { get; set; }
        public int Egysegar { get; set; }

        public Termek(int id, string nev, int afa, string mennyisegiEgyseg, int egysegar)
        {
            this.ID = id;
            this.Nev = nev;
            this.AFA = afa;
            this.MennyisegiEgyseg = mennyisegiEgyseg;
            this.Egysegar = egysegar;
        }
    }
}
