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

        private void BtnArukuldesTorlesEsemenyek(object sender, RoutedEventArgs e)
        {
            int index = indexOfSelectedRowArukiadas();
            if (((Button)sender).ToolTip != null)
            {
                //Ablak bezarasa gomb eseménye
                RaktarkoziAtadasTorlese.Visibility = Visibility.Collapsed;
            }
            else
            {
                switch (((Label)((Grid)((Button)sender).Content).Children[0]).Content)
                {
                    case "Igen":
                        //meglévő termék törlése "igen"
                        RaktarkoziAtadasTorlese.Visibility = Visibility.Collapsed;
                        AtadasAdatTorlese(index);
                        break;
                    default:
                        //meglévő termék törlése "nem"
                        RaktarkoziAtadasTorlese.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }
    }
}
