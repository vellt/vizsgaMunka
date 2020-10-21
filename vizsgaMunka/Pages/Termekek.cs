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

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Termek> ListOfTermekek = new List<Termek>();

        /// <summary>
        /// Szinkronizálja a táblát
        /// </summary>
        private void szinkronizalasTermekek()
        {
            spTermekekTabla.Children.Clear();
            for (int i = 0; i < ListOfTermekek.Count; i++) tablazatKialakitasaTermekek(ListOfTermekek[i].ID, ListOfTermekek[i].Nev, ListOfTermekek[i].AFA, ListOfTermekek[i].MennyisegiEgyseg, ListOfTermekek[i].Egysegar, (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spTermekekTabla.Parent).ScrollToEnd();
        }

        /// <summary>
        /// Beszúrja a sorokat
        /// </summary>
        private void tablazatKialakitasaTermekek(int id, string nev, string afa, string mennyisegiEgyseg, string egysegar, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TablaSorAdatok rtsa = new TablaSorAdatok();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = nev;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).HorizontalContentAlignment = HorizontalAlignment.Center;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content = afa;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = mennyisegiEgyseg;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).HorizontalContentAlignment = HorizontalAlignment.Right;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Padding = new Thickness(0, 0, 40, 0);
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = egysegar;

            ((Grid)((Button)rtsa.Content).Content).Background = hatterSzin;

            btn.Content = rtsa;
            btn.Click += SorokKijeloleseKattintasera;

            Grid gr0 = new Grid();
            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(7);
            ColumnDefinition colDef2 = new ColumnDefinition();
            gr0.ColumnDefinitions.Add(colDef1);
            gr0.ColumnDefinitions.Add(colDef2);

            Grid gr = new Grid();
            gr.Height = 30;
            gr.Background = hatterSzin;

            Grid.SetColumn(gr, 0);
            Grid.SetColumn(btn, 1);


            gr0.Children.Add(gr);
            gr0.Children.Add(btn);
            spTermekekTabla.Children.Add(gr0);
            //törlés módosítás gomb eltüntetése
            btnTermekekTablaTorles.Visibility = Visibility.Collapsed;
            btnTermekekTablaModositas.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Megkeresi az indexét a kijeleölt sornak
        /// </summary>
        private int indexOfSelectedRowTermekek()
        {
            var background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF05B422"));
            for (int i = 0; i < spTermekekTabla.Children.Count; i++)
            {
                var background2 = ((Grid)((Grid)spTermekekTabla.Children[i]).Children[0]).Background;
                if (background2.ToString() == background.ToString())
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Törli a kijelölt sort a táblázatból
        /// </summary>
        private void TermekAdatTorlese(int index)
        {
            ListOfTermekek.RemoveAt(index);
            szinkronizalasTermekek();
        }

        /// <summary>
        /// A meglévő táblázat egy kijelölt sorának adatát módódítja
        /// </summary>
        private void TermekAdatModositasa(int index, string nev, string afa, string mennyisegiEgyseg, string egysear)
        {
            ListOfTermekek[index].Nev = nev;
            ListOfTermekek[index].AFA = afa;
            ListOfTermekek[index].MennyisegiEgyseg = mennyisegiEgyseg;
            ListOfTermekek[index].Egysegar = egysear;
            szinkronizalasTermekek();
        }

        /// <summary>
        /// A táblázatba hozzáad egy sort, a beállított argumentumok alapján
        /// </summary>
        private void ujAdatHozzaadasaTermekez(int index, string nev, string afa, string mennyisegiEgyseg, string egysear)
        {
            Termek r = new Termek(index, nev, afa, mennyisegiEgyseg, egysear);
            ListOfTermekek.Add(r);
            szinkronizalasTermekek();
        }
    }
}
