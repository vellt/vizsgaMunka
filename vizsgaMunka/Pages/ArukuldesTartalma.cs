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
using vizsgaMunka.Classes;

namespace vizsgaMunka
{
    /// <summary>
    /// Pages of Arukuldes Tartalma
    /// </summary>
    public enum ArukuldesTartalmaEnum: byte
    {
        /// <summary>
        /// Főképernyő
        /// </summary>
        Main=0,
        /// <summary>
        /// Elem Törlése kisablak
        /// </summary>
        Torles=1,
        /// <summary>
        /// Elem Hozzáadása klsablak
        /// </summary>
        Hozzadas=2,
        /// <summary>
        /// Elem Módosítása kisablak
        /// </summary>
        Modositas=3,
    }
    public partial class MainWindow : Window
    {
        List<Termek> ListOfTermekTallozo = new List<Termek>();
        List<ArukuldesTartalma> ListOfArukuldesTartalma = new List<ArukuldesTartalma>();
        List<ArukuldesTartalma> ListOfArukuldesTartalmaTemp = new List<ArukuldesTartalma>();

        /// <summary>
        /// Áruküldés Tartalma Dispatcher - szétosztja az eseményket, a bejövő esemenykódok alapján /ToolTip-Content/
        /// </summary>
        private void ArukuldesTartalmaBtn(object sender, MouseButtonEventArgs e)
        {
            switch (new Seged().getEsemenyKod(sender))
            {
                case "Vissza":
                    //tooltip
                    arukuldesTartalmaHidden(new Seged().getIndexFromTag(((Grid)sender).Tag));
                    break;
                case "Termék tallózó bezárása":
                    arukuldesTartalmaTermekTallozoBezar();
                    break;
                case "Elem hozzáadáasa a táblázathoz":
                    arukuldesTartalmaElemHozzaadasaShow(
                        ((Label)((Grid)((Grid)sender).Parent).Children[0]).Content, 
                        ((Label)((Grid)((Grid)sender).Parent).Children[0]).Tag);
                    break;
                case "Termék tallózó megjelenítése":
                    arukuldesTartalmaTermekTallozoShow();
                    break;
                case "Elem módosítása":
                    arukuldesTartalmaElemModositasaShow();
                    break;
                case "Elem törlése":
                    arukuldesTartalmaElemTorleseShow();
                    break;
                case "Nyomtatás":
                    arukuldesTartalmaNyomtatasShow();
                    break;
                case "Átadás Mentése":
                    arukuldesTartalmaAtadasMentes();
                    break;
                case "Átadás Módosítása":
                    arukuldesTartalmaAtadasMódosítása();
                    break;
                case "Igen":
                    arukuldesTartalmaIgen();
                    break;
                case "Nem":
                    arukuldesTartalmaNem();
                    break;
                case "Rögzítés":
                    arukuldesTartalmaRogzites();
                    break;
                case "Módosítás":
                    arukuldesTartalmaModositas();
                    break;
            }
        }

        private void arukuldesTartalmaAtadasMódosítása()
        {
            int index = new Seged().indexOfSelectedRow(spArukuldesekTabla);
            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Main);
            ListOfArukuldesek[index].Megjegyzes = tbxMegjegyzes.Text;
            ListOfArukuldesek[index].ArukiadoRaktar_Raktar_ID = cmbxArukiadoRaktarak.SelectedIndex;
            ListOfArukuldesek[index].BevetelezoRaktar_Raktar_ID = cmbxBevetelezoRaktarak.SelectedIndex;
            ListOfArukuldesek[index].Datum = dprDatum.SelectedDate.Value;
            ListOfArukuldesek[index].Aruertek = new Seged().removeFt(lbArukuldesTartalmaVegosszeg.Content);

