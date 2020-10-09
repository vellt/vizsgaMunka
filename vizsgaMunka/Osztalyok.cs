using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vizsgaMunka
{
    class Raktar
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
}
