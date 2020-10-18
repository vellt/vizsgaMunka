﻿using System;
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

namespace vizsgaMunka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Raktar> ListOfRaktarak = new List<Raktar>();
        List<Termek> ListOfTermekek = new List<Termek>();
        List<Szallitolevelek> ListOfArukuldesek = new List<Szallitolevelek>();
        public MainWindow()
        {
            InitializeComponent();
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

        #region Aktivitasok
        private void ujErtesitesHozzaadasaAktivitas(string Nev, string FelhasznaloNev, string Tartalom, DateTime datum)
        {
            string datum_ = datum.ToString("yyyy.MM.dd. HH:mm");
            ertesitesek ert = new ertesitesek();
            ((Label)((Grid)ert.Content).Children[1]).Content = Tartalom;
            ((Label)((Grid)((Grid)((Grid)ert.Content).Children[0]).Children[1]).Children[0]).Content = Nev;
            ((Label)((Grid)((Grid)((Grid)ert.Content).Children[0]).Children[1]).Children[1]).Content = FelhasznaloNev;
            ((Label)((Grid)((Grid)ert.Content).Children[0]).Children[2]).Content = datum_;
            ertesitesekSP.Children.Add(ert);

        }
        #endregion

        #region Raktarak
        private void RaktarAdatTorlese(int index)
        {
            ListOfRaktarak.RemoveAt(index);
            szinkronizalasRaktarak();
        }

        private void RaktarAdatModositasa(int index, string nev, string hely, string telefon, string email)
        {
            ListOfRaktarak[index].Nev = nev;
            ListOfRaktarak[index].Hely = hely;
            ListOfRaktarak[index].Telefon = telefon;
            ListOfRaktarak[index].Email = email;
            szinkronizalasRaktarak();
        }


        /// <summary>
        /// Raktar Kisablak indito
        /// </summary>
        private void BtnRaktarakraVonatkozoInterakciok(object sender, RoutedEventArgs e)
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

        private void BtnRaktarEsemenyek(object sender, RoutedEventArgs e)
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

        private void ujAdatHozzaadasaRaktarhoz(int index, string nev, string hely, string telefon, string email)
        {
            Raktar r = new Raktar(index, nev, hely, telefon, email);
            ListOfRaktarak.Add(r);
            szinkronizalasRaktarak();
        }

        private void szinkronizalasRaktarak()
        {
            spRaktartTabla.Children.Clear();
            for (int i = 0; i < ListOfRaktarak.Count; i++) tablazatKialakitasaRaktarak(ListOfRaktarak[i].ID, ListOfRaktarak[i].Nev, ListOfRaktarak[i].Hely, ListOfRaktarak[i].Telefon, ListOfRaktarak[i].Email, (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spRaktartTabla.Parent).ScrollToEnd();
        }

        private void tablazatKialakitasaRaktarak(int id, string nev, string hely, string telefonszam, string email, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TablaSorAdatok rtsa = new TablaSorAdatok();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = nev;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content = hely;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = telefonszam;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = email;
            ((Grid)((Button)rtsa.Content).Content).Background = hatterSzin;

            btn.Content = rtsa;
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
        }
        #endregion

        #region Termekek
        private void BtnTermekekreVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).ToolTip.ToString())
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

        private void szinkronizalasTermekek()
        {
            spTermekekTabla.Children.Clear();
            for (int i = 0; i < ListOfTermekek.Count; i++) tablazatKialakitasaTermekek(ListOfTermekek[i].ID, ListOfTermekek[i].Nev, ListOfTermekek[i].AFA, ListOfTermekek[i].MennyisegiEgyseg, ListOfTermekek[i].Egysegar, (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spTermekekTabla.Parent).ScrollToEnd();
        }

        private void tablazatKialakitasaTermekek(int id, string nev, string afa, string mennyisegiEgyseg, string egysegar, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TablaSorAdatok rtsa = new TablaSorAdatok();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = nev;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).HorizontalContentAlignment = HorizontalAlignment.Center;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content = afa;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = mennyisegiEgyseg;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).HorizontalContentAlignment = HorizontalAlignment.Right;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Padding=new Thickness(0,0,40,0);
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = egysegar;

            ((Grid)((Button)rtsa.Content).Content).Background = hatterSzin;

            btn.Content = rtsa;
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

        private void BtnTermekEsemenyek(object sender, RoutedEventArgs e)
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
        private void TermekAdatTorlese(int index)
        {
            ListOfTermekek.RemoveAt(index);
            szinkronizalasTermekek();
        }

        private void TermekAdatModositasa(int index, string nev, string afa, string mennyisegiEgyseg, string egysear)
        {
            ListOfTermekek[index].Nev = nev;
            ListOfTermekek[index].AFA = afa;
            ListOfTermekek[index].MennyisegiEgyseg = mennyisegiEgyseg;
            ListOfTermekek[index].Egysegar = egysear;
            szinkronizalasTermekek();
        }

        private void ujAdatHozzaadasaTermekez(int index, string nev, string afa, string mennyisegiEgyseg, string egysear)
        {
            Termek r = new Termek(index, nev, afa, mennyisegiEgyseg, egysear);
            ListOfTermekek.Add(r);
            szinkronizalasTermekek();
        }
        #endregion

        #region Raktarak Termekek

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
            }

        }

        #endregion

        #region Raktárközi Átadás
        private void BtnRaktarkoziAtadasraVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).ToolTip.ToString())
            {
                case "új áruküldés":
                    RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                    cmbxArukiadoRaktarak.SelectedIndex = 0;
                    cmbxBevetelezoRaktarak.SelectedIndex = 0;
                    dprDatum.SelectedDate = DateTime.Now;
                    txbTermekMEHozzaadasa.Text = String.Empty;
                    txbRaktarakKozottiAtadasTermekKereso.Text = String.Empty;
                    spnlRaktarakKozottiAtadasTermekekSzurtLista = new StackPanel(); //megcsinalni
                    break;
                case "elem módosítása":
                    RaktarkoziAtadasAblak.Visibility = Visibility.Visible;
                    //kezdő értékek beállítása
                   
                    break;
                case "elem törlése":
                    RaktarkoziAtadasTorlese.Visibility = Visibility.Visible;
                    break;
                case "szinkronizálás":
                    szinkronizalasArukuldes();
                    break;
            }
        }

        private void szinkronizalasArukuldes()
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

        private void tablazatKialakitasaArukukldesek(int id, DateTime datum, int arukiadoRaktar_id, int bevetelezoRaktar_id, int aruertek, string megjegyzes, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            ArukuldesTablazatSorai rtsa = new ArukuldesTablazatSorai();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = datum.ToString("yyyy.MM.dd. HH:mm");

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).HorizontalContentAlignment = HorizontalAlignment.Center;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content =ListOfRaktarak[arukiadoRaktar_id].Nev;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = ListOfRaktarak[bevetelezoRaktar_id].Nev;

            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).HorizontalContentAlignment = HorizontalAlignment.Right;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Padding = new Thickness(0, 0, 40, 0);
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = aruertek;

            ((Grid)((Button)rtsa.Content).Content).Background = hatterSzin;

            btn.Content = rtsa;
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

        private void BtnArukuldesEsemenyek(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowArukiadas();
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
                    #region belső táblázat eseményei
                    case "termék hozzáadása":
                        break;
                    case "termék módosítása":
                        break;
                    case "termék törlése":
                        break; 
                    #endregion

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
                    case "Nyomtatás":
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
        private void AtadasAdatTorlese(int index)
        {
            ListOfArukuldesek.RemoveAt(index);
            szinkronizalasArukuldes();
        }

        private void AtadasAdatModositasa(int index, DateTime datum, int arukiadoRaktar, int bevetelezoRaktar, int aruertek, string megjegyzes)
        {
            ListOfArukuldesek[index].Datum = datum;
            ListOfArukuldesek[index].ArukiadoRaktar_Raktar_ID = arukiadoRaktar;
            ListOfArukuldesek[index].BevetelezoRaktar_Raktar_ID = bevetelezoRaktar;
            ListOfArukuldesek[index].Aruertek = aruertek;
            ListOfArukuldesek[index].Megjegyzes = megjegyzes;
            szinkronizalasArukuldes();
        }

        private void ujAdatHozzaadasaAtadasokhoz(int id, DateTime datum, int arukiadoRaktar, int bevetelezoRaktar, int aruertek, string megjegyzes)
        {
            Szallitolevelek r = new Szallitolevelek(id, datum, arukiadoRaktar, bevetelezoRaktar, aruertek, megjegyzes);
            ListOfArukuldesek.Add(r);
            szinkronizalasArukuldes();
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ujAdatHozzaadasaRaktarhoz(0, "berettyobolt", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu");
            ujErtesitesHozzaadasaAktivitas("Szántó", "@vel", "szállítót", DateTime.Now);
            ujAdatHozzaadasaTermekez(0, "Sertés Karaj", "5 %", "kg", "1 299 Ft");
            ujAdatHozzaadasaAtadasokhoz(0, DateTime.Now, 0, 0, 700000,"megjegyzes");
            ujAdatHozzaadasaAtadasokhoz(0, DateTime.Now, 0, 0, 700000,"megjegyzes");
        }

        private void BtnArukuldesekreVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {

        }

        private void BtnArukdelesEsemenyek(object sender, RoutedEventArgs e)
        {

        }
    }
}
