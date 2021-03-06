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
using vizsgaMunka.TableHeaders;

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
            ListOfRaktarak.Add(new Classes.Raktar(0, "berettyobolt", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu"));
            ListOfRaktarak.Add(new Classes.Raktar(1, "Derecske", "Berettyoujfalu Piac u 12", "06 30 84 63 175", "berettyo@bolt.hu"));
            ListOfTermekek.Add(new Classes.Termek(0, "Sertés Karaj", 5, "kg", 1299));
            ListOfTermekek.Add(new Classes.Termek(1, "Sertés Comb", 5, "kg", 1299));
            ListOfArukuldesek.Add(new Szallitolevelek(0, DateTime.Now, 0, 0, 0, ""));
            ListOfArukuldesek.Add(new Szallitolevelek(1, DateTime.Now, 0, 0, 0, ""));

            raktarSzinkronizalas();
            termekSzinkronizalas();
            arukuldesSzinkronizalas();
            
            //testfiok
            ListOfFiok.Add(new Fiok(0, "vellt", "Szántó Benjámin", "jelszo123"));
            //aktualis felhasznalo
            lbAktualUser.Content = ListOfFiok[0].TeljesNev;
            lbAktualUser.Tag = ListOfFiok[0].ID;
        }

        

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
            }
            else legorduloMenusavEltuntetese();
        }

        private void legorduloMenusavEltuntetese()
        {
            menu.Visibility = Visibility.Collapsed;
            GrFiokBeall.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
        }
        #endregion




        
    }
}