            for (int i = 0; i < ListOfArukuldesTartalma.Count; i++)
            {
                if (ListOfArukuldesTartalma[i].Szallitolevel_ID == Convert.ToInt32(lbArukuldesTartalmaSorszam.Content.ToString().Replace("#", "")))
                {
                    ListOfArukuldesTartalma.RemoveAt(i);
                }
            }
            ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalma();
            arukuldesSzinkronizalas();
        }

        private void arukuldesTartalmaModositas()
        {
            int index = new Seged().indexOfSelectedRow(spArukuldesTartalma);
            ListOfArukuldesTartalmaTemp[index].Mennyiseg = double.Parse(atadasTartalmaSS3Mennyiség.Text);
            ListOfArukuldesTartalmaTemp[index].Egysegar = int.Parse(atadasTartalmaSS3Egysegar.Text);
            ListOfArukuldesTartalmaTemp[index].BruttoAr = (int)(ListOfArukuldesTartalmaTemp[index].Mennyiseg * ListOfArukuldesTartalmaTemp[index].Egysegar);
            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Modositas);
            arukuldesTartalmaSzinkronizalas();
        }

        private void arukuldesTartalmaRogzites()
        {
            //A hozzáadás kisablak gombja: rögzíti a tételt
            int index = int.Parse(txbArukuldesTartalmaTermeknev.Tag.ToString());
           
            ListOfArukuldesTartalmaTemp.Add(
                new ArukuldesTartalma(
                    id:(ListOfArukuldesTartalmaTemp.Count == 0) ? 0 : ListOfArukuldesTartalmaTemp[ListOfArukuldesTartalmaTemp.Count - 1].ID + 1,
                    szallitolevel_ID: Convert.ToInt32(lbArukuldesTartalmaSorszam.Content.ToString().Replace("#", "")),
                    nev: ListOfTermekTallozo[index].Nev,
                    mennyiseg: Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',', '.')),
                    mennyisegiEgyseg: ListOfTermekTallozo[index].MennyisegiEgyseg,
                    egysegar: int.Parse(ListOfTermekTallozo[index].Egysegar.ToString()),
                    afa: int.Parse(ListOfTermekTallozo[index].AFA.ToString()),
                    bruttoAr: Convert.ToInt32(Convert.ToDouble(tbxArukuldesTartalmaMennyiseg.Text.Replace(',', '.')) * Convert.ToDouble(ListOfTermekTallozo[index].Egysegar))));
            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Hozzadas);
        }

        private void arukuldesTartalmaNem()
        {
            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Torles);
        }

        private void arukuldesTartalmaIgen()
        {
            //Akk törlöl egy kijelölt elemet a táblázatból
            int index = new Seged().indexOfSelectedRow(spArukuldesTartalma);
            ListOfArukuldesTartalmaTemp.RemoveAt(index);
            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Torles);
            arukuldesTartalmaSzinkronizalas();
        }

        private void arukuldesTartalmaAtadasMentes()
        {
            int index = (ListOfArukuldesek.Count == 0) ? 0 : ListOfArukuldesek[ListOfArukuldesek.Count - 1].ID + 1;
            //reportot küldünk az esemenyről
            ujAdatHozzaadasaAktivitasok(index, Esemeny.AtadasCreated);

            arukuldesTartalmaHidden(ArukuldesTartalmaEnum.Main);
            ListOfArukuldesek.Add(new Szallitolevelek(
                id: (ListOfArukuldesek.Count == 0) ? 0 : ListOfArukuldesek[ListOfArukuldesek.Count - 1].ID + 1,
                datum: dprDatum.SelectedDate.Value,
                arukiadoRaktar_raktar_id: cmbxArukiadoRaktarak.SelectedIndex,
                bevetelezoRaktar_raktar_id: cmbxBevetelezoRaktarak.SelectedIndex,
                aruertek: Convert.ToInt32(lbArukuldesTartalmaVegosszeg.Content.ToString().Trim("Ft".ToCharArray()).Replace(" ", "")),
                megjegyzes: tbxMegjegyzes.Text));

            ListOfArukuldesTartalmaTempHozzaadasListOfArukuldesTartalma();
            arukuldesSzinkronizalas();
        }

        private void arukuldesTartalmaNyomtatasShow()
        {
            //még nincs kész
        }

        private void arukuldesTartalmaElemTorleseShow()
        {
            atadasTartalmaSS1.Visibility = Visibility.Visible;
        }

        private void arukuldesTartalmaElemModositasaShow()
        {
            int index = new Seged().indexOfSelectedRow(spArukuldesTartalma);
            atadasTartalmaSS3.Visibility = Visibility.Visible;
            atadasTartalmaSS3Megnevezes.Text = ListOfArukuldesTartalmaTemp[index].Nev;
            atadasTartalmaSS3Mennyiség.Text = ListOfArukuldesTartalmaTemp[index].Mennyiseg.ToString();
            atadasTartalmaSS3Me.Text = ListOfArukuldesTartalmaTemp[index].MennyisegiEgyseg;
            atadasTartalmaSS3Egysegar.Text = ListOfArukuldesTartalmaTemp[index].Egysegar.ToString();
            atadasTartalmaSS3Afa.Text = ListOfArukuldesTartalmaTemp[index].AFA.ToString();
        }

        private void arukuldesTartalmaTermekTallozoShow()
        {
            TermekTallozo.Visibility = Visibility.Visible;
        }

        private void arukuldesTartalmaElemHozzaadasaShow(object termekNev, object index)
        {
            atadasTartalmaSS2.Visibility = Visibility.Visible;
            tbxArukuldesTartalmaMennyiseg.Text = String.Empty;
            txbArukuldesTartalmaTermeknev.Content = termekNev;
            txbArukuldesTartalmaTermeknev.Tag = index;
            tbxArukuldesTartalmaMennyiseg.Focus();
        }

        private void arukuldesTartalmaTermekTallozoBezar()
        {
            TermekTallozo.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// "bezárja az abalakot a Tag-Code alapján"
        /// </summary>
        /// <param name="tag">Add meg a "gomb:grid" Tag-Code-ját</param>
        private void arukuldesTartalmaHidden(ArukuldesTartalmaEnum Pages)
        {
            //szűrés
            switch (Pages)
            {
                case ArukuldesTartalmaEnum.Main:
                    //MainScreen-ből lépjen ki: váltson az arukluldes lapra
                    tbcrlTartalom.SelectedIndex = 4;
                    atadasTartalma.Visibility = Visibility.Collapsed;
                    break;
                case ArukuldesTartalmaEnum.Torles:
                    //SubScrren1-ből lépjen ki: "Elem törlése kisablak"
                    atadasTartalmaSS1.Visibility = Visibility.Collapsed;
                    break;
                case ArukuldesTartalmaEnum.Hozzadas:
                    //SubScrren2-ből lépjen ki: "Elem hozzáadaása kisablak"
                    atadasTartalmaSS2.Visibility = Visibility.Collapsed;
                    break;
                case ArukuldesTartalmaEnum.Modositas:
                    //SubScrren3-ból lépjen ki: "Elem módosítása kisablak"
                    atadasTartalmaSS3.Visibility = Visibility.Collapsed;
                    break;
            }
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

        private void arukuldesTartalmaSzinkronizalas()
        {
            int vegosszeg = 0;
            spArukuldesTartalma.Children.Clear();
            //módosíákor hasznos amikor amugy is lenne pár elem a listába
            for (int i = 0; i < ListOfArukuldesTartalmaTemp.Count; i++)
            {
                vegosszeg += ListOfArukuldesTartalmaTemp[i].BruttoAr;
                TableRow7Column row = new TableRow7Column(
                    id: ListOfArukuldesTartalmaTemp[i].ID.ToString(),
                    egy: ListOfArukuldesTartalmaTemp[i].Nev,
                    ketto: ListOfArukuldesTartalmaTemp[i].Mennyiseg.ToString(),
                    harom: ListOfArukuldesTartalmaTemp[i].MennyisegiEgyseg,
                    negy: new Seged().ToHUF(ListOfArukuldesTartalmaTemp[i].Egysegar),
                    ot: new Seged().ToAFA(ListOfArukuldesTartalmaTemp[i].AFA),
                    hat: new Seged().ToHUF(ListOfArukuldesTartalmaTemp[i].BruttoAr),
                    hatterSzin: (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                row.egy.HorizontalAlignment = HorizontalAlignment.Left;
                row.ketto.HorizontalAlignment = HorizontalAlignment.Right;
                row.ketto.Padding= new Thickness(10, 0, 10, 0);
                row.harom.HorizontalAlignment = HorizontalAlignment.Right;
                row.harom.Padding = new Thickness(15, 0, 15, 0);
                row.negy.HorizontalAlignment = HorizontalAlignment.Right;
                row.negy.Padding = new Thickness(10, 0, 10, 0);
                row.ot.HorizontalAlignment = HorizontalAlignment.Right;
                row.ot.Padding = new Thickness(10, 0, 10, 0);
                row.hat.HorizontalAlignment = HorizontalAlignment.Right;
                row.hat.Padding = new Thickness(15, 0, 15, 0);
                row.PreviewMouseUp += ArukuldesTartalmaBtn;
                spArukuldesTartalma.Children.Add(row);
            }
            ((ScrollViewer)spArukuldesTartalma.Parent).ScrollToEnd();
            lbArukuldesTartalmaVegosszeg.Content = new Seged().ToHUF(vegosszeg);
            spArukuldesTartalma = new Seged().GombokLathatosaga(spArukuldesTartalma, false);
        }

        private void txbTermekTallozoTextChanged(object sender, TextChangedEventArgs e)
        {
            spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear();
            ListOfTermekTallozo = new List<Termek>();
            if (txbTermekTallozo.Text != "Keresés..." && !string.IsNullOrWhiteSpace(txbTermekTallozo.Text))
            {
                for (int i = 0; i < ListOfTermekek.Count; i++)
                {
                    if (ListOfTermekek[i].Nev.ToUpper().StartsWith(txbTermekTallozo.Text.ToUpper()))
                    {
                        ListOfTermekTallozo.Add(new Termek(
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

        private void TermekTallozoFeltolteseSzurtListaval(List<Termek> szurtLista)
        {
            //létre hozni a listában elem sablont és feltolteni a szurtlista elemeivel

            for (int i = 0; i < szurtLista.Count; i++)
            {
                Grid gr1 = new Grid();
                gr1.Margin = new Thickness(5);
                gr1.Height = 30;
                gr1.Background = Brushes.White;

                ContentControl lb = new Label();
                lb.HorizontalAlignment = HorizontalAlignment.Left;
                lb.Content = szurtLista[i].Nev;
                lb.Tag = i;
                lb.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#707070"));
                lb.FontFamily = new FontFamily("Segoe UI");
                lb.FontSize = 12;
                lb.VerticalContentAlignment = VerticalAlignment.Center;

                Grid btn = new Grid();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Width = 30;
                btn.Height = 30;
                btn.HorizontalAlignment = HorizontalAlignment.Right;
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Thickness(1, 0, 1, 0);
                btn.ToolTip = "Elem hozzáadáasa a táblázathoz";
                Add icon = new Add();
                icon.Fill = (Brush)(new BrushConverter().ConvertFrom("#707070"));
                btn.Children.Add(icon);
                btn.Background = Brushes.White;
                btn.PreviewMouseUp += ArukuldesTartalmaBtn;
                
                gr1.Children.Add(lb);
                gr1.Children.Add(btn);

                spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Add(gr1);
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (txbTermekTallozo.Text == "Keresés...") txbTermekTallozo.Text = "";
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbTermekTallozo.Text)) txbTermekTallozo.Text = "Keresés...";
        }
    }
}
