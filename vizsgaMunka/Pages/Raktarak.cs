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
        List<Classes.Raktar> ListOfRaktarak = new List<Classes.Raktar>();

        /// <summary>
        /// Megkeresi az indexét a kijeleölt sornak
        /// </summary>
        private int indexOfSelectedRowRaktarak()
        {
            var background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF05B422"));
            for (int i = 0; i < spRaktartTabla.Children.Count; i++)
            {
                var background2 = ((Grid)((Grid)spRaktartTabla.Children[i]).Children[0]).Background;
                if (background2.ToString() == background.ToString())
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Törli a kijelölt sort a táblázatból
        /// </summary>
        partial void BtnRaktarakraVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).ToolTip.ToString())
            {
                case "raktár hozzáadása":
                    raktarHozzaadasa.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    txbRaktarNevHozzaadasa.Text = String.Empty;
                    txbRaktarHelyHozzaadasa.Text = String.Empty;
                    txbRaktarTelefonHozzaadasa.Text = String.Empty;
                    txbRaktarEmailHozzaadasa.Text = String.Empty;
                    break;
                case "raktár módosítása":
                    raktarModositasa.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    int index = indexOfSelectedRowRaktarak();
                    txbRaktarNevModositasa.Text = ListOfRaktarak[index].Nev;
                    txbRaktarHelyModositasa.Text = ListOfRaktarak[index].Hely;
                    txbRaktarTelefonModositasa.Text = ListOfRaktarak[index].Telefon;
                    txbRaktarEmailModositasa.Text = ListOfRaktarak[index].Email;
                    break;
                case "raktár törlése":
                    raktarTorlese.Visibility = Visibility.Visible;
                    break;
                case "szinkronizálás":
                    szinkronizalasRaktarak();
                    break;
            }
        }

        partial void BtnRaktarEsemenyek(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowRaktarak();
            if (((Button)sender).ToolTip != null)
            {
                //Ablak bezarasa gomb eseménye
                raktarHozzaadasa.Visibility = Visibility.Collapsed;
                raktarModositasa.Visibility = Visibility.Collapsed;
                raktarTorlese.Visibility = Visibility.Collapsed;
            }
            else
            {
                switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
                {
                    case "Mentés":
                        //Mentés gomb eseménye
                        raktarHozzaadasa.Visibility = Visibility.Collapsed;
                        ujAdatHozzaadasaRaktarhoz(spRaktartTabla.Children.Count,
                                                  txbRaktarNevHozzaadasa.Text,
                                                  txbRaktarHelyHozzaadasa.Text,
                                                  txbRaktarTelefonHozzaadasa.Text, txbRaktarEmailHozzaadasa.Text);
                        break;
                    case "Módosítás":
                        //Módosítás gomb eseménye
                        raktarModositasa.Visibility = Visibility.Collapsed;
                        RaktarAdatModositasa(index,
                                             txbRaktarNevModositasa.Text,
                                             txbRaktarHelyModositasa.Text,
                                             txbRaktarTelefonModositasa.Text,
                                             txbRaktarEmailModositasa.Text);
                        break;
                    case "Igen":
                        //meglévő raktar törlése "igen"
                        raktarTorlese.Visibility = Visibility.Collapsed;
                        RaktarAdatTorlese(index);
                        break;
                    default:
                        //meglévő raktar törlése "nem"
                        raktarTorlese.Visibility = Visibility.Collapsed;
                        break;
                }
            }

        }

        partial void RaktarAdatTorlese(int index)
        {
            ListOfRaktarak.RemoveAt(index);
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// A meglévő táblázat egy kijelölt sorának adatát módódítja
        /// </summary>
        partial void RaktarAdatModositasa(int index, string nev, string hely, string telefon, string email)
        {
            ListOfRaktarak[index].Nev = nev;
            ListOfRaktarak[index].Hely = hely;
            ListOfRaktarak[index].Telefon = telefon;
            ListOfRaktarak[index].Email = email;
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// A táblázatba hozzáad egy sort, a beállított argumentumok alapján
        /// </summary>
        partial void ujAdatHozzaadasaRaktarhoz(int index, string nev, string hely, string telefon, string email)
        {
            Classes.Raktar r = new Classes.Raktar(index, nev, hely, telefon, email);
            ListOfRaktarak.Add(r);
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// Szinkronizálja a táblát
        /// </summary>
        partial void szinkronizalasRaktarak()
        {
            spRaktartTabla.Children.Clear();
            for (int i = 0; i < ListOfRaktarak.Count; i++) tablazatKialakitasaRaktarak
                    (ListOfRaktarak[i].ID, 
                    ListOfRaktarak[i].Nev, 
                    ListOfRaktarak[i].Hely, 
                    ListOfRaktarak[i].Telefon, 
                    ListOfRaktarak[i].Email, 
                    (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spRaktartTabla.Parent).ScrollToEnd();
        }

        /// <summary>
        /// Beszúrja a sorokat
        /// </summary>
        partial void tablazatKialakitasaRaktarak(int id, string nev, string hely, string telefonszam, string email, SolidColorBrush hatterSzin)
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
            tableROW.harom.Content = hely;
            tableROW.negy.Content = telefonszam;
            tableROW.ot.Content = email;
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
            spRaktartTabla.Children.Add(gr0);
            //törlés módosítűás gomb eltüntetése
            btnTablaTorles.Visibility = Visibility.Collapsed;
            btnTablaModositas.Visibility = Visibility.Collapsed;
            ;
        }
    }
}
