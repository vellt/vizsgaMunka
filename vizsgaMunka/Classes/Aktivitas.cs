using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka.Classes
{
    class Aktivitas
    {
        public int ID { get; set; }
        public int Felhasznalo_ID { get; set; }
        public string Esemeny { get; set; }
        public int Ref_ID { get; set; }
        public DateTime Datum { get; set; }

        private string EsemenySetByeCode(Esemeny Code)
        {
            string result = String.Empty;
            switch ((int)Code)
            {
                case 0: result = "Raktárt hozott létre"; break;
                case 1: result = "Raktárt módosított"; break;
                case 2: result = "Raktárt törölt"; break;

                case 3: result = "Terméket hozott létre"; break;
                case 4: result = "Terméket módosított"; break;
                case 5: result = "Terméket törölt"; break;

                case 6: result = "Átadást állított ki"; break;
                case 7: result = "Átadást módosított"; break;
                case 8: result = "Átadást törölt"; break;
                case 9: result = "Átadást nyomtatott"; break;
            }
            return result;
        }

        public Aktivitas(int id, int felhasznaloId, int refId, Esemeny esemeny, DateTime datum)
        {
            this.ID = id;
            this.Felhasznalo_ID = felhasznaloId;
            this.Esemeny = EsemenySetByeCode(esemeny);
            this.Ref_ID = refId;
            this.Datum = datum;
        }
    }
}
