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
    /// <summary>
    /// Megadja az elem esemenyállapotát
    /// </summary>
    public enum Esemeny
    {
        /// <summary>
        /// Létrehozott egy raktárat.
        /// </summary>
        RaktarCreated = 0,
        /// <summary>
        ///  Módosított egy raktárat.
        /// </summary>
        RaktarModified = 1,
        /// <summary>
        /// Törölt egy raktárat.
        /// </summary>
        RaktarDeleted = 2,
        /// <summary>
        /// Létrehozott egy terméket.
        /// </summary>
        TermekCreated = 3,
        /// <summary>
        /// Módoított egy terméket.
        /// </summary>
        TermekModified = 4,
        /// <summary>
        /// Törölt egy terméket.
        /// </summary>
        TermekDeleted = 5,
        /// <summary>
        /// Létrehozott egy átadást
        /// </summary>
        AtadasCreated = 6,
        /// <summary>
        /// Módosított egy átadást
        /// </summary>
        AtadasModified = 7,
        /// <summary>
        /// Törölt egy átadást
        /// </summary>
        AtadasDeleted = 8,
        /// <summary>
        /// Kinyomtatott egy átadást.
        /// </summary>
        AtadasPrinted = 9
    }
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
            a.lbAktivitasRefID.Content = new Seged().ToSorszam(refId);

            ertesitesekSP.Children.Add(a);
        }
        
        private string SzamFromazasaFelhNev(string felhasznaloNev)
        {
            return $"@{felhasznaloNev}";
        }

        //itt lép be a programba
        private void ujAdatHozzaadasaAktivitasok(int refId, Esemeny esemeny)
        {
            int index = (ListOfAktivitas.Count == 0) ? 0 : ListOfAktivitas[ListOfAktivitas.Count - 1].ID + 1;

            Aktivitas akt = new Aktivitas(
                id: index,
                felhasznaloId: (int)lbAktualUser.Tag,
                refId: refId,
                esemeny: esemeny,
                datum: DateTime.Now);

            ListOfAktivitas.Add(akt);
            szinkronizalasAktivitasok();
        }

        private void szinkronizalasAktivitasok()
        {
            ertesitesekSP.Children.Clear();
            for (int i = ListOfAktivitas.Count-1; i >= 0; i--)
            {
                ErtesitesElemLegeneralasa(
                    Nev: ListOfFiok[ListOfAktivitas[i].Felhasznalo_ID].TeljesNev,
                    FelhasznaloNev: ListOfFiok[ListOfAktivitas[i].Felhasznalo_ID].FelhasznaloNev,
                    Tartalom: ListOfAktivitas[i].Esemeny,
                    refId: ListOfAktivitas[i].Ref_ID,
                    datum: ListOfAktivitas[i].Datum);
            }
        }
    }
}
