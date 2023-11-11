using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Grafico.xam
    /// </summary>
    public partial class Grafico : Page
    {
        List<String> times = new List<string>();
        private List<Line> oldLines;
        private DispatcherTimer timer;

        private plot plot;
        private bool sub = false;
        private bool info = false;
        private int zoomScale = 1;

        private int tempo = 2;

        int items = 0;

        float xs = ListData1.configData.getXs();

        private SerialCOM serialCOM;
        private CEESP ceesp;

        Storyboard ShowInfo;
        Storyboard HideInfo;
        Storyboard ShowSub;
        Storyboard HideSub;

        public Grafico(SerialCOM referenceSerial, CEESP ceesp)
        {
            InitializeComponent();
            InitializeTime(10);

            ShowInfo = (Storyboard)FindResource("ShowInfo");
            HideInfo = (Storyboard)FindResource("HideInfo");
            ShowSub = (Storyboard)FindResource("ShowSub");
            HideSub = (Storyboard)FindResource("HideSub");

            // Inicia a legenda e as informações de forma escondida
            HideSub.Begin();
            HideInfo.Begin();

            Info.Visibility = Visibility.Visible;
            Legenda.Visibility = Visibility.Visible;
            // -----------------------------------------------------

            Phase.SelectedIndex = 0;

            plot = new plot(ListData1.configData.getCenterX(), ListData1.configData.getCenterY() / 2, ListData1.configData.getXs());

            this.serialCOM = referenceSerial;
            this.ceesp = ceesp;
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
            List<ColectedData> data = ListData1.colectedData;

            ColectedData valores = data[data.Count - 1]; //Pega o ultimo dado coletado

            List<Line> objects = new List<Line>

            {
                plot.createVa(valores.getVa(index) * this.zoomScale), //Adiciona Va
                plot.createIa(valores.getIa(index) * this.zoomScale, valores.getFP(index) * this.zoomScale, valores.getFPType(index)), //Adiciona Ia
                plot.createXs(valores.getIa(index) * this.zoomScale,valores.getFP(index) * this.zoomScale, valores.getFPType(index)), //Adiciona Xs
                plot.createEa() //Adiciona Ea
            };

            // Atuliza tabela de valores
            VaValue.Content = "Va: " + Math.Round(valores.getVa(index), ListData1.configData.getDecimals()).ToString() + "V";
            IaValue.Content = "Ia: " + Math.Round(valores.getIa(index), ListData1.configData.getDecimals()).ToString() + "A";
            XsIaValue.Content = "XsIa: " + Math.Round((valores.getIa(index) * ListData1.configData.getXs()), ListData1.configData.getDecimals()).ToString() + "V";
            FPValue.Content = "FP: " + Math.Round(valores.getFP(index), ListData1.configData.getDecimals()).ToString() + valores.getFPType(index);

            // Adiciona as linhas
            foreach (Line i in objects)
            {
                Graph.Children.Add(i);
            }
            oldLines = objects;
            ;
        }

        public void InitializeTime(int n)
        {
            times.Add("Pause");
            for (int i = 2; i < n; i += 2)
            {
                times.Add(i + "s");
            }
            CBTimes.ItemsSource = times;
        }

        private void CBTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TimeSelectet.Content = CBTimes.SelectedItem.ToString();

            if (CBTimes.SelectedItem.ToString() == "Pause")
            {
                if (timer != null)
                {
                    TimeSelectet.Content = "P";
                    timer.Stop();
                }
            }
            else
            {
                try
                {
                    
                    string? v = CBTimes.SelectedValue.ToString();
                    string selectedValueString = v;

                    this.tempo = int.Parse(selectedValueString[0].ToString());

                    if (timer != null)
                        timer.Interval = TimeSpan.FromSeconds(this.getRefreshTime());
                    if (timer != null && !timer.IsEnabled)
                        timer.Start();
                }
                catch
                {
                    tempo = 2;
                }
            }
        }

        private void btLegenda_Click(object sender, RoutedEventArgs e)
        {
            if (sub)
            {
                ShowSub.Stop();
                HideSub.Begin();
                sub = false;
            }
            else
            {
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
            }
            else
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
            atualiza();
        }

        public async void atualiza()
        {
            if (true)
            {
                // Realiza leitura no serialCOM e atualiza o colectedData
                ListData1.colectedData.Add(await serialCOM.readValues());

                // Atualiza o dataView da classe dados
                this.ceesp.atualizaDados();

                // Atualiza o Graph
                this.ceesp.atualizaGraph();

                if (ListData1.colectedData.Count > 0)
                {
                    try
                    {
                        drawLines();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show("Dados inválidos. Verifique se o módulo está conectado e ativo. \n" + error.Message);
                    }
                }
            }
        }

        public void setXs(float XsValue)
        {
            this.plot.setXs(XsValue);
        }

        private void ViewZoomMais_Click(object sender, RoutedEventArgs e)
        {
            if (this.zoomScale < 10)
            {
                this.zoomScale++;
            }

            LabelZoom.Content = this.zoomScale.ToString() + "x";

            // Se houver dados já lidos, atualiza
            if (ListData1.colectedData.Count != 0 && zoomScale > 0 && zoomScale < 10)
            {
                try
                {
                    drawLines();
                }
                catch
                {

                }
            }
        }

        private void ViewZoomMenos_Click(object sender, RoutedEventArgs e)
        {
            if (this.zoomScale >= 1)
            {
                this.zoomScale--;
            }

            LabelZoom.Content = this.zoomScale.ToString() + "x";

            if (ListData1.colectedData.Count != 0 && zoomScale > 0 && zoomScale < 10)
            {
                try
                {
                    drawLines();
                }
                catch { }
            }
        }

        // Sistema de refresh automático
        public void AutoRefreshInit()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(this.getRefreshTime()); // Defina o intervalo inicial
            timer.Tick += async (sender, e) => atualiza();

            // Inicie o timer
            timer.Start();
        }

        public int getRefreshTime()
        {
            return this.tempo;
        }
    }
}