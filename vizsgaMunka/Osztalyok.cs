using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace vizsgaMunka
{
    public class Ceg
    {
        private int ID { get; set; }
        private string Nev { get; set; }
        private string Cim { get; set; }
        private int Adoszam { get; set; }
    }

    public class Fiok
    {
        public int ID { get; set; }
        public string FelhasznaloNev { get; set; }
        public string TeljesNev { get; set; }
        public string Jelszo { get; set; }
    }
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

    public class Szallitolevelek
    {
        public int ID { get; set; }
        public DateTime Datum { get; set; }
        public int ArukiadoRaktar_Raktar_ID { get; set; }
        public int BevetelezoRaktar_Raktar_ID { get; set; }
        public int Aruertek { get; set; }
        public string Megjegyzes { get; set; }

        public Szallitolevelek(int id, DateTime datum, int arukiadoRaktar_raktar_id, int bevetelezoRaktar_raktar_id, int aruertek, string megjegyzes)
        {
            this.ID = id;
            this.Datum = datum;
            this.ArukiadoRaktar_Raktar_ID = arukiadoRaktar_raktar_id;
            this.BevetelezoRaktar_Raktar_ID = bevetelezoRaktar_raktar_id;
            this.Aruertek = aruertek;
            this.Megjegyzes = megjegyzes;
        }
    }

    public class Tetelek
    {
        public int ID { get; set; }
        public int Szallitolevel_ID { get; set; }
        public string Nev { get; set; }
        public double Mennyiseg { get; set; }
        public string MennyisegiEgyseg { get; set; }
        public int Egysegar { get; set; }
        public int AFA { get; set; }
        public int BruttoAr { get; set; }

        public Tetelek(int id,int szallitolevel_ID, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int afa, int bruttoAr)
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
