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

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Termek> ListOfTermekek = new List<Termek>();

        partial void BtnTermekekreVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button) sender).ToolTip.ToString())
            {
                case "termék hozzáadása":
                    termekHozzaadasa.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    txbTermekNevHozzaadasa.Text = String.Empty;
                    txbTermekAFAHozzaadasa.Text = String.Empty;
                    txbTermekMEHozzaadasa.Text = String.Empty;
                    txbTermekArHozzaadasa.Text = String.Empty;
                    break;
                case "termék módosítása":
                    termekModositasa.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    int index = indexOfSelectedRowTermekek();
                    txbTermekNevModositasa.Text = ListOfTermekek[index].Nev;
                    txbTermekAFAModositasa.Text = ListOfTermekek[index].AFA;
                    txbTermekMEModositasa.Text = ListOfTermekek[index].MennyisegiEgyseg;
                    txbTermekArModositasa.Text = ListOfTermekek[index].Egysegar;
                    break;
                case "termék törlése":
                    termekTorlese.Visibility = Visibility.Visible;
                    break;
                case "szinkronizálás":
                    szinkronizalasTermekek();
                    break;
            }
        }

        partial void BtnTermekEsemenyek(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowTermekek();
            if (((Button)sender).ToolTip != null)
            {
                //Ablak bezarasa gomb eseménye
                termekHozzaadasa.Visibility = Visibility.Collapsed;
                termekModositasa.Visibility = Visibility.Collapsed;
                termekTorlese.Visibility = Visibility.Collapsed;
            }
            else
            {
                switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
                {
                    case "Mentés":
                        //Mentés gomb eseménye
                        termekHozzaadasa.Visibility = Visibility.Collapsed;
                        ujAdatHozzaadasaTermekez(spTermekekTabla.Children.Count,
                                                  txbTermekNevHozzaadasa.Text,
                                                  txbTermekAFAHozzaadasa.Text,
                                                  txbTermekMEHozzaadasa.Text, txbTermekArHozzaadasa.Text);
                        break;
                    case "Módosítás":
                        //Módosítás gomb eseménye
                        termekModositasa.Visibility = Visibility.Collapsed;
                        TermekAdatModositasa(index,
                                             txbTermekNevModositasa.Text,
                                             txbTermekAFAModositasa.Text,
                                             txbTermekMEModositasa.Text,
                                             txbTermekArModositasa.Text);
                        break;
                    case "Igen":
                        //meglévő termék törlése "igen"
                        termekTorlese.Visibility = Visibility.Collapsed;
                        TermekAdatTorlese(index);
                        break;
                    default:
                        //meglévő termék törlése "nem"
                        termekTorlese.Visibility = Visibility.Collapsed;
                        break;
                }
            }


        }

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

            TableRowWith5Column tableROW = new TableRowWith5Column();
            tableROW.egy.Content = id;
            tableROW.ketto.Content = nev;
            tableROW.ketto.HorizontalContentAlignment = HorizontalAlignment.Left;
            tableROW.harom.Content = afa;
            tableROW.negy.Content = mennyisegiEgyseg;
            tableROW.ot.Content = egysegar;
            tableROW.ot.Padding = new Thickness(0, 0, 45, 0);
            tableROW.ot.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.hatter.Background = hatterSzin;

            btn.Content = tableROW;
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
