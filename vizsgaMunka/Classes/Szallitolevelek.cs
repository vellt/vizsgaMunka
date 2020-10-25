using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka
{
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
}
