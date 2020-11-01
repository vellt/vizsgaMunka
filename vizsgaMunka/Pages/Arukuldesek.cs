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
using vizsgaMunka.Classes;

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Szallitolevelek> ListOfArukuldesek = new List<Szallitolevelek>();

        /// <summary>
        /// Áruküldés Dispatcher - szétosztja az eseményket, a bejövő esemenykódok alapján /ToolTip-Content/
        /// </summary>
        private void ArukuldesekBtn(object sender, MouseButtonEventArgs e)
        {
            switch ((((Grid)sender).ToolTip == null) ? ((ContentControl)((Grid)sender).Children[0]).Content.ToString() : ((Grid)sender).ToolTip.ToString())
            {
                case "Új áruküldés":
                    //tooltip
                    arukuldesHozzaadasaShow();
                    break;
                case "Áruküldés módosítása":
                    //tooltip
                    arukuldesModositasaShow();
                    break;
                case "Áruküldés törlése":
                    //tooltip
                    arukuldesTorleseShow();
                    break;
                case "Szinkronizálás":
                    //tooltip
                    arukuldesSzinkronizalas();
                    break;
                case "Igen":
                    //content
                    arukuldesIgen();
                    break;
                case "Nem":
                    //content
                    arukuldesNem();
                    break;
            }
        }

        /// <summary>
        /// A törlés ablakot "bezárja"
        /// </summary>
        private void arukuldesNem()
        {
            ArukuldesSS2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Törli a kijelölt elemet a táblázatból
        /// </summary>
        private void arukuldesIgen()
        {
            int index = new Seged().indexOfSelectedRow(spArukuldesekTabla);
            //reportot küldünk az esemenyről
            ujAdatHozzaadasaAktivitasok(index, 8);
            //töröljük a listából a kijelölt elemet majd szinkronizáljuk a táblát a listával és bezárjuk az ablakot
            arukuldesNem();
            ListOfArukuldesek.RemoveAt(index);
            arukuldesSzinkronizalas();
        }

        /// <summary>
        /// Szinkronizálja a táblázatot
        /// </summary>
        private void arukuldesSzinkronizalas()
        {
            spArukuldesekTabla.Children.Clear();
            for (int i = 0; i < ListOfArukuldesek.Count; i++)
            {
                TableRow5Column row = new TableRow5Column(
                    id: ListOfArukuldesek[i].ID.ToString(),
                    egy: ListOfArukuldesek[i].Datum.ToString("yyyy.MM.dd."),
                    ketto: ListOfRaktarak[ListOfArukuldesek[i].ArukiadoRaktar_Raktar_ID].Nev,
                    harom: ListOfRaktarak[ListOfArukuldesek[i].BevetelezoRaktar_Raktar_ID].Nev,
                    negy: new Seged().ToHUF(ListOfArukuldesek[i].Aruertek),
                    hatterSzin: (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                row.egy.Margin = new Thickness(5, 0, 0, 0);
                row.ketto.HorizontalAlignment= HorizontalAlignment.Left;
                row.ketto.Margin= new Thickness(5, 0, 0, 0);
                row.harom.HorizontalAlignment= HorizontalAlignment.Left;
                row.harom.Margin = new Thickness(5, 0, 0, 0);
                row.negy.Margin = new Thickness(0, 0, 45, 0);
                row.negy.HorizontalAlignment = HorizontalAlignment.Right;
                row.MouseDown += arukuldesSorKattintasEsemeny;
                spArukuldesekTabla.Children.Add(row);
            }
            ((ScrollViewer)spArukuldesekTabla.Parent).ScrollToEnd();

            //törlés módosítás gomb eltüntetése
            spArukuldesButtons = new Seged().GombokLathatosaga(spArukuldesButtons, false);

        }

        /// <summary>
        /// Megjeleníti az Áruküldés Törlés ablakot
        /// </summary>
        private void arukuldesTorleseShow()
        {
            ArukuldesSS2.Visibility = Visibility.Visible; //az elem törlő ablak megjelenik
        }

        /// <summary>
        /// Megjeleníti az ÁruküldésTartalma ablakot
        /// </summary>
        private void arukuldesModositasaShow()
        {
            RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
            //kezdő értékek beállítása
            int index = new Seged().indexOfSelectedRow(spArukuldesekTabla);
            dprDatum.SelectedDate = ListOfArukuldesek[index].Datum;
            txbTermekTallozo.Text = "Keresés..."; //törli a termék tallozo kereséi mezőértékét
            spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear(); //törli a termék tallozo ered enyeit
            spArukuldesTartalma.Children.Clear(); //törli a belső táblát
            lbArukuldesTartalmaSorszam.Content = new Seged().ToSorszam(ListOfArukuldesek[index].ID);
            TermekTallozo.Visibility = Visibility.Collapsed;
            cmbxArukiadoRaktarak.SelectedIndex = ListOfArukuldesek[index].ArukiadoRaktar_Raktar_ID;
            cmbxBevetelezoRaktarak.SelectedIndex = ListOfArukuldesek[index].BevetelezoRaktar_Raktar_ID;
            ListOfArukuldesTartalmaTemp = new List<ArukuldesTartalma>();
            ListOfArukuldesTartalmaTempFeltoltese(index);
            szinkronizalasArukuldesTartalma();
            ArukuldesTartalmaMentesModostasIcon.ToolTip = "Módosítás";
            tbxMegjegyzes.Text = ListOfArukuldesek[index].Megjegyzes;
        }

        /// <summary>
        /// Megjeleníti az ÁruküldésTartalma ablakot
        /// </summary>
        private void arukuldesHozzaadasaShow()
        {
            RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
            //kezdő értékek beállítása
            dprDatum.SelectedDate = DateTime.Now; 
            txbTermekTallozo.Text = "Keresés..."; 
            spnlRaktarakKozottiAtadasTermekekSzurtLista.Children.Clear(); //törli a termék tallozo eredményeit
            spArukuldesTartalma.Children.Clear(); //törli a belső táblát
            lbArukuldesTartalmaSorszam.Content = new Seged().ToSorszam(ListOfArukuldesek.Count);
            lbArukuldesTartalmaVegosszeg.Content = new Seged().ToHUF(0);
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
            ListOfArukuldesTartalmaTemp = new List<ArukuldesTartalma>();
        }

        /// <summary>
        /// Kijelöli az aktuális sort, megjeleníti a törlés módosítás button-t.
        /// </summary>
        private void arukuldesSorKattintasEsemeny(object sender, MouseButtonEventArgs e)
        {
            //reset
            spArukuldesekTabla = new Seged().TablaSorokKijelolesenekEltuntese(spArukuldesekTabla);
            //akt. elem kijelolese
            ((TableRow5Column)sender).Indikator.Visibility = Visibility.Visible;
            //gomok: modosit törlöl láthatóvá tétele
            spArukuldesButtons = new Seged().GombokLathatosaga(spArukuldesButtons, true);
        }
    }
}
