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
        List<Termek> szurtLista = new List<Termek>();
        List<ArukuldesTartalma> ListOfArukuldesTartalma = new List<ArukuldesTartalma>();

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
                    txbArukuldesTartalmaTermeknev.Tag= ((Label)((Grid)((Button)sender).Parent).Children[0]).Tag;
                    tbxArukuldesTartalmaMennyiseg.Focus();
                    break;
                case "Elem módosítása":
                    //Elem módósító ablak megjelenítése
                    ArukuldesTartalmaElemModositasa.Visibility = Visibility.Visible;
                    txbArukuldestartalmaMegnevezes.Text = ListOfArukuldesTartalma[index].Nev;
                    txbArukuldestartalmaMennyiség.Text = ListOfArukuldesTartalma[index].Mennyiseg.ToString();
                    txbArukuldestartalmaMe.Text = ListOfArukuldesTartalma[index].MennyisegiEgyseg;
                    txbArukuldestartalmaEgysegar.Text = ListOfArukuldesTartalma[index].Egysegar.ToString();
                    txbArukuldestartalmaAfa.Text = ListOfArukuldesTartalma[index].AFA.ToString();
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
                    break;
                case "Bezár":
                    //termektallozó eltüntetése
                    TermekTallozo.Visibility = Visibility.Collapsed;
                    break;
                case "Rögzítés":
                    //A hozzáadás kisablak gombja: rögzíti a tételt
                    int index2 = int.Parse(txbArukuldesTartalmaTermeknev.Tag.ToString());
                    ujAdatHozzaadasaArukuldesTartalma(
                        spArukuldesTartalma.Children.Count,
                        szurtLista[index2].Nev,
                       Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',', '.')),
                        szurtLista[index2].MennyisegiEgyseg,
                        int.Parse(szurtLista[index2].Egysegar.ToString()),
                        int.Parse(szurtLista[index2].AFA.ToString()),
                        Convert.ToInt32(Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',','.')) * Convert.ToDouble(szurtLista[index2].Egysegar))
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

        private void ujAdatHozzaadasaArukuldesTartalma(int index, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr)
        {
            ArukuldesTartalma a = new ArukuldesTartalma(index, 0, nev, mennyiseg, mennyisegiEgyseg, egysegar, aFA, bruttoAr);
            ListOfArukuldesTartalma.Add(a);
            szinkronizalasArukuldesTartalma();
        }

        private void szinkronizalasArukuldesTartalma()
        {
            int vegosszeg = 0;
            spArukuldesTartalma.Children.Clear();
            for (int i = 0; i < ListOfArukuldesTartalma.Count; i++)
            {
                tablazatKialakitasaArukuldesTartalma(
                    ListOfArukuldesTartalma[i].ID, 
                    ListOfArukuldesTartalma[i].Nev, 
                    ListOfArukuldesTartalma[i].Mennyiseg, 
                    ListOfArukuldesTartalma[i].MennyisegiEgyseg, 
                    ListOfArukuldesTartalma[i].Egysegar, 
                    ListOfArukuldesTartalma[i].AFA, 
                    ListOfArukuldesTartalma[i].BruttoAr,
                    (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                vegosszeg += ListOfArukuldesTartalma[i].BruttoAr;
            }
            ((ScrollViewer)spRaktartTabla.Parent).ScrollToEnd();
            lbArukuldesTartalmaVegosszeg.Content = SzamFormazasaFt(vegosszeg);
        }

        private void tablazatKialakitasaArukuldesTartalma(int iD, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr, SolidColorBrush hatterSzin)
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
                    ListOfArukuldesTartalma[index].Mennyiseg = double.Parse(txbArukuldestartalmaMennyiség.Text);
                    ListOfArukuldesTartalma[index].Egysegar = int.Parse(txbArukuldestartalmaEgysegar.Text);
                    ListOfArukuldesTartalma[index].BruttoAr =int.Parse($"{ double.Parse(txbArukuldestartalmaMennyiség.Text) * int.Parse(txbArukuldestartalmaEgysegar.Text)}");
                    ArukuldesTartalmaElemModositasa.Visibility = Visibility.Collapsed;
                    szinkronizalasArukuldesTartalma();
                    break;
                case "Igen":
                    //Akk törlöl egy kijelölt elemet a táblázatból
                    ListOfArukuldesTartalma.RemoveAt(index);
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
    }
}
