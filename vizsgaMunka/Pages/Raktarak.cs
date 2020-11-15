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
        List<Raktar> ListOfRaktarak = new List<Raktar>();

        /// <summary>
        /// Raktár Dispatcher - szétosztja az eseményket, a bejövő esemenykódok alapján /ToolTip-Content/
        /// </summary>
        private void RaktarakBtn(object sender, MouseButtonEventArgs e)
        {
            switch ((((Grid)sender).ToolTip == null) ? ((ContentControl)((Grid)sender).Children[0]).Content.ToString() : ((Grid)sender).ToolTip.ToString())
            {
                case "Raktár hozzáadása":
                    //tooltip
                    raktarHozzaadasaShow();
                    break;
                case "Raktár módosítása":
                    //tooltip
                    raktarModositasaShow();
                    break;
                case "Raktár törlése":
                    //tooltip
                    raktarTorleseShow();
                    break;
                case "Szinkronizálás":
                    //tooltip
                    raktarSzinkronizalas();
                    break;
                case "Vissza":
                    //tooltip
                    raktarVissza();
                    break;
                case "Mentés":
                    //content
                    raktarMentes();
                    break;
                case "Módosítás":
                    //content
                    raktarModositas();
                    break;
                case "Igen":
                    //content
                    raktarIgen();
                    break;
                case "Nem":
                    //content
                    raktarNem();
                    break;
            }
        }

        /// <summary>
        /// A törlés ablakot "bezárja"
        /// </summary>
        private void raktarNem()
        {
            raktarVissza();
        }

        /// <summary>
        /// Törli a kijelölt elemet a táblázatból
        /// </summary>
        private void raktarIgen()
        {
            int index = new Seged().indexOfSelectedRow(spRaktarTabla);
            //reportot küldünk az esemenyről:2
            ujAdatHozzaadasaAktivitasok(index, Esemeny.RaktarDeleted);
            //töröljük a listából a kijelölt elemet majd szinkronizáljuk a táblát a listával és bezárjuk az ablakot
            raktarVissza();
            ListOfRaktarak.RemoveAt(index);
            raktarSzinkronizalas();
        }

        /// <summary>
        /// Módosítja a kijelölt elemet a táblázatban
        /// </summary>
        private void raktarModositas()
        {
            int index = new Seged().indexOfSelectedRow(spRaktarTabla);
            //reportot küldünk az esemenyről:1
            ujAdatHozzaadasaAktivitasok(index, Esemeny.RaktarModified);
            //beállítjuk a módosított értékeket a listában majd szinkronizáljuk a táblát a listával és bezárjuk az ablakot
            raktarVissza();
            ListOfRaktarak[index].Nev = raktarSS1Nev.txbtartalom.Text;
            ListOfRaktarak[index].Hely = raktarSS1Hely.txbtartalom.Text;
            ListOfRaktarak[index].Telefon = raktarSS1Telefon.txbtartalom.Text;
            ListOfRaktarak[index].Email = raktarSS1Email.txbtartalom.Text;
            raktarSzinkronizalas();
        }

        /// <summary>
        /// Hozzáad egy új elemet a táblázathoz
        /// </summary>
        private void raktarMentes()
        {
            //ha a raktarlistában nincs elem: 0, ellenkező esetben az utolsó tag id-jehez adunk hozzá egyet
            int index = (ListOfRaktarak.Count == 0) ? 0 : ListOfRaktarak[ListOfRaktarak.Count - 1].ID + 1;
            //reportot küldünk az esemenyről
            ujAdatHozzaadasaAktivitasok(index, 0);
            //Hozzáadjuk az új elemet a listához majd aszinkronizáljuk a táblát a listával és bezárjuk az ablakot
            raktarVissza();
            ListOfRaktarak.Add(
                new Raktar(
                    id: index,
                    nev: raktarSS1Nev.txbtartalom.Text,
                    hely: raktarSS1Hely.txbtartalom.Text,
                    telefon: raktarSS1Telefon.txbtartalom.Text,
                    email: raktarSS1Email.txbtartalom.Text));
            raktarSzinkronizalas();
        }

        /// <summary>
        /// A segédablakokat "bezárja"
        /// </summary>
        private void raktarVissza()
        {
            raktarSS1.Visibility = Visibility.Collapsed;
            raktarSS2.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Szinkronizálja a táblázatot
        /// </summary>
        private void raktarSzinkronizalas()
        {
            spRaktarTabla.Children.Clear();
            for (int i = 0; i < ListOfRaktarak.Count; i++)
            {
                TableRow5Column row = new TableRow5Column(
                    id: ListOfRaktarak[i].ID.ToString(),
                    egy: ListOfRaktarak[i].Nev,
                    ketto: ListOfRaktarak[i].Hely,
                    harom: ListOfRaktarak[i].Telefon,
                    negy: ListOfRaktarak[i].Email,
                    hatterSzin: (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
                row.egy.HorizontalAlignment = HorizontalAlignment.Left;
                row.egy.Margin = new Thickness(5, 0, 0, 0);
                row.MouseDown += raktarSorKattintasEsemeny;
                spRaktarTabla.Children.Add(row);
            }   
            ((ScrollViewer)spRaktarTabla.Parent).ScrollToEnd();

            //törlés módosítás gomb eltüntetése
            spRaktarButtons = new Seged().GombokLathatosaga(spRaktarButtons, false);
        }

        /// <summary>
        /// Megjeleníti a Raktár Törlés ablakot
        /// </summary>
        private void raktarTorleseShow()
        {
            raktarSS2.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Megjeleníti a Raktár Módosítás ablakot
        /// </summary>
        private void raktarModositasaShow()
        {
            raktarSS1.Visibility = Visibility.Visible;
            //kezdő értékek beállítása
            int index = new Seged().indexOfSelectedRow(spRaktarTabla);
            raktarSS1Fejlec.Content = "Raktár módosítása";
            raktarSS1Nev.txbtartalom.Text = ListOfRaktarak[index].Nev;
            raktarSS1Hely.txbtartalom.Text = ListOfRaktarak[index].Hely;
            raktarSS1Telefon.txbtartalom.Text = ListOfRaktarak[index].Telefon;
            raktarSS1Email.txbtartalom.Text = ListOfRaktarak[index].Email;
            raktarSS1Button.Content = "Módosítás";
        }

        /// <summary>
        /// Megjeleníti a Raktár Hozzáadás ablakot
        /// </summary>
        private void raktarHozzaadasaShow()
        {
            raktarSS1.Visibility = Visibility.Visible;
            //kezdő értkek beállítása
            raktarSS1Fejlec.Content = "Raktár hozzáadása";
            raktarSS1Nev.txbtartalom.Text = String.Empty;
            raktarSS1Hely.txbtartalom.Text = String.Empty;
            raktarSS1Telefon.txbtartalom.Text = String.Empty;
            raktarSS1Email.txbtartalom.Text = String.Empty;
            raktarSS1Button.Content = "Mentés";
        }

        /// <summary>
        /// Kijelöli az aktuális sort, megjeleníti a törlés módosítás button-t.
        /// </summary>
        private void raktarSorKattintasEsemeny(object sender, MouseButtonEventArgs e)
        {
            //reset
            spRaktarTabla = new Seged().TablaSorokKijelolesenekEltuntese(spRaktarTabla);
            //akt. elem kijelolese
            ((TableRow5Column)sender).Indikator.Visibility = Visibility.Visible;
            //gomok: modosit törlöl láthatóvá tétele
            spRaktarButtons = new Seged().GombokLathatosaga(spRaktarButtons, true);
        }
    }
}
