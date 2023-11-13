using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

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

        public SerialCOM serialCOM;
        public Grafico grafico;
        public Inicio inicio;
        public Dados dados;
        public Graph graph;
        public Relatorio relatorio;

        private Brush rtColorInicio;
        private Brush rtColorGrafico;
        private Brush rtColorDados;
        private Brush rtColorRelatorio;
        

        public CEESP()
        {
            InitializeComponent();

            this.serialCOM = new SerialCOM(this);
            this.grafico = new Grafico(this.serialCOM, this);
            this.inicio = new Inicio(this.grafico, this, this.serialCOM);
            this.dados = new Dados(this);
            this.graph = new Graph(this);
            this.relatorio = new Relatorio(this, this.grafico);


            //Associa cada frame do tabcontrol Work a uma pagina.
            FrameInicio.Navigate(this.inicio);
            FrameGraficos.Navigate(this.grafico);
            FrameRelatorio.Navigate(this.relatorio);
            FrameDados.Navigate(this.dados);
            FrameGraphPlot.Navigate(this.graph);

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

        private void tbInicio_MouseEnter(object sender, RoutedEventArgs e)
        {
            this.rtColorInicio = rtInicio.Fill;
            rtInicio.Fill = Brushes.DarkGray;
        }

        private void btInicio_MouseLeave(object sender, RoutedEventArgs e)
        {
            rtInicio.Fill = this.rtColorInicio;
        }

        private void btGrafico_Click(object sender, RoutedEventArgs e)
        {
            SetPage(1, true);
        }

        private void btGrafico_MouseEnter(object sender, RoutedEventArgs e)
        {
            this.rtColorGrafico = rtInicio.Fill;
            rtGrafico.Fill = Brushes.DarkGray;
        }

        private void btGrafico_MouseLeave(object sender, RoutedEventArgs e)
        {
            rtGrafico.Fill = this.rtColorGrafico;
        }

        private void btDados_Click(object sender, RoutedEventArgs e)
        {
            SetPage(3, true);
        }

        private void btDados_MouseEnter(object sender, RoutedEventArgs e)
        {
            this.rtColorDados = rtInicio.Fill;
            rtDados.Fill = Brushes.DarkGray;
        }

        private void btDados_MouseLeave(object sender, RoutedEventArgs e)
        {
            rtDados.Fill = this.rtColorDados;
        }

        private void btRelatorio_Click(object sender, RoutedEventArgs e)
        {
            SetPage(2, true);
        }

        private void btRelatorio_MouseEnter(object sender, RoutedEventArgs e)
        {
            this.rtColorRelatorio = rtInicio.Fill;
            rtRelat.Fill = Brushes.DarkGray;
        }

        private void btRelatorio_MouseLeave(object sender, RoutedEventArgs e)
        {
            rtRelat.Fill = this.rtColorRelatorio;
        }

        public void atualizaDados()
        {
            this.dados.atualizaDados();
        }

        public void atualizaGraph()
        {
            this.graph.atualizaGraph();
        }

        public void atualizaGrafico()
        {
            this.grafico.drawLines();
        }

        public void atualizaCBRelatorio()
        {
            this.relatorio.atualizaCB();
        }

        public int getTimeRefresh()
        {
            return this.grafico.getRefreshTime();
        }

        public bool isValidPort()
        {
            return this.serialCOM.isValidPort();
        }

        public void setProgress(string texto, int progresso, bool ativo)
        {
            this.inicio.setProgress(texto, progresso, ativo);
        }


    }
}
