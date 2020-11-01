using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using vizsgaMunka.DesignPatterns;

namespace vizsgaMunka.Classes
{
    /// <summary>
    /// Ez az osztály segéd metódusokat tartalmaz
    /// </summary>
    public class Seged
    {
        /// <summary>
        /// Megkeresi az indexét a kijeleölt sornak
        /// </summary>
        public int indexOfSelectedRow(StackPanel rows)
        {
            for (int i = 0; i < rows.Children.Count; i++)
            {
                if (((TableRow5Column)rows.Children[i]).Indikator.Visibility==Visibility.Visible)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Módosítja egy gombcsoport azon elemeinek láthatóságát aminek van Tag értéke
        /// </summary>
        /// <param name="buttons">A gombcsoport</param>
        /// <param name="lathatosag">A láthatóságának értéke: true/false</param>
        /// <returns></returns>
        public StackPanel GombokLathatosaga(StackPanel buttons, bool lathatosag)
        {
            for (int i = 0; i < buttons.Children.Count; i++)
            {
                if (((Grid)buttons.Children[i]).Tag != null)
                {
                    if (lathatosag)
                    {
                        ((Grid)buttons.Children[i]).Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ((Grid)buttons.Children[i]).Visibility = Visibility.Collapsed;
                    }
                }
            }
            return buttons;
        }

        /// <summary>
        /// A hozzáadott táblátzat sorainak láthatóságait collapsedre állítja
        /// </summary>
        /// <param name="rows">A táblázat ami tartalmazza a sorokat: Stackpanel</param>
        /// <returns></returns>
        public StackPanel TablaSorokKijelolesenekEltuntese(StackPanel rows)
        {
            //reset
            for (int i = 0; i < rows.Children.Count; i++)
            {
                ((TableRow5Column)rows.Children[i]).Indikator.Visibility = Visibility.Collapsed;
            }

            return rows;
        }
    }
}
