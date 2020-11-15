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
        List<Classes.Termek> ListOfTermekek = new List<Classes.Termek>();

        /// <summary>
        /// Termék Dispatcher - szétosztja az eseményket, a bejövő esemenykódok alapján /ToolTip-Content/
        /// </summary>
        private void TermekekBtn(object sender, MouseButtonEventArgs e)
        {
            switch ((((Grid)sender).ToolTip == null) ? ((ContentControl)((Grid)sender).Children[0]).Content.ToString() : ((Grid)sender).ToolTip.ToString())
            {
                case "Termék hozzáadása":
                    //tooltip
                    termekHozzaadasaShow();
                    break;
                case "Termék módosítása":
                    //tooltip
                    termekModositasaShow();
                    break;
                case "Termék törlése":
                    //tooltip
                    termekTorleseShow();
                    break;
                case "Szinkronizálás":
                    //tooltip
                    termekSzinkronizalas();
                    break;
                case "Vissza":
                    //tooltip
                    termekVissza();
                    break;
                case "Mentés":
                    //content
                    termekMentes();
                    break;
                case "Módosítás":
                    //content
                    termekModositas();
                    break;
                case "Igen":
                    //content
                    termekIgen();
                    break;
                case "Nem":
                    //content
                    termekNem();
                    break;
            }
        }

        /// <summary>
        /// A törlés ablakot "bezárja"
        /// </summary>
        private void termekNem()
        {
            termekVissza();
        }

        /// <summary>
        /// Törli a kijelölt elemet a táblázatból
        /// </summary>
        private void termekIgen()
        {
            int index = new Seged().indexOfSelectedRow(spTermekTabla);
            //reportot küldünk az esemenyről:5
            ujAdatHozzaadasaAktivitasok(index, Esemeny.TermekDeleted);
            //töröljük a listából a kijelölt elemet majd szinkronizáljuk a táblát a listával és bezárjuk az ablakot
            termekVissza();
            ListOfTermekek.RemoveAt(index);
            termekSzinkronizalas();
        }

        /// <summary>
        /// Módosítja a kijelölt elemet a táblázatban
        /// </summary>
        private void termekModositas()
        {
            int index = new Seged().indexOfSelectedRow(spTermekTabla);
            //reportot küldünk az esemenyről:4
            ujAdatHozzaadasaAktivitasok(index, Esemeny.TermekModified);
            //beállítjuk a módosított értékeket a listában majd szinkronizáljuk a táblát a listával és bezárjuk az ablakot
            termekVissza();
            ListOfTermekek[index].Nev = termekSS1Nev.txbtartalom.Text;
            ListOfTermekek[index].AFA =Convert.ToInt32(termekSS1AFA.txbtartalom.Text);
            ListOfTermekek[index].MennyisegiEgyseg = termekSS1ME.txbtartalom.Text;
            ListOfTermekek[index].Egysegar =Convert.ToInt32( termekSS1Ar.txbtartalom.Text);
            termekSzinkronizalas();
        }

        /// <summary>
        /// Hozzáad egy új elemet a táblázathoz
        /// </summary>
        private void termekMentes()
        {
            //ha a rterméklistában nincs elem: 0, ellenkező esetben az utolsó tag id-jehez adunk hozzá egyet
            int index = (ListOfTermekek.Count == 0) ? 0 : ListOfTermekek[ListOfTermekek.Count - 1].ID + 1;
            //reportot küldünk az esemenyről:3
            ujAdatHozzaadasaAktivitasok(index, Esemeny.TermekCreated);
            //Hozzáadjuk az új elemet a listához majd aszinkronizáljuk a táblát a listával és bezárjuk az ablakot
            termekVissza();
            ListOfTermekek.Add(
                new Termek(
                    id: index,
                    nev: termekSS1Nev.txbtartalom.Text,
                    afa:Convert.ToInt32(termekSS1AFA.txbtartalom.Text),
                    mennyisegiEgyseg:termekSS1ME.txbtartalom.Text,
                    egysegar: Convert.ToInt32(termekSS1Ar.txbtartalom.Text)));
            termekSzinkronizalas();
        }

        /// <summary>
        /// A segédablakokat "bezárja"
        /// </summary>
        private void termekVissza()
        {
            termekSS1.Visibility = Visibility.Collapsed;
            termekSS2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Szinkronizálja a táblázatot
        /// </summary>
        private void termekSzinkronizalas()
        {
            spTermekTabla.Children.Clear();
            for (int i = 0; i < ListOfTermekek.Count; i++)
            {
                TableRow5Column row = new TableRow5Column(
                    id: ListOfTermekek[i].ID.ToString(),
                    egy: ListOfTermekek[i].Nev,
                    ketto: new Seged().ToAFA(ListOfTermekek[i].AFA),
                    harom: ListOfTermekek[i].MennyisegiEgyseg,
                    negy: new Seged().ToHUF(ListOfTermekek[i].Egysegar),
                    hatterSzin: (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                row.egy.HorizontalAlignment = HorizontalAlignment.Left;
                row.egy.Margin = new Thickness(5, 0, 0, 0);
                row.negy.Margin = new Thickness(0, 0, 40, 0);
                row.negy.HorizontalAlignment = HorizontalAlignment.Right;
                row.MouseDown += termekSorKattintasEsemeny;
                spTermekTabla.Children.Add(row);
            } 
            ((ScrollViewer)spTermekTabla.Parent).ScrollToEnd();

            //törlés módosítás gomb eltüntetése
            spTermekButtons = new Seged().GombokLathatosaga(spTermekButtons, false);
        }

        /// <summary>
        /// Megjeleníti a Termék Törlés ablakot
        /// </summary>
        private void termekTorleseShow()
        {
            termekSS2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Megjeleníti a Termék Módosítás ablakot
        /// </summary>
        private void termekModositasaShow()
        {
            termekSS1.Visibility = Visibility.Visible;
            //kezdő értékek beállítása
            int index = new Seged().indexOfSelectedRow(spTermekTabla);
            termekSS1Fejlec.Content = "Termék módosítása";
            termekSS1Nev.txbtartalom.Text = ListOfTermekek[index].Nev;
            termekSS1AFA.txbtartalom.Text = ListOfTermekek[index].AFA.ToString();
            termekSS1ME.txbtartalom.Text = ListOfTermekek[index].MennyisegiEgyseg;
            termekSS1Ar.txbtartalom.Text = ListOfTermekek[index].Egysegar.ToString();
            termekSS1Button.Content = "Módosítás";
        }

        /// <summary>
        /// Megjeleníti a Termék Hozzáadás ablakot
        /// </summary>
        private void termekHozzaadasaShow()
        {
            termekSS1.Visibility = Visibility.Visible;
            //kezdő értékek beállítása
            termekSS1Fejlec.Content = "Termkék hozzáadása";
            termekSS1Nev.txbtartalom.Text = String.Empty;
            termekSS1AFA.txbtartalom.Text = String.Empty;
            termekSS1ME.txbtartalom.Text = String.Empty;
            termekSS1Ar.txbtartalom.Text = String.Empty;
            termekSS1Button.Content = "Mentés";
        }

        /// <summary>
        /// Kijelöli az aktuális sort, megjeleníti a törlés módosítás button-t.
        /// </summary>
        private void termekSorKattintasEsemeny(object sender, MouseButtonEventArgs e)
        {
            //reset
            spTermekTabla = new Seged().TablaSorokKijelolesenekEltuntese(spTermekTabla);
            //akt. elem kijelolese
            ((TableRow5Column)sender).Indikator.Visibility = Visibility.Visible;
            //gomok: modosit törlöl láthatóvá tétele
            spTermekButtons = new Seged().GombokLathatosaga(spTermekButtons, true);
        }

        

    }
}
