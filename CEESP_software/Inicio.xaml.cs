using System.Collections.Generic;
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

            setProgress("", 0, false);
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
                setProgress("Iniciando busca", 2, true);

                List<string> compatiblePorts = await serialCOM.SearchPorts(); //Busca portas de forma assincrona

                foreach (string port in compatiblePorts)
                {
                    LPorts.Items.Add(port);
                }

                if (compatiblePorts.Count >= 1)
                    LPorts.Visibility = System.Windows.Visibility.Visible;

                setProgress("", 0, false);
                verbose.Visibility = Visibility.Visible;

                if (compatiblePorts.Count >= 1)
                {
                    verbose.Content = "Portas compativeis: " + compatiblePorts.Count;
                } else
                {
                    verbose.Content = "Nenhuma porta válida encontrada";
                }
            }
            else
            {
                if (Xs.Text != "")
                {
                    verbose.Visibility = Visibility.Hidden;
                    float XsValue = float.Parse(Xs.Text);
                    this.grafico.setXs(XsValue);

                    // Inicializa a atualização automática
                    this.grafico.AutoRefreshInit();
                }
                else
                {
                    MessageBox.Show("O valor informado não é válido.\nAdotando Xs = 5.");
                    this.grafico.setXs(ListData1.configData.getXs());
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


        public void setProgress(string texto, float progresso, bool ativo)
        {
            if (ativo)
            {
                progress.Visibility = Visibility.Visible;
                verbose.Visibility = Visibility.Visible;

                //Adiciona valores
                progress.Value = progresso;
                verbose.Content = texto;
            } else
            {
                progress.Visibility = Visibility.Hidden;
                verbose.Visibility = Visibility.Hidden;
            }
        }
    }
}
