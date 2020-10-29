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
using System.Globalization;

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Classes.Szallitolevelek> ListOfArukuldesek = new List<Classes.Szallitolevelek>();

        private void AtadasAdatTorlese(int index)
        {
            ListOfArukuldesek.RemoveAt(index);
            szinkronizalasArukuldes();
        }

        partial void ujAdatHozzaadasaAtadasokhoz(int id, DateTime datum, int arukiadoRaktar, int bevetelezoRaktar, int aruertek, string megjegyzes)
        {
            Classes.Szallitolevelek r = new Classes.Szallitolevelek(id, datum, arukiadoRaktar, bevetelezoRaktar, aruertek, megjegyzes);
            ListOfArukuldesek.Add(r);
            szinkronizalasArukuldes();
        }

        partial void BtnRaktarkoziAtadasraVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).ToolTip.ToString())
            {
                case "új áruküldés":
                    RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása

                    dprDatum.SelectedDate = DateTime.Now; //datum beállítása
                    txbTermekTallozo.Text = "Keresés..."; //törli a termék tallozo kereséi mezőértékét
                    spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear(); //törli a termék tallozo ered enyeit
                    spArukuldesTartalma.Children.Clear(); //törli a belső táblát
                    lbArukuldesTartalmaSorszam.Content =SzamFormazasaSorszam(ListOfArukuldesek.Count);
                    lbArukuldesTartalmaVegosszeg.Content = SzamFormazasaFt(0);
                    TermekTallozo.Visibility = Visibility.Collapsed;
                    cmbxArukiadoRaktarak.Items.Clear();
                    cmbxBevetelezoRaktarak.Items.Clear();
                    for (int i = 0; i < ListOfRaktarak.Count; i++)
                    {
                        cmbxArukiadoRaktarak.Items.Add(ListOfRaktarak[i].Nev);
                        cmbxBevetelezoRaktarak.Items.Add(ListOfRaktarak[i].Nev);
                    }
                    cmbxArukiadoRaktarak.SelectedIndex = 0; //arukiado raktar kezdő ertek beallitása
                    cmbxBevetelezoRaktarak.SelectedIndex = 0; //bevetelezo raktar kezdő ertek beallitása
                    ArukuldesTartalmaMentesModostasIcon.ToolTip = "Mentés";
                    tbxMegjegyzes.Text = String.Empty;
                    ListOfArukuldesTartalmaTemp = new List<Classes.ArukuldesTartalma>();
                    break;
                case "elem módosítása":
                    RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    int index = indexOfSelectedRowArukiadas();
                    dprDatum.SelectedDate = ListOfArukuldesek[index].Datum;
                    txbTermekTallozo.Text = "Keresés..."; //törli a termék tallozo kereséi mezőértékét
                    spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear(); //törli a termék tallozo ered enyeit
                    spArukuldesTartalma.Children.Clear(); //törli a belső táblát
                    lbArukuldesTartalmaSorszam.Content =SzamFormazasaSorszam(ListOfArukuldesek[index].ID);
                    TermekTallozo.Visibility = Visibility.Collapsed;
                    cmbxArukiadoRaktarak.SelectedIndex = ListOfArukuldesek[index].ArukiadoRaktar_Raktar_ID;
                    cmbxBevetelezoRaktarak.SelectedIndex = ListOfArukuldesek[index].BevetelezoRaktar_Raktar_ID;
                    ListOfArukuldesTartalmaTemp = new List<Classes.ArukuldesTartalma>();
                    ListOfArukuldesTartalmaTempFeltoltese(index);
                    szinkronizalasArukuldesTartalma();
                    ArukuldesTartalmaMentesModostasIcon.ToolTip = "Módosítás";
                    tbxMegjegyzes.Text = ListOfArukuldesek[index].Megjegyzes;
                    break;
                case "elem törlése":
                    ArukuldesElemTorlese.Visibility = Visibility.Visible; //az elem törlő ablak megjelenik
                    break;
                case "szinkronizálás":
                    szinkronizalasArukuldes();
                    break;
            }
        }

        partial void tablazatKialakitasaArukukldesek(int id, DateTime datum, int arukiadoRaktar_id, int bevetelezoRaktar_id, int aruertek, string megjegyzes, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TableRowWith5Column tableROW = new TableRowWith5Column();
            tableROW.egy.Content = id;
            tableROW.ketto.Content = datum.ToString("yyyy.MM.dd.");
            tableROW.harom.Content = ListOfRaktarak[arukiadoRaktar_id].Nev;
            tableROW.harom.HorizontalContentAlignment = HorizontalAlignment.Left;
            tableROW.negy.Content = ListOfRaktarak[bevetelezoRaktar_id].Nev;
            tableROW.negy.HorizontalContentAlignment = HorizontalAlignment.Left;
            tableROW.ot.Content = SzamFormazasaFt(aruertek);
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
            spArukuldesekTabla.Children.Add(gr0);
            //törlés módosítás gomb eltüntetése
            btnRaktarkoziAtadasModositas.Visibility = Visibility.Collapsed;
            btnArukuldesekTablaTorles.Visibility = Visibility.Collapsed;
        }

        partial void szinkronizalasArukuldes()
        {
            spArukuldesekTabla.Children.Clear();
            for (int i = 0; i < ListOfArukuldesek.Count; i++)
                tablazatKialakitasaArukukldesek(
                    ListOfArukuldesek[i].ID,
                    ListOfArukuldesek[i].Datum,
                    ListOfArukuldesek[i].ArukiadoRaktar_Raktar_ID,
                    ListOfArukuldesek[i].BevetelezoRaktar_Raktar_ID,
                    ListOfArukuldesek[i].Aruertek,
                    ListOfArukuldesek[i].Megjegyzes,
                    (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spArukuldesekTabla.Parent).ScrollToEnd();
        }

        private int indexOfSelectedRowArukiadas()
        {
            var background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF05B422"));
            for (int i = 0; i < spArukuldesekTabla.Children.Count; i++)
            {
                var background2 = ((Grid)((Grid)spArukuldesekTabla.Children[i]).Children[0]).Background;
                if (background2.ToString() == background.ToString())
                    return i;
            }
            return -1;
        }

        partial void BtnArukuldesTorlesEsemenyek(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowArukiadas();
            if (((Button)sender).ToolTip != null)
            {
                //Ablak bezarasa gomb eseménye
                ArukuldesElemTorlese.Visibility = Visibility.Collapsed;
            }
            else
            {
                switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
                {
                    case "Igen":
                        //meglévő arukuldes törlése "igen"
                        ujAdatHozzaadasaAktivitasok(index, 8);
                        ArukuldesElemTorlese.Visibility = Visibility.Collapsed;
                        AtadasAdatTorlese(index);
                        break;
                    default:
                        //meglévő arukuldes törlése "nem"
                        ArukuldesElemTorlese.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }
    }
}
