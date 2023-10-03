using System.Windows;
using System.Windows.Media.Animation;

namespace CEESP_software
{
    /// <summary>
    /// Lógica interna para CEESP.xaml
    /// </summary>
    public partial class CEESP : Window
    {
        private Storyboard EscondeMenu;
        private Storyboard MostraMenu;
        private bool MenuAberto = false;

        /*private Brush rtColorInicio;
        private Brush rtColorGrafico;
        private Brush rtColorDados;
        private Brush rtColorRelatorio;
        */

        public CEESP()
        {
            InitializeComponent();

            //Associa cada frame do tabcontrol Work a uma pagina.
            FrameInicio.Navigate(new Inicio());
            FrameGraficos.Navigate(new Grafico());
            FrameDados.Navigate(new Dados(this));
            FrameGraphPlot.Navigate(new Graph(this));

            this.EscondeMenu = (Storyboard)FindResource("EscondeMenu");
            this.MostraMenu = (Storyboard)FindResource("MostraMenu");

            EscondeMenu.Begin();
            MenuAberto = false;

            Work.SelectedIndex = 0; //Sequencia: Inicio (0), Grafico, Relatorio, Dados, Config, Graph (5).
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            if (MenuAberto == false)
            {
                EscondeMenu.Stop();
                MostraMenu.Begin();
                MenuAberto = true;
            }
            else
            {
                MostraMenu.Stop();
                EscondeMenu.Begin();
                MenuAberto = false;
            }
        }

        private void Esconder()
        {
            MostraMenu.Stop();
            EscondeMenu.Begin();
            MenuAberto = false;
        }

        public void SetPage(int index, bool menu)
        {
            this.Work.SelectedIndex = index;
            if (menu)
            {
                Esconder();
            }
        }

        private void btIncio_Click(object sender, RoutedEventArgs e)
        {
            SetPage(0, true);  
        }

        private void btGrafico_Click(object sender, RoutedEventArgs e)
        {
            SetPage(1, true);
        }

        private void btDados_Click(object sender, RoutedEventArgs e)
        {
            SetPage(3, true);
        }

        private void btRelatorio_Click(object sender, RoutedEventArgs e)
        {
            SetPage(2, true);
        }

    }
}
