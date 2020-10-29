using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vizsgaMunka.VectorIcons;
using vizsgaMunka.DesignPatterns;
using vizsgaMunka.Classes;

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Eseményeket ad hozzá a StackPanelhez
        /// </summary>
        private void ErtesitesElemLegeneralasa(string Nev, string FelhasznaloNev, string Tartalom, int refId, DateTime datum)
        {
            ActivityRowForList a = new ActivityRowForList();
            a.lbTeljesNev.Content = Nev;
            a.lbFelhasznaloNev.Content =SzamFromazasaFelhNev(FelhasznaloNev);
            a.datum.Content = datum.ToString("yyyy.MM.dd. HH:mm");
            a.lbAktivitasLeirasa.Content = Tartalom;
            a.lbAktivitasRefID.Content =SzamFormazasaSorszam(refId);

            ertesitesekSP.Children.Add(a);
        }
        
        private string SzamFromazasaFelhNev(string felhasznaloNev)
        {
            return $"@{felhasznaloNev}";
        }

        //itt lép be a programba
        /// <summary>
        ///<para>ESEMÉNY KÓDOK</para> 
        ///<para> 0: Raktárt hozott létre </para> 
        ///<para> 1: Raktárt módosított</para> 
        ///<para> 2: Raktárt törölt</para>
        ///<para> - </para>
        ///<para> 3: Terméket hozott létre</para>
        ///<para> 4: Terméket módosított</para>
        ///<para> 5: Terméket törölt</para>
        ///<para> - </para>
        ///<para> 6: Átadást állított ki</para>
        ///<para> 7: Átadást módosított</para>
        ///<para> 8: Átadást törölt</para>
        ///<para> 9: Átadást nyomtatott</para>
        /// </summary>
        private void ujAdatHozzaadasaAktivitasok(int refId, int esemenyCode)
        {
            Aktivitas akt = new Aktivitas(
                (ListOfAktivitas.Count == 0) ? 0 : ListOfAktivitas[ListOfAktivitas.Count - 1].ID + 1,
                Convert.ToInt32(lbAktualUser.Tag),
                refId,
                esemenyCode,
                DateTime.Now
            );
            ListOfAktivitas.Add(akt);
            szinkronizalasAktivitasok();
        }

        private void szinkronizalasAktivitasok()
        {
            ertesitesekSP.Children.Clear();
            for (int i = ListOfAktivitas.Count-1; i >= 0; i--)
            {
                ErtesitesElemLegeneralasa(
                    ListOfFiok[ListOfAktivitas[i].Felhasznalo_ID].TeljesNev, 
                    ListOfFiok[ListOfAktivitas[i].Felhasznalo_ID].FelhasznaloNev, 
                    ListOfAktivitas[i].Esemeny,
                    ListOfAktivitas[i].Ref_ID,
                    ListOfAktivitas[i].Datum);
            }
        }
    }
}
