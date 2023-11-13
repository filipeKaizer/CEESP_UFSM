using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
            }
            else
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
        }

        public void atualizaGraph()
        {
            // Define o modelo do gráfico
            var plotModel = new PlotModel { Title = "" };

            //Muda coloração
            plotModel.PlotAreaBorderColor = OxyColor.FromRgb(160, 160, 160);
            plotModel.TitleColor = OxyColor.FromRgb(160, 160, 160);
            plotModel.TextColor = OxyColor.FromRgb(160, 160, 160);
            plotModel.SubtitleColor = OxyColor.FromRgb(160, 160, 160);
            plotModel.SelectionColor = OxyColor.FromRgb(160, 160, 160);

            /* CRIA OS TIPOS DE LINHAS */
            var VaLineSeries = new LineSeries
            {
                Color = OxyColors.Green,  // Cor da linha
                StrokeThickness = ListData1.configData.getLarguraLinha()     // Espessura da linha
            };

            var IaLineSeries = new LineSeries
            {
                Color = OxyColors.Red,  // Cor da linha
                StrokeThickness = ListData1.configData.getLarguraLinha()    // Espessura da linha
            };

            var EaLineSeries = new LineSeries
            {
                Color = OxyColors.Blue,  // Cor da linha
                StrokeThickness = ListData1.configData.getLarguraLinha()    // Espessura da linha
            };

            var RPMLineSeries = new LineSeries
            {
                Color = OxyColors.Yellow,  // Cor da linha
                StrokeThickness = ListData1.configData.getLarguraLinha()   // Espessura da linha
            };
            /*-------------------------*/

            /* ADICIONA OS PONTOS IGNORANDO OS NULOS */
            List<ColectedData> data = new List<ColectedData>(); 

            foreach (ColectedData i in ListData1.colectedData)
            {
                if (i.getVa(0) != 0 && i.getVa(1) != 0 && i.getVa(2) != 0 && i.getVa(3) != 0)
                    data.Add(i);
            }

            for (int i = 0; i < data.Count; i++)
            {
                ColectedData valores = data[i];
                int t = valores.getTempo();

                VaLineSeries.Points.Add(new DataPoint(t, valores.getVa(0)));
                IaLineSeries.Points.Add(new DataPoint(t, valores.getIa(0)));
                RPMLineSeries.Points.Add(new DataPoint(t, valores.getRPM()));
                EaLineSeries.Points.Add(new DataPoint(t, valores.getEa(0)));
            }
            /*--------------------*/


            // Adicione a série ao PlotModel conforme CheckBox
            if (VaCheckBox.IsChecked == true)
                plotModel.Series.Add(VaLineSeries);

            if (IaCheck.IsChecked == true)
                plotModel.Series.Add(IaLineSeries);

            if (RPM.IsChecked == true)
                plotModel.Series.Add(RPMLineSeries);

            if (EaCheck.IsChecked == true)
                plotModel.Series.Add(EaLineSeries);

            // Associe o PlotModel ao PlotView
            PlotGraph.Model = plotModel;
        }

        private void IaCheck_Checked(object sender, RoutedEventArgs e)
        {
            atualizaGraph();
        }

        private void EaCheck_Checked(object sender, RoutedEventArgs e)
        {
            atualizaGraph();
        }

        private void RPM_Checked(object sender, RoutedEventArgs e)
        {
            atualizaGraph();
        }

        private void VaCheck_Checked(object sender, RoutedEventArgs e)
        {
            atualizaGraph();
        }
    }
}
