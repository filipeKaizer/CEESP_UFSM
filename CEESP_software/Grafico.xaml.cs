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
using System.Windows.Media.Animation;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Grafico.xam
    /// </summary>
    public partial class Grafico : Page
    {
        List <String> times = new List<string>();
        private List<Line> oldLines;

        private plot plot;
        private bool sub = true;
        private bool info = true;

        Storyboard ShowInfo;
        Storyboard HideInfo;
        Storyboard ShowSub;
        Storyboard HideSub;

        public Grafico()
        {
            InitializeComponent();
            InitializeTime(10);

            ShowInfo = (Storyboard)FindResource("ShowInfo");
            HideInfo = (Storyboard)FindResource("HideInfo");
            ShowSub = (Storyboard)FindResource("ShowSub");
            HideSub = (Storyboard)FindResource("HideSub");

            plot = new plot(250, 450/2, 5);
        }




        public void drawLines()
        {
            /*Remove linhas antigas*/
            if (oldLines != null)
            {
                foreach (Line i in oldLines)
                {
                    Graph.Children.Remove(i);
                }
            }

            List<Line> objects = new List<Line>
            {
                plot.createVa(200), //Adiciona Va
                plot.createIa(10, (float)0.85, true), //Adiciona Ia
                plot.createXs(10,(float)0.85), //Adiciona Xs
                plot.createEa() //Adiciona Ea
            };

            foreach(Line i in objects)
            {
                Graph.Children.Add(i);
            }

            oldLines = objects;
;        }

        public void InitializeTime(int n)
        {
            for (int i = 2; i < n; i+=2)
            {
                times.Add(i+"s");
            }
            CBTimes.ItemsSource = times;
        }

        private void CBTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeSelectet.Content = CBTimes.SelectedItem.ToString();
        }

        private void btLegenda_Click(object sender, RoutedEventArgs e)
        {
            drawLines();
            if (sub)
            {
                ShowSub.Stop();
                HideSub.Begin();
                sub = false;
            } else { 
                HideSub.Stop();
                ShowSub.Begin();
                sub = true;
            }
        }

        private void btDados_Click(object sender, RoutedEventArgs e)
        {
            if (info)
            {
                ShowInfo.Stop();
                HideInfo.Begin();
                info = false;
            } else
            {
                HideInfo.Stop();
                ShowInfo.Begin();
                info = true;
            }
        }
    }
}
