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

namespace vizsgaMunka.NavButtons
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(Statistics), new PropertyMetadata(""));

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MainColor.Background = Brushes.Green;
            if (Indikator.Background != Brushes.Green)
            {
                Indikator.Background = Brushes.WhiteSmoke;
            }
            main.Background = Brushes.WhiteSmoke;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (Indikator.Background != Brushes.Green)
            {
                MainColor.Background = (Brush)(new BrushConverter().ConvertFrom("#707070"));
                Indikator.Background = Brushes.White;
            }
            main.Background = Brushes.White;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Indikator.Background = Brushes.Green;
            MainColor.Background = Brushes.Green;
        }
    }
}
