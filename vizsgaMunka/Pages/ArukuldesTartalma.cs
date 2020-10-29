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
using System.Security.Policy;

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Classes.Termek> ListOfTermekTallozo = new List<Classes.Termek>();
        List<Classes.ArukuldesTartalma> ListOfArukuldesTartalma = new List<Classes.ArukuldesTartalma>();
        List<Classes.ArukuldesTartalma> ListOfArukuldesTartalmaTemp = new List<Classes.ArukuldesTartalma>();


        partial void btnArukuldesTartalmaToolTip(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowArukuldesTartalma();
            switch (((Button)sender).ToolTip.ToString())
            {

                case "Termék hozzáadása":
                    //Terméktallozó megjelenítése
                    TermekTallozo.Visibility = Visibility.Visible;
                    break;
                case "Elem hozzáadáasa a táblázathoz":
                    //A kivalasztott termékhez megjeleníti a súly hozzáadása ablakot megjeleníti
                    ArukuldesTartalmaElemHozzaadasa.Visibility = Visibility.Visible;
                    tbxArukuldesTartalmaMennyiseg.Text = String.Empty;
                    txbArukuldesTartalmaTermeknev.Content = ((Label)((Grid)((Button)sender).Parent).Children[0]).Content;
                    txbArukuldesTartalmaTermeknev.Tag = ((Label)((Grid)((Button)sender).Parent).Children[0]).Tag;
                    tbxArukuldesTartalmaMennyiseg.Focus();
                    break;
                case "Elem módosítása":
                    //Elem módósító ablak megjelenítése
                    ArukuldesTartalmaElemModositasa.Visibility = Visibility.Visible;
                    txbArukuldestartalmaMegnevezes.Text = ListOfArukuldesTartalmaTemp[index].Nev;
                    txbArukuldestartalmaMennyiség.Text = ListOfArukuldesTartalmaTemp[index].Mennyiseg.ToString();
                    txbArukuldestartalmaMe.Text = ListOfArukuldesTartalmaTemp[index].MennyisegiEgyseg;
                    txbArukuldestartalmaEgysegar.Text = ListOfArukuldesTartalmaTemp[index].Egysegar.ToString();
                    txbArukuldestartalmaAfa.Text = ListOfArukuldesTartalmaTemp[index].AFA.ToString();
                    break;
                case "Elem törlése":
                    //Elem törlő ablak megjelenítése
                    ArukuldesTartalmaElemTorlese.Visibility = Visibility.Visible;
                    break;
                case "Nyomtatás":
                    //az átadást mentse
                    //mutassa meg a nyomtatási nézetet
                    //nyomtatas utan zarja be az ablakot
                    
                case "Mentés":
                    //menti az átadást
                    ujAdatHozzaadasaAktivitasok((ListOfArukuldesek.Count == 0) ? 0 : ListOfArukuldesek[ListOfArukuldesek.Count - 1].ID + 1, 6);
                    AtadasRogziteseUjAtadas();
                    break;
                case "Módosítás":
                    //menti az átadást
                    ujAdatHozzaadasaAktivitasok(indexOfSelectedRowArukiadas(), 7);
                    AtadasRogziteseModositas();
                    break;
                case "Bezár":
                    //termektallozó eltüntetése
                    
                    TermekTallozo.Visibility = Visibility.Collapsed;
                    break;
                case "Rögzítés":
                    //A hozzáadás kisablak gombja: rögzíti a tételt
                    int index2 = int.Parse(txbArukuldesTartalmaTermeknev.Tag.ToString());
                    ujAdatHozzaadasaArukuldesTartalma(
                        (ListOfArukuldesTartalmaTemp.Count==0)?0: ListOfArukuldesTartalmaTemp[ListOfArukuldesTartalmaTemp.Count-1].ID+1,
                        ListOfTermekTallozo[index2].Nev,
                       Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',', '.')),
                        ListOfTermekTallozo[index2].MennyisegiEgyseg,
                        int.Parse(ListOfTermekTallozo[index2].Egysegar.ToString()),
                        int.Parse(ListOfTermekTallozo[index2].AFA.ToString()),
                        Convert.ToInt32(Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',', '.')) * Convert.ToDouble(ListOfTermekTallozo[index2].Egysegar))
                        );
                    ArukuldesTartalmaElemHozzaadasa.Visibility = Visibility.Collapsed;
                    break;
                case "Vissza":
                    //Az elem törlő és módosító, hozzáadó ablak eltünteése
                    ArukuldesTartalmaElemTorlese.Visibility = Visibility.Collapsed;
                    ArukuldesTartalmaElemHozzaadasa.Visibility = Visibility.Collapsed;
                    ArukuldesTartalmaElemModositasa.Visibility = Visibility.Collapsed;

                    break;
            }
        }

        private void AtadasRogziteseModositas()
        {
            int index = indexOfSelectedRowArukiadas();
            RaktarkoziAtadasAblak.Visibility = Visibility.Collapsed;
            ListOfArukuldesek[index].Megjegyzes = tbxMegjegyzes.Text;
            ListOfArukuldesek[index].ArukiadoRaktar_Raktar_ID = cmbxArukiadoRaktarak.SelectedIndex;
            ListOfArukuldesek[index].BevetelezoRaktar_Raktar_ID = cmbxBevetelezoRaktarak.SelectedIndex;
            ListOfArukuldesek[index].Datum = dprDatum.SelectedDate.Value;
            ListOfArukuldesek[index].Aruertek = Convert.ToInt32(lbArukuldesTartalmaVegosszeg.Content.ToString().Trim("Ft".ToCharArray()).Replace(" ", ""));
            ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalmaMODOSITASKOR();
            szinkronizalasArukuldes();
        }

        private void ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalmaMODOSITASKOR()
        {
            for (int i = 0; i < ListOfArukuldesTartalma.Count; i++)
            {
                if (ListOfArukuldesTartalma[i].Szallitolevel_ID == Convert.ToInt32(lbArukuldesTartalmaSorszam.Content.ToString().Replace("#", "")))
                {
                    ListOfArukuldesTartalma.RemoveAt(i);
                }
            }
            ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalma();
        }

        private void AtadasRogziteseUjAtadas()
        {
            RaktarkoziAtadasAblak.Visibility = Visibility.Collapsed;
            ListOfArukuldesek.Add(new Classes.Szallitolevelek(
                (ListOfArukuldesek.Count == 0) ? 0 : ListOfArukuldesek[ListOfArukuldesek.Count - 1].ID + 1,
                dprDatum.SelectedDate.Value,
                cmbxArukiadoRaktarak.SelectedIndex,
                cmbxBevetelezoRaktarak.SelectedIndex,
                Convert.ToInt32(lbArukuldesTartalmaVegosszeg.Content.ToString().Trim("Ft".ToCharArray()).Replace(" ", "")),
                tbxMegjegyzes.Text
                ));
            ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalma();
            szinkronizalasArukuldes();
        }

        private void ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalma()
        {
            for (int i = 0; i < ListOfArukuldesTartalmaTemp.Count; i++)
            {
                ListOfArukuldesTartalma.Add(ListOfArukuldesTartalmaTemp[i]);
            }
        }

        private void ListOfArukuldesTartalmaTempFeltoltese(int ID)
        {
            
            for (int i = 0; i < ListOfArukuldesTartalma.Count; i++)
            {
                if (ID==ListOfArukuldesTartalma[i].Szallitolevel_ID)
                {
                    ListOfArukuldesTartalmaTemp.Add(ListOfArukuldesTartalma[i]);
                }  
            }
        }

        partial void ujAdatHozzaadasaArukuldesTartalma(int index, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr)
        {
            Classes.ArukuldesTartalma a = new Classes.ArukuldesTartalma(index, Convert.ToInt32(lbArukuldesTartalmaSorszam.Content.ToString().Replace("#", "")), nev, mennyiseg, mennyisegiEgyseg, egysegar, aFA, bruttoAr);
            ListOfArukuldesTartalmaTemp.Add(a);
            szinkronizalasArukuldesTartalma();
        }

        partial void szinkronizalasArukuldesTartalma()
        {
            int vegosszeg = 0;
            spArukuldesTartalma.Children.Clear();
            //módosíákor hasznos amikor amugy is lenne pár elem a listába
            for (int i = 0; i < ListOfArukuldesTartalmaTemp.Count; i++)
            {
                tablazatKialakitasaArukuldesTartalma(
                        ListOfArukuldesTartalmaTemp[i].ID,
                        ListOfArukuldesTartalmaTemp[i].Nev,
                        ListOfArukuldesTartalmaTemp[i].Mennyiseg,
                        ListOfArukuldesTartalmaTemp[i].MennyisegiEgyseg,
                        ListOfArukuldesTartalmaTemp[i].Egysegar,
                        ListOfArukuldesTartalmaTemp[i].AFA,
                        ListOfArukuldesTartalmaTemp[i].BruttoAr,
                        (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                vegosszeg += ListOfArukuldesTartalmaTemp[i].BruttoAr;
            }
           
            ((ScrollViewer)spRaktartTabla.Parent).ScrollToEnd();
            lbArukuldesTartalmaVegosszeg.Content = SzamFormazasaFt(vegosszeg);
        }

        partial void tablazatKialakitasaArukuldesTartalma(int iD, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TableRowWith7Column tableROW = new TableRowWith7Column();
            tableROW.egy.Content = iD;
            tableROW.ketto.Content = nev;
            tableROW.ketto.HorizontalContentAlignment = HorizontalAlignment.Left;

            tableROW.harom.Content = mennyiseg;
            tableROW.harom.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.harom.Padding = new Thickness(10, 0, 10, 0);

            tableROW.negy.Content = mennyisegiEgyseg;
            tableROW.negy.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.negy.Padding = new Thickness(15, 0, 15, 0);

            tableROW.ot.Content = SzamFormazasaFt(egysegar);
            tableROW.ot.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.ot.Padding = new Thickness(10, 0, 10, 0);

            tableROW.hat.Content = SzamFormazasaAFA(aFA);
            tableROW.hat.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.hat.Padding = new Thickness(10, 0, 10, 0);

            tableROW.het.Content = SzamFormazasaFt(bruttoAr);
            tableROW.het.HorizontalContentAlignment = HorizontalAlignment.Right;
            tableROW.het.Padding = new Thickness(15, 0, 15, 0);

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
            spArukuldesTartalma.Children.Add(gr0);
            //törlés módosítűás gomb eltüntetése
            btnTablaTorles.Visibility = Visibility.Collapsed;
            btnArukuldesTartalmaTorles.Visibility = Visibility.Collapsed;
        }

        partial void btnArukuldesTartalmaContent(object sender, RoutedEventArgs e)
        {

            int index = indexOfSelectedRowArukuldesTartalma();
            switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
            {
                case "Vissza":
                    //bezarja a programrészt
                    RaktarkoziAtadasAblak.Visibility = Visibility.Collapsed;
                    break;
                case "Módosítás":
                    ListOfArukuldesTartalmaTemp[index].Mennyiseg = double.Parse(txbArukuldestartalmaMennyiség.Text);
                    ListOfArukuldesTartalmaTemp[index].Egysegar = int.Parse(txbArukuldestartalmaEgysegar.Text);
                    ListOfArukuldesTartalmaTemp[index].BruttoAr = int.Parse($"{ double.Parse(txbArukuldestartalmaMennyiség.Text) * int.Parse(txbArukuldestartalmaEgysegar.Text)}");
                    ArukuldesTartalmaElemModositasa.Visibility = Visibility.Collapsed;
                    szinkronizalasArukuldesTartalma();
                    break;
                case "Igen":
                    //Akk törlöl egy kijelölt elemet a táblázatból
                    ListOfArukuldesTartalmaTemp.RemoveAt(index);
                    ArukuldesTartalmaElemTorlese.Visibility = Visibility.Collapsed;
                    szinkronizalasArukuldesTartalma();
                    break;
                case "Nem":
                    //eltünteti a kisablakot
                    ArukuldesTartalmaElemTorlese.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private int indexOfSelectedRowArukuldesTartalma()
        {
            var background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF05B422"));
            for (int i = 0; i < spArukuldesTartalma.Children.Count; i++)
            {
                var background2 = ((Grid)((Grid)spArukuldesTartalma.Children[i]).Children[0]).Background;
                if (background2.ToString() == background.ToString())
                    return i;
            }
            return -1;
        }

        private string SzamFormazasaFt(int Szam)
        {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return Szam.ToString("#,0 Ft", nfi);
        }

        private string SzamFormazasaAFA(int Szam)
        {
            return $"{Szam} %";
        }
        private string SzamFormazasaSorszam(int Szam)
        {
            return $"#{Szam}";
        }

        partial void txbTermekTallozoTextChanged(object sender, TextChangedEventArgs e)
        {
            spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear();
            ListOfTermekTallozo = new List<Classes.Termek>();
            if (txbTermekTallozo.Text != "Keresés..." && !string.IsNullOrWhiteSpace(txbTermekTallozo.Text))
            {
                for (int i = 0; i < ListOfTermekek.Count; i++)
                {
                    if (ListOfTermekek[i].Nev.ToUpper().StartsWith(txbTermekTallozo.Text.ToUpper()))
                    {
                        ListOfTermekTallozo.Add(new Classes.Termek(
                            ListOfTermekek[i].ID,
                            ListOfTermekek[i].Nev,
                            ListOfTermekek[i].AFA,
                            ListOfTermekek[i].MennyisegiEgyseg,
                            ListOfTermekek[i].Egysegar));
                    }
                }
                TermekTallozoFeltolteseSzurtListaval(ListOfTermekTallozo);
            }

        }

        partial void TermekTallozoFeltolteseSzurtListaval(List<Classes.Termek> szurtLista)
        {
            //létre hozni a listában elem sablont és feltolteni a szurtlista elemeivel

            for (int i = 0; i < szurtLista.Count; i++)
            {
                Grid gr1 = new Grid();
                gr1.Margin = new Thickness(5);
                gr1.Height = 30;
                gr1.Background = Brushes.White;

                Label lb = new Label();
                lb.HorizontalAlignment = HorizontalAlignment.Left;
                lb.Content = szurtLista[i].Nev;
                lb.Tag = i;
                lb.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#707070"));
                lb.FontFamily = new FontFamily("Segoe UI");
                lb.FontSize = 12;
                lb.VerticalContentAlignment = VerticalAlignment.Center;

                Button btn = new Button();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Width = 28;
                btn.Height = 28;
                btn.HorizontalAlignment = HorizontalAlignment.Right;
                btn.Padding = new Thickness(-1);
                btn.BorderThickness = new Thickness(0);
                btn.Cursor = Cursors.Hand;
                btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                btn.Margin = new Thickness(1, 0, 1, 0);
                btn.ToolTip = "Elem hozzáadáasa a táblázathoz";
                btn.Click += btnArukuldesTartalmaToolTip;

                Grid gr2 = new Grid();
                gr2.Width = 30;
                gr2.Height = 30;
                gr2.Background = Brushes.White;
                gr2.Children.Add(new Add());

                btn.Content = gr2;
                gr1.Children.Add(lb);
                gr1.Children.Add(btn);

                spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Add(gr1);
                ;
            }
        }

        partial void RemoveText(object sender, RoutedEventArgs e)
        {
            if (txbTermekTallozo.Text == "Keresés...")
            {
                txbTermekTallozo.Text = "";
            }
        }

        partial void AddText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbTermekTallozo.Text))
                txbTermekTallozo.Text = "Keresés...";
        }

    }
}
