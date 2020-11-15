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
using vizsgaMunka.TableHeaders;

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
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
