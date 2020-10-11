using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace vizsgaMunka
{
    public class Raktar
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public string Hely { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public Raktar(int id, string nev, string hely, string telefon, string email)
        {
            this.ID = id;
            this.Nev = nev;
            this.Hely = hely;
            this.Telefon = telefon;
            this.Email = email;
        }
    }

    public class Termek
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public string AFA { get; set; }
        public string MennyisegiEgyseg { get; set; }
        public string Egysegar { get; set; }

        public Termek(int id, string nev, string afa, string mennyisegiEgyseg, string egysegar)
        {
            this.ID = id;
            this.Nev = nev;
            this.AFA = afa;
            this.MennyisegiEgyseg = mennyisegiEgyseg;
            this.Egysegar = egysegar;
        }
    }
}
