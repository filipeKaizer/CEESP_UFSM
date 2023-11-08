﻿using System;
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
        private bool sub = false;
        private bool info = false;
        int items=0;

        float xs = 5;

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

            plot = new plot(250, 450 / 2, 5);

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
            ColectedData valores = data[data.Count-1]; //Pega o ultimo dado coletado

            List<Line> objects = new List<Line>

            {
                plot.createVa(valores.getVa(index)), //Adiciona Va
                plot.createIa(valores.getIa(index), valores.getFP(index), valores.getFPType(index)), //Adiciona Ia
                plot.createXs(valores.getIa(index),valores.getFP(index), valores.getFPType(index)), //Adiciona Xs
                plot.createEa() //Adiciona Ea
            };

            // Atuliza tabela de valores
            VaValue.Content = "Va: " + Math.Round(valores.getVa(index), 2).ToString() + "V";
            IaValue.Content = "Ia: " + Math.Round(valores.getIa(index), 2).ToString() + "A";
            XsIaValue.Content = "XsIa: " + Math.Round((valores.getIa(index)*5), 2).ToString() + "V";
            FPValue.Content = "FP: " + Math.Round(valores.getFP(index), 2).ToString() + valores.getFPType(index);

            // Adiciona as linhas
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
            atualiza();
        }

        private async void atualiza()
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

        public void setXs(float XsValue)
        {
            this.plot.setXs(XsValue);
        }
    }
}