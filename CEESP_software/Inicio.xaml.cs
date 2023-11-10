using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        List<string> compatiblePorts;

        private SerialCOM serialCOM;
        private Brush originalBtColor;

        private CEESP ceesp;

        Storyboard connectAnim;
        bool connectAnimStatus = false;
        bool portSelected = false;

        Grafico grafico;

        public Inicio(Grafico grafico, CEESP ceesp, SerialCOM serialCOM)
        {
            InitializeComponent();
            this.serialCOM = serialCOM;
            connectAnim = (Storyboard)FindResource("Connected");
            this.grafico = grafico;
            this.ceesp = ceesp;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            originalBtColor = btRetangulo.Fill;
            btRetangulo.Fill = Brushes.LightGray;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btRetangulo.Fill = originalBtColor;
        }

        private async void Buscar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!portSelected)
            {
                LPorts.Visibility = System.Windows.Visibility.Visible;
                List<string> compatiblePorts = await serialCOM.SearchPorts(); //Busca portas de forma assincrona

                foreach (string port in compatiblePorts)
                {
                    LPorts.Items.Add(port);
                }
            } else
            {
                if (Xs.Text != "")
                {
                    float XsValue = float.Parse(Xs.Text);
                    this.grafico.setXs(XsValue);

                    // Inicializa a atualização automática

                } else
                {
                    MessageBox.Show("O valor informado não é válido.\nAdotando Xs = 5.");
                    this.grafico.setXs(5);
                }

                ceesp.SetPage(1, true);
            }
        }

        private void LPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!connectAnimStatus)
            {
                connectAnim.Begin();
                connectAnimStatus = true;
            }
            Buscar.Content = "Iniciar";
            portSelected = true;

            serialCOM.setPort(LPorts.SelectedItem.ToString());
        }

    }
}
