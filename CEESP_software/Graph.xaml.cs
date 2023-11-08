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
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.Wpf;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Graph.xam
    /// </summary>
    public partial class Graph : Page
    {
        CEESP CEESP;

        Storyboard Showcheck;
        Storyboard Hidecheck;

        private PlotModel plotModel;
        bool CheckVisible = false;

        public Graph(CEESP reference)
        {
            InitializeComponent();

            CEESP = reference;
            Showcheck = (Storyboard)FindResource("Mostra_Check");
            Hidecheck = (Storyboard)FindResource("Esconde_Check");
        }


        public void changeCheck()
        {
            if (!CheckVisible)
            {
               Hidecheck.Stop();
               Showcheck.Begin();
               Check_Situation.Content = "A";
            } else
            {
                Showcheck.Stop();
                Hidecheck.Begin();
                Check_Situation.Content = "D";
            }
            CheckVisible = !CheckVisible;
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            CEESP.SetPage(3, false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeCheck();
            atualizaGraph();


        }

        public void atualizaGraph()
        {
            var plotModel = new PlotModel { Title = "Meu Gráfico" };

            // Limpa


            /* CRIA OS TIPOS DE LINHAS */ 
            var VaLineSeries = new LineSeries
            {
                Color = OxyColors.Green,  // Cor da linha
                StrokeThickness = 2     // Espessura da linha
            };

            var IaLineSeries = new LineSeries
            {
                Color = OxyColors.Red,  // Cor da linha
                StrokeThickness = 2     // Espessura da linha
            };

            var EaLineSeries = new LineSeries
            {
                Color = OxyColors.Blue,  // Cor da linha
                StrokeThickness = 2     // Espessura da linha
            };
            /*-------------------------*/

            /* ADICIONA OS PONTOS */
            List<ColectedData> data = ListData1.colectedData;
            
            for (int i = 0; i < data.Count; i++)
            {
                ColectedData valores = data[i];

                VaLineSeries.Points.Add(new DataPoint(i, valores.getVa(0)));
                IaLineSeries.Points.Add(new DataPoint(i, valores.getIa(0)));
            }
            /*--------------------*/



            // Adicione a série ao PlotModel
            plotModel.Series.Add(VaLineSeries);
            plotModel.Series.Add(IaLineSeries);

            // Associe o PlotModel ao PlotView
            PlotGraph.Model = plotModel;
        } 
    }
}
