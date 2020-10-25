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
            ActivityRowForList a = new ActivityRowForList();
            a.lbTeljesNev.Content = Nev;
            a.lbFelhasznaloNev.Content = FelhasznaloNev;
            a.datum.Content = datum.ToString("yyyy.MM.dd. HH:mm");
            a.lbAktivitasLeirasa.Content = Tartalom;
            
            ertesitesekSP.Children.Add(a);
        }
    }
}
