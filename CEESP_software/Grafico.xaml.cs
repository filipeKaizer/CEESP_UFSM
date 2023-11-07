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
using System.Threading;

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
        int items=0;

        public List<ColectedData> Data;
        private SerialCOM Serial;
        Storyboard ShowInfo;
        Storyboard HideInfo;
        Storyboard ShowSub;
        Storyboard HideSub;

        public Grafico(SerialCOM referenceSerial, List<ColectedData> referenceData)
        {
            InitializeComponent();
            InitializeTime(10);

            ShowInfo = (Storyboard)FindResource("ShowInfo");
            HideInfo = (Storyboard)FindResource("HideInfo");
            ShowSub = (Storyboard)FindResource("ShowSub");
            HideSub = (Storyboard)FindResource("HideSub");

            Phase.SelectedIndex = 0;

            plot = new plot(250, 450 / 2, 5);
            Data = referenceData;
            this.Serial = referenceSerial;
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

            int index = Phase.SelectedIndex;
            MessageBox.Show(index.ToString());
            List<ColectedData> data = ListData1.colectedData;
            MessageBox.Show(data.Count.ToString());
            ColectedData valores = data[data.Count-1]; //Pega o ultimo dado coletado

            MessageBox.Show("Dado: " + valores.getIa(0));
            MessageBox.Show("Dado: " + valores.getFP(0));
            List<Line> objects = new List<Line>

            {
                plot.createVa(valores.getVa(index)), //Adiciona Va
                plot.createIa(valores.getIa(index), valores.getFP(index), valores.getFPType(index)), //Adiciona Ia
                plot.createXs(valores.getIa(index),valores.getFP(index)), //Adiciona Xs
                plot.createEa() //Adiciona Ea
            };

            VaValue.Content = "Va: "+" V";
            IaValue.Content = "Ia: " + valores.getIa(index).ToString() + " A";
            XsIaValue.Content = "XsIa: " + (valores.getIa(index)*5).ToString() + " Ω";



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
            //drawLines();
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

        private void Phase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListData1.colectedData.Count > 0)
            {
                drawLines();
            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            if (ListData1.colectedData.Count > 0)
            {
                drawLines();
            }
        }
    }
}
