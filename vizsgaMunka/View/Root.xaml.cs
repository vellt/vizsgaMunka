﻿using System;
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
            ListOfRaktarak.Add(new Raktar(0, "berettyobolt", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu"));
            ListOfRaktarak.Add(new Raktar(1, "Derecske", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu"));
            ListOfTermekek.Add(new Termek(0, "Sertés Karaj", 5, "kg", 1299));
            ListOfTermekek.Add(new Termek(1, "Sertés Comb", 5, "kg", 1299));
            raktarSzinkronizalas();
            termekSzinkronizalas();
            
            //testfiok
            ListOfFiok.Add(new Fiok(0, "vellt", "Szántó Benjámin", "jelszo123"));

            //aktualis felhasznalo
            lbAktualUser.Content = ListOfFiok[0].TeljesNev;
            lbAktualUser.Tag = ListOfFiok[0].ID;
        }

        #region Oldalsáv
        
        

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
                    //btnTermekekTablaTorles.Visibility = Visibility.Visible;
                    //btnTermekekTablaModositas.Visibility = Visibility.Visible;
                    break;
                case "spRaktarTabla":
                    //btnTablaTorles.Visibility = Visibility.Visible;
                    //btnTablaModositas.Visibility = Visibility.Visible;
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

        private void btnAktivitasok_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnAktivitasok.Indikator.Background = Brushes.Green;
            btnAktivitasok.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 0;
        }

        private void btnStatisztikak_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnStatisztikak.Indikator.Background = Brushes.Green;
            btnStatisztikak.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 1;
        }

        private void btnRaktarak_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnRaktarak.Indikator.Background = Brushes.Green;
            btnRaktarak.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 2;
        }

        private void btnTermekek_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnTermekek.Indikator.Background = Brushes.Green;
            btnTermekek.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 3;
        }

        private void btnAtadas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnAtadas.Indikator.Background = Brushes.Green;
            btnAtadas.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 4;
        }

        private void btnBeallitasok_MouseDown(object sender, MouseButtonEventArgs e)
        {
            navButtonReset();
            btnBeallitasok.Indikator.Background = Brushes.Green;
            btnBeallitasok.MainColor.Background = Brushes.Green;
            tbcrlTartalom.SelectedIndex = 5;
        }

        private void navButtonReset()
        {
            legorduloMenusavEltuntetese();
            btnAktivitasok.Indikator.Background = Brushes.White;
            btnAktivitasok.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));

            btnStatisztikak.Indikator.Background = Brushes.White;
            btnStatisztikak.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));

            btnRaktarak.Indikator.Background = Brushes.White;
            btnRaktarak.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));

            btnTermekek.Indikator.Background = Brushes.White;
            btnTermekek.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));

            btnAtadas.Indikator.Background = Brushes.White;
            btnAtadas.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));

            btnBeallitasok.Indikator.Background = Brushes.White;
            btnBeallitasok.MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));
        }

        
    }
}
