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

namespace vizsgaMunka.DesignPatterns
{
    /// <summary>
    /// Interaction logic for TableRow7Column.xaml
    /// </summary>
    public partial class TableRow7Column : UserControl
    {
        public TableRow7Column(string id, string egy, string ketto, string harom, string negy, string ot, string hat, SolidColorBrush hatterSzin)
        {
            InitializeComponent();
            this.id.Content = id;
            this.egy.Content = egy;
            this.ketto.Content = ketto;
            this.harom.Content = harom;
            this.negy.Content = negy;
            this.ot.Content = ot;
            this.hat.Content = hat;
            this.hatter.Background = hatterSzin;
        }
        public TableRow7Column()
        {
            InitializeComponent();
        }
    }
}
