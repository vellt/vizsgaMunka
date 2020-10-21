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

namespace vizsgaMunka
{
    public partial class MainWindow : Window
    {
        List<Raktar> ListOfRaktarak = new List<Raktar>();
        /// <summary>
        /// Törli a kijelölt sort a táblázatból
        /// </summary>
        private void RaktarAdatTorlese(int index)
        {
            ListOfRaktarak.RemoveAt(index);
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// A meglévő táblázat egy kijelölt sorának adatát módódítja
        /// </summary>
        private void RaktarAdatModositasa(int index, string nev, string hely, string telefon, string email)
        {
            ListOfRaktarak[index].Nev = nev;
            ListOfRaktarak[index].Hely = hely;
            ListOfRaktarak[index].Telefon = telefon;
            ListOfRaktarak[index].Email = email;
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// Megkeresi az indexét a kijeleölt sornak
        /// </summary>
        private int indexOfSelectedRowRaktarak()
        {
            var background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF05B422"));
            for (int i = 0; i < spRaktartTabla.Children.Count; i++)
            {
                var background2 = ((Grid)((Grid)spRaktartTabla.Children[i]).Children[0]).Background;
                if (background2.ToString() == background.ToString())
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// A táblázatba hozzáad egy sort, a beállított argumentumok alapján
        /// </summary>
        private void ujAdatHozzaadasaRaktarhoz(int index, string nev, string hely, string telefon, string email)
        {
            Raktar r = new Raktar(index, nev, hely, telefon, email);
            ListOfRaktarak.Add(r);
            szinkronizalasRaktarak();
        }

        /// <summary>
        /// Szinkronizálja a táblát
        /// </summary>
        private void szinkronizalasRaktarak()
        {
            spRaktartTabla.Children.Clear();
            for (int i = 0; i < ListOfRaktarak.Count; i++) tablazatKialakitasaRaktarak(ListOfRaktarak[i].ID, ListOfRaktarak[i].Nev, ListOfRaktarak[i].Hely, ListOfRaktarak[i].Telefon, ListOfRaktarak[i].Email, (i % 2 == 0) ? Brushes.WhiteSmoke : Brushes.White);
            ((ScrollViewer)spRaktartTabla.Parent).ScrollToEnd();
        }

        /// <summary>
        /// Beszúrja a sorokat
        /// </summary>
        private void tablazatKialakitasaRaktarak(int id, string nev, string hely, string telefonszam, string email, SolidColorBrush hatterSzin)
        {
            Button btn = new Button();
            btn.Padding = new Thickness(-2);
            btn.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            btn.VerticalContentAlignment = VerticalAlignment.Stretch;
            btn.BorderThickness = new Thickness(0);
            btn.Cursor = Cursors.Hand;

            TablaSorAdatok rtsa = new TablaSorAdatok();
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[0]).Content = id;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[1]).Content = nev;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[2]).Content = hely;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[3]).Content = telefonszam;
            ((Label)((Grid)((Button)rtsa.Content).Content).Children[4]).Content = email;
            ((Grid)((Button)rtsa.Content).Content).Background = hatterSzin;

            btn.Content = rtsa;
            btn.Click += SorokKijeloleseKattintasera;

            Grid gr0 = new Grid();
            ColumnDefinition colDef1 = new ColumnDefinition();
            colDef1.Width = new GridLength(7);
            ColumnDefinition colDef2 = new ColumnDefinition();
            gr0.ColumnDefinitions.Add(colDef1);
            gr0.ColumnDefinitions.Add(colDef2);

            Grid gr = new Grid();
            gr.Height = 30;
            gr.Background = hatterSzin;

            Grid.SetColumn(gr, 0);
            Grid.SetColumn(btn, 1);


            gr0.Children.Add(gr);
            gr0.Children.Add(btn);
            spRaktartTabla.Children.Add(gr0);
            //törlés módosítűás gomb eltüntetése
            btnTablaTorles.Visibility = Visibility.Collapsed;
            btnTablaModositas.Visibility = Visibility.Collapsed;
        }
    }
}
