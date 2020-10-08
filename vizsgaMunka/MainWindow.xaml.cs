using System;
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
        public MainWindow()
        {
            InitializeComponent();

            ujErtesitesHozzaadasaAktivitas("Szántó", "@vel", "szállítót", DateTime.Now);
            ujRaktartablaSorHozzaadasa("2", "Berettyoujfalu", "4100, Berettyoujfalu, Bolt utca 3", "06 30 33 33 333", "berettyo@bolt.hu", true);
            ujRaktartablaSorHozzaadasa("2", "Berettyoujfalu", "4100, Berettyoujfalu, Bolt utca 3", "06 30 33 33 333", "berettyo@bolt.hu", false);
            ujRaktartablaSorHozzaadasa("2", "Berettyoujfalu", "4100, Berettyoujfalu, Bolt utca 3", "06 30 33 33 333", "berettyobolt@kukori.hu", true);
        }

        private void SorokKijeloleseKattintasera(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ((StackPanel)((Button)sender).Parent).Children.Count; i++)
            {
                ((Grid)((Grid)((Button)((Button)((StackPanel)((Button)sender).Parent).Children[i]).Content).Content).Children[6]).Background = Brushes.Transparent;
            }
            ((Grid)((Grid)((Button)((Button)sender).Content).Content).Children[6]).Background= new SolidColorBrush(Color.FromRgb(5, 180, 34));
        }


        private void ujRaktartablaSorHozzaadasa(string id, string nev, string hely, string telefonszam, string email, bool szin)
        {
            //szin==true : szurke
            //szin==false : feher
            Button btn = new Button();
            btn.Padding = new Thickness(-1);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;
            btn.Click+= BtnRaktarEsemenyekLathatovaTetele;

            raktarTablaSorAdatok rtsa = new raktarTablaSorAdatok();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = nev;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content = hely;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = telefonszam;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = email;
            if (!szin) ((Grid)((Button)rtsa.Content).Content).Background = Brushes.White;

            btn.Content=rtsa;
            btn.Click += SorokKijeloleseKattintasera;
            spRaktartTabla.Children.Add(btn);
        }

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
            ((Grid)((Grid)((Grid)btnBeallitasok.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnRaktarkoziAtadas.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnTermekek.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnRaktarak.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnStatisztikak.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
            ((Grid)((Grid)((Grid)btnAktivitasok.Content).Children[0]).Children[0]).Background= Brushes.WhiteSmoke;
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
                    tbcrlTartalom.SelectedIndex=0;
                    break;
                case "Statisztikák":
                    tbcrlTartalom.SelectedIndex=1;
                    break;
                case "Raktárak":
                    tbcrlTartalom.SelectedIndex=2;
                    break;
                case "Termékek":
                    tbcrlTartalom.SelectedIndex=3;
                    break;
                case "Raktárközi átadás":
                    tbcrlTartalom.SelectedIndex=4;
                    break;
                case "Beállítások":
                    tbcrlTartalom.SelectedIndex=5;
                    break;
            }
        }

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
            else
            {
                legorduloMenusavEltuntetese();
            }
        }

        private void legorduloMenusavEltuntetese()
        {
            menu.Visibility = Visibility.Collapsed;
            GrFiokBeall.Background = new SolidColorBrush(Color.FromRgb(246, 246, 246));
            AktivMenuszalaghozHatter.Visibility = Visibility.Collapsed;

        }

        private void BtnRaktarakraVonatkozoInterakciok(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).ToolTip.ToString())
            {
                case "raktár hozzáadása":
                    raktarHozzaadasa.Visibility = Visibility.Visible;
                    break;
                case "raktár módosítása":
                    raktarModositasa.Visibility = Visibility.Visible;
                    break;
                case "raktár törlése":
                    raktarTorlese.Visibility = Visibility.Visible;
                    break;
                case "szinkronizálás":

                    break;
            }
        }

        private void BtnRaktarEsemenyekLathatovaTetele(object sender, RoutedEventArgs e)
        {
            btnTablaTorles.Visibility = Visibility.Visible;
            btnTablaModositas.Visibility = Visibility.Visible;



            //if (((Button)sender).ToolTip!=null)
            //{
            //    //ablak bezarasa
            //    raktarHozzaadasa.Visibility = Visibility.Collapsed;
            //    raktarModositasa.Visibility = Visibility.Collapsed;
            //    raktarTorlese.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
            //    {
            //        case "Mentés":
            //            //új raktar modosítása
            //            raktarHozzaadasa.Visibility = Visibility.Collapsed;
            //            break;
            //        case "Módosítás":
            //            //meglévő raktar módosítása
            //            raktarModositasa.Visibility = Visibility.Collapsed;
            //            break;
            //        case "Igen":
            //            //meglévő raktar törlése "igen"
            //            raktarTorlese.Visibility = Visibility.Collapsed;
            //            break;
            //        case "szinkronizálás":
            //            //szinkronizálás
            //            break;
            //        default:
            //            //meglévő raktar törlése "nem"
            //            raktarTorlese.Visibility = Visibility.Collapsed;
            //            break;
            //    }
            //}

        }

        private void BtnRaktarEsemenyek(object sender, RoutedEventArgs e)
        {
            //((Button)((Button)sender).Content).Content



            //if (((Button)sender).ToolTip!=null)
            //{
            //    //ablak bezarasa
            //    raktarHozzaadasa.Visibility = Visibility.Collapsed;
            //    raktarModositasa.Visibility = Visibility.Collapsed;
            //    raktarTorlese.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
            //    {
            //        case "Mentés":
            //            //új raktar modosítása
            //            raktarHozzaadasa.Visibility = Visibility.Collapsed;
            //            break;
            //        case "Módosítás":
            //            //meglévő raktar módosítása
            //            raktarModositasa.Visibility = Visibility.Collapsed;
            //            break;
            //        case "Igen":
            //            //meglévő raktar törlése "igen"
            //            raktarTorlese.Visibility = Visibility.Collapsed;
            //            break;
            //        case "szinkronizálás":
            //            //szinkronizálás
            //            break;
            //        default:
            //            //meglévő raktar törlése "nem"
            //            raktarTorlese.Visibility = Visibility.Collapsed;
            //            break;
            //    }
            //}
            
        }

        private void BtnEsemenyek(object sender, RoutedEventArgs e)
        {

        }
    }
}
