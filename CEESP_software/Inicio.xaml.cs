using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System.Windows.Media.Animation;
using System.Linq;

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

        Storyboard connectAnim;
        bool connectAnimStatus = false;

        public Inicio()
        {
            InitializeComponent();
            this.serialCOM = new SerialCOM();
            connectAnim = (Storyboard)FindResource("Connected");
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
            if (!connectAnimStatus)
            {
                connectAnim.Begin();
                connectAnimStatus = true;
            }


           // data.Add(serialCOM.readValues());
        }
    }
}
