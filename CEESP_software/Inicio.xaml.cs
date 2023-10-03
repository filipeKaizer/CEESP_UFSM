using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        List <string> compatiblePorts;

        private SerialCOM serialCOM;
        private Brush originalBtColor;


        public Inicio()
        {
            InitializeComponent();
            this.serialCOM = new SerialCOM();

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
            LPorts.Visibility = System.Windows.Visibility.Visible;
            List <string> compatiblePorts = await serialCOM.SearchPorts(); //Busca portas de forma assincrona

            foreach (string port in compatiblePorts)
            {
                LPorts.Items.Add(port);
            }
        }

        private void LPorts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
