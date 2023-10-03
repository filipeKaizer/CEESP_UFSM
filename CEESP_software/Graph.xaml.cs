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
        }



    }
}
