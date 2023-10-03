using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CEESP_software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush originalBtColor;
        public MainWindow()
        {
            InitializeComponent();
            Storyboard initialize = (Storyboard)FindResource("Init");
            initialize.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CEESP Software = new CEESP();
            Software.Show();
            this.Close();
        }


        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            originalBtColor = bInicio.Fill;
            bInicio.Fill = Brushes.LightGray;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bInicio.Fill = originalBtColor;
        }
    }
}
