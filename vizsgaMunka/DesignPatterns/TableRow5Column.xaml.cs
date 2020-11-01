using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TableRow5Column.xaml
    /// </summary>
    public partial class TableRow5Column : UserControl
    {
        public TableRow5Column(string id, string egy, string ketto, string harom, string negy,SolidColorBrush hatterSzin)
        {
            InitializeComponent();
            this.id.Content = id;
            this.egy.Content = egy;
            this.ketto.Content = ketto;
            this.harom.Content = harom;
            this.negy.Content = negy;
            this.hatter.Background = hatterSzin;
        }
        public TableRow5Column()
        {
            InitializeComponent();
        }
    }
}
