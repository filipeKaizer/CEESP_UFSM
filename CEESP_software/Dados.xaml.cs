using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Dados.xam
    /// </summary>
    public partial class Dados : Page
    {
        private Brush ActualSaveAfterEditColor;
        public CEESP CEESP;
        public List<ColectedData> Data;
        public SerialCOM Serial;

        bool edit=false;
        public Dados(CEESP reference, List<ColectedData> referenceData, SerialCOM referenceSerial)
        {
            InitializeComponent();
            CEESP = reference;
            this.Data = referenceData;
            Serial = referenceSerial;

        }


        private void btSaveAfterEdit_Click(object sender, RoutedEventArgs e)
        {
            float[] Ia = {1,2,3};
            float[] Va = {1,2,3};
            float[] FP = {1,2,3};
            ColectedData Data1 = new ColectedData
            Data1.Add(new ColectedData(Ia,Va,FP, 2400, 60));
            

            List<ListViewItemModel> ListViewItemsModels = Data.Select(x => new ListViewItemModel()
            {
                Ia = x.Ia[0].ToString(),
                Va = x.Va[0].ToString(),
                FP = x.FP[0].ToString(),
                RPM = x.RPM.ToString(),
                frequency = x.frequency.ToString()
            }).ToList();

            foreach(ListViewItemModel i in ListViewItemsModels)
            {
                ListViewItem Item = new ListViewItem();

                Item.Content = i;


                ListData.Items.Add(Item);
            }
        }

        private void btSaveAfterEdit_MouseEnter(object sender, RoutedEventArgs e)
        {
            ActualSaveAfterEditColor = RSave.Fill;
            RSave.Fill = Brushes.LightGray;
        }

        private void btSaveAfterEdit_MouseLeave(object sender, RoutedEventArgs e)
        {
            RSave.Fill = ActualSaveAfterEditColor;
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btGraph_Click(object sender, RoutedEventArgs e)
        {
            CEESP.SetPage(5, false);
        }

        private void btEdit_Click_1(object sender, RoutedEventArgs e)
        {
            changeVisibility();
        }

        private void changeVisibility()
        {
            if (edit)
            {
                TBIa.IsEnabled = false;
                TBVa.IsEnabled = false;
                TBXA.IsEnabled = false;
                TBAngle.IsEnabled = false;
                edit = false;
                btSaveAfterEdit.Visibility = Visibility.Hidden;
            }
            else
            {
                TBIa.IsEnabled = true;
                TBVa.IsEnabled = true;
                TBXA.IsEnabled = true;
                TBAngle.IsEnabled = true;
                edit = true;
                btSaveAfterEdit.Visibility = Visibility.Visible;
            }

        }
    }
}
