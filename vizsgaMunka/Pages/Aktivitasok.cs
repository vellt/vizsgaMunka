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

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Eseményeket ad hozzá a StackPanelhez
        /// </summary>
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
    }
}
