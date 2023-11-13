using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Relatorio.xam
    /// </summary>
    public partial class Relatorio : Page
    {
        private CEESP ceesp;
        private Grafico grafico;
        public Relatorio(CEESP ceesp, Grafico grafico)
        {
            InitializeComponent();
            this.ceesp = ceesp;
            this.grafico = grafico;
        }



        public void atualizaCB()
        {
            CBValores.Items.Clear();

            if (ListData1.colectedData.Count > 0)
            {
                int index = 0;
                foreach (ColectedData i in ListData1.colectedData)
                {
                    CBValores.Items.Add("Grafico " + index + " - " + i.getTempo() + "s");
                    index++;
                }
            }
        }

        private void CKMostRecent_Checked(object sender, RoutedEventArgs e)
        {
            if (ListData1.colectedData.Count > 0)
                CBValores.SelectedIndex = ListData1.colectedData.Count - 1;
            else
                CBValores.SelectedIndex = -1;
        }
    }
}
