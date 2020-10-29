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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Aktivitas> ListOfAktivitas = new List<Aktivitas>();
        List<Fiok> ListOfFiok = new List<Fiok>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ujAdatHozzaadasaRaktarhoz(0, "berettyobolt", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu");
            ujAdatHozzaadasaRaktarhoz(1, "Derecske", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu");
            ujAdatHozzaadasaTermekez(0, "Sertés Karaj", 5, "kg", 1299);
            ujAdatHozzaadasaTermekez(1, "Sertés Comb", 5, "kg", 1299);
            //ujAdatHozzaadasaAtadasokhoz(0, DateTime.Now, 0, 0, 700000, "megjegyzes");
            //ujAdatHozzaadasaAtadasokhoz(0, DateTime.Now, 0, 0, 700000, "megjegyzes");
            
            //testfiok
            ListOfFiok.Add(new Fiok(0, "vellt", "Szántó Benjámin", "jelszo123"));

            //aktualis felhasznalo
            lbAktualUser.Content = ListOfFiok[0].TeljesNev;
            lbAktualUser.Tag = ListOfFiok[0].ID;
        }

        #region Oldalsáv
        private void BtnNavigacioAzOldalakKozott(object sender, RoutedEventArgs e)
        {
            legorduloMenusavEltuntetese();
            //hatter
            ((Grid)btnBeallitasok.Content).Background = Brushes.White;
            ((Grid)btnRaktarkoziAtadas.Content).Background = Brushes.White;
            ((Grid)btnTermekek.Content).Background = Brushes.White;
            ((Grid)btnRaktarak.Content).Background = Brushes.White;
            ((Grid)btnStatisztikak.Content).Background = Brushes.White;
            ((Grid)btnAktivitasok.Content).Background = Brushes.White;
            //hatter modosító
            ((Grid)((Button)sender).Content).Background = Brushes.WhiteSmoke;

            //vonal
            ((Grid)((Grid)((Grid)btnBeallitasok.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnRaktarkoziAtadas.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnTermekek.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnRaktarak.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnStatisztikak.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnAktivitasok.Content).Children[0]).Children[0]).Background = Brushes.WhiteSmoke;
            //vonalszín módosító
            ((Grid)((Grid)((Grid)((Button)sender).Content).Children[0]).Children[0]).Background = new SolidColorBrush(Color.FromRgb(5, 180, 34));

            //index kép
            ((Path)((Canvas)((Grid)((Grid)btnBeallitasok.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            ((Path)((Canvas)((Grid)((Grid)btnRaktarkoziAtadas.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            ((Path)((Canvas)((Grid)((Grid)btnTermekek.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            ((Path)((Canvas)((Grid)((Grid)btnRaktarak.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            ((Path)((Canvas)((Grid)((Grid)btnStatisztikak.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            ((Path)((Canvas)((Grid)((Grid)btnAktivitasok.Content).Children[0]).Children[1]).Children[0]).Fill = Brushes.Gray;
            //index kép szín módosító
            ((Path)((Canvas)((Grid)((Grid)((Button)sender).Content).Children[0]).Children[1]).Children[0]).Fill = new SolidColorBrush(Color.FromRgb(5, 180, 34));

            //betűszín
            ((Label)((Grid)((Grid)btnBeallitasok.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            ((Label)((Grid)((Grid)btnRaktarkoziAtadas.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            ((Label)((Grid)((Grid)btnTermekek.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            ((Label)((Grid)((Grid)btnRaktarak.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            ((Label)((Grid)((Grid)btnStatisztikak.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            ((Label)((Grid)((Grid)btnAktivitasok.Content).Children[0]).Children[2]).Foreground = Brushes.Gray;
            //betűszín módosító
            ((Label)((Grid)((Grid)((Button)sender).Content).Children[0]).Children[2]).Foreground = Brushes.Black;

            //oldal kivalsztasa label content alapján
            switch (((Label)((Grid)((Grid)((Button)sender).Content).Children[0]).Children[2]).Content.ToString())
            {
                case "Aktivitások":
                    tbcrlTartalom.SelectedIndex = 0;
                    break;
                case "Statisztikák":
                    tbcrlTartalom.SelectedIndex = 1;
                    break;
                case "Raktárak":
                    tbcrlTartalom.SelectedIndex = 2;
                    break;
                case "Termékek":
                    tbcrlTartalom.SelectedIndex = 3;
                    break;
                case "Raktárközi átadás":
                    tbcrlTartalom.SelectedIndex = 4;
                    break;
                case "Beállítások":
                    tbcrlTartalom.SelectedIndex = 5;
                    break;
            }
        }

        #endregion

        #region Felsősáv
        private void BtnKereses(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Visibility.Collapsed;
            lbKereses.Visibility = Visibility.Visible;
        }

        private void BtnFiokBeallitasokJobbFelsoSarok(object sender, RoutedEventArgs e)
        {
            var background = GrFiokBeall.Background as SolidColorBrush;
            var background2 = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFF6F6F6"));
            if (background.Color == background2.Color)
            {
                menu.Visibility = Visibility.Visible;
                GrFiokBeall.Background = new SolidColorBrush(Color.FromRgb(241, 241, 241));
                AktivMenuszalaghozHatter.Visibility = Visibility.Visible;
            }
            else legorduloMenusavEltuntetese();
        }

        private void legorduloMenusavEltuntetese()
        {
            menu.Visibility = Visibility.Collapsed;
            GrFiokBeall.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
            AktivMenuszalaghozHatter.Visibility = Visibility.Collapsed;
        }
        #endregion

        //Raktarak
        partial void BtnRaktarakraVonatkozoInterakciok(object sender, RoutedEventArgs e);
        partial void BtnRaktarEsemenyek(object sender, RoutedEventArgs e);
        partial void tablazatKialakitasaRaktarak(int id, string nev, string hely, string telefonszam, string email, SolidColorBrush hatterSzin);
        partial void szinkronizalasRaktarak();
        partial void ujAdatHozzaadasaRaktarhoz(int index, string nev, string hely, string telefon, string email);
        partial void RaktarAdatModositasa(int index, string nev, string hely, string telefon, string email);
        partial void RaktarAdatTorlese(int index);

       
        //Termekek
        partial void BtnTermekekreVonatkozoInterakciok(object sender, RoutedEventArgs e);
        partial void BtnTermekEsemenyek(object sender, RoutedEventArgs e);
        partial void szinkronizalasTermekek();
        partial void tablazatKialakitasaTermekek(int id, string nev, int afa, string mennyisegiEgyseg, int egysegar, SolidColorBrush hatterSzin);
        partial void TermekAdatTorlese(int index);
        partial void TermekAdatModositasa(int index, string nev, int afa, string mennyisegiEgyseg, int egysear);
        partial void ujAdatHozzaadasaTermekez(int index, string nev, int afa, string mennyisegiEgyseg, int egysear);

        //Arukuldesek
        partial void BtnRaktarkoziAtadasraVonatkozoInterakciok(object sender, RoutedEventArgs e);
        partial void tablazatKialakitasaArukukldesek(int id, DateTime datum, int arukiadoRaktar_id, int bevetelezoRaktar_id, int aruertek, string megjegyzes, SolidColorBrush hatterSzin);
        partial void BtnArukuldesTorlesEsemenyek(object sender, RoutedEventArgs e);
        partial void szinkronizalasArukuldes();
        partial void ujAdatHozzaadasaAtadasokhoz(int id, DateTime datum, int arukiadoRaktar, int bevetelezoRaktar, int aruertek, string megjegyzes);

        #region Raktarak Termekek Arukuldesek

        /// <summary>
        /// A tablazatban a sor elejét mező kijelölésekor zöldre szinezi
        /// </summary>
        private void SorokKijeloleseKattintasera(object sender, RoutedEventArgs e)
        {
            //aktiv mező kijelölése
            for (int i = 0; i < ((StackPanel)((Grid)((Button)sender).Parent).Parent).Children.Count; i++) ((Grid)((Grid)((StackPanel)((Grid)((Button)sender).Parent).Parent).Children[i]).Children[0]).Background = (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White;
            ((Grid)((Grid)((Button)sender).Parent).Children[0]).Background = new SolidColorBrush(Color.FromRgb(5, 180, 34));

            switch (((StackPanel)((Grid)((Button)sender).Parent).Parent).Name.ToString())
            {
                case "spArukuldesekTabla":
                    btnRaktarkoziAtadasModositas.Visibility = Visibility.Visible;
                    btnArukuldesekTablaTorles.Visibility = Visibility.Visible;
                    break;
                case "spTermekekTabla":
                    btnTermekekTablaTorles.Visibility = Visibility.Visible;
                    btnTermekekTablaModositas.Visibility = Visibility.Visible;
                    break;
                case "spRaktartTabla":
                    btnTablaTorles.Visibility = Visibility.Visible;
                    btnTablaModositas.Visibility = Visibility.Visible;
                    break;
                case "spArukuldesTartalma":
                    btnArukuldesTartalmaModositas.Visibility = Visibility.Visible;
                    btnArukuldesTartalmaTorles.Visibility = Visibility.Visible;
                    break;
            }

        }

        #endregion



        //ArukuldesTartalma
        partial void btnArukuldesTartalmaToolTip(object sender, RoutedEventArgs e);
        partial void btnArukuldesTartalmaContent(object sender, RoutedEventArgs e);
        partial void AddText(object sender, RoutedEventArgs e);
        partial void RemoveText(object sender, RoutedEventArgs e);
        partial void TermekTallozoFeltolteseSzurtListaval(List<Classes.Termek> szurtLista);
        partial void txbTermekTallozoTextChanged(object sender, TextChangedEventArgs e);
        partial void tablazatKialakitasaArukuldesTartalma(int iD, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr, SolidColorBrush hatterSzin);
        partial void szinkronizalasArukuldesTartalma();
        partial void ujAdatHozzaadasaArukuldesTartalma(int index, string nev, double mennyiseg, string mennyisegiEgyseg, int egysegar, int aFA, int bruttoAr);
    }
}
