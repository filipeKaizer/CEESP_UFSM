using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CEESP_software
{
    /// <summary>
    /// Interação lógica para Dados.xam
    /// </summary>
    public partial class Dados : Page
    {
        private Brush ActualSaveAfterEditColor;
        public CEESP CEESP;


        bool edit = true;
        public Dados(CEESP reference)
        {
            InitializeComponent();
            CEESP = reference;
            changeVisibility();
        }

        private void btSaveAfterEdit_Click(object sender, RoutedEventArgs e)
        {
            atualizaBaseDeDados();
        }

        private void btSaveAfterEdit_MouseEnter(object sender, RoutedEventArgs e)
        {
            ActualSaveAfterEditColor = RSave.Fill;
            RetSave.Fill = Brushes.LightGray;
        }

        private void btSaveAfterEdit_MouseLeave(object sender, RoutedEventArgs e)
        {
            RetSave.Fill = ActualSaveAfterEditColor;
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
                TBFP.IsEnabled = false;
                TBRPM.IsEnabled = false;
                TBF.IsEnabled = false;
                edit = false;
                btSaveAfterEdit.Visibility = Visibility.Hidden;
                RetSave.Visibility = Visibility.Hidden;
                LabelSave.Visibility = Visibility.Hidden;
            }
            else
            {
                TBIa.IsEnabled = true;
                TBVa.IsEnabled = true;
                TBFP.IsEnabled = true;
                TBRPM.IsEnabled = true;
                TBF.IsEnabled = true;
                edit = true;
                btSaveAfterEdit.Visibility = Visibility.Visible;
                btSaveAfterEdit.IsEnabled = true;
                RetSave.Visibility = Visibility.Visible;
                LabelSave.Visibility = Visibility.Visible;
            }
        }

        public void atualizaDados()
        {
            ListViewItem item;

            ListData.Items.Clear();

            foreach (ColectedData i in ListData1.colectedData)
            {
                item = new ListViewItem();
                item.Content = new
                {
                    Va = i.getVa(0),
                    Ia = i.getIa(0),
                    FP = i.getFP(0),
                    RPM = '-',
                    F = i.getFrequency()
                };

                ListData.Items.Add(item);
            }
        }

        private void atualizaBaseDeDados()
        {
            if (ListData.SelectedIndex != -1)
            {
                try
                {
                    ListData1.colectedData[ListData.SelectedIndex].setIa(float.Parse(TBIa.Text), 0);
                    ListData1.colectedData[ListData.SelectedIndex].setVa(float.Parse(TBVa.Text), 0);
                    ListData1.colectedData[ListData.SelectedIndex].setFP(float.Parse(TBFP.Text), 0);
                    ListData1.colectedData[ListData.SelectedIndex].setRPM(float.Parse(TBRPM.Text));
                    ListData1.colectedData[ListData.SelectedIndex].setFrequency(float.Parse(TBF.Text));
                    atualizaDados();
                } catch
                {
                    MessageBox.Show("Erro.");
                }
            }
        }

        private void ListData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListData.SelectedIndex != -1)
            {
                int p = 2;

                float fp = ListData1.colectedData[ListData.SelectedIndex].getFP(0);
                TBIa.Text = Math.Round(ListData1.colectedData[ListData.SelectedIndex].getIa(0), p).ToString();
                TBVa.Text = Math.Round(ListData1.colectedData[ListData.SelectedIndex].getVa(0), p).ToString();
                TBFP.Text = Math.Round(fp, p).ToString();
                TBRPM.Text = Math.Round(ListData1.colectedData[ListData.SelectedIndex].getRPM(), p).ToString();
                TBF.Text = Math.Round(ListData1.colectedData[ListData.SelectedIndex].getFrequency(), p).ToString();

                try
                {
                    TBAngle.Text = Math.Round(Math.Acos((double) fp), 2).ToString() + "º"; 
                } catch
                {

                }

            }
        }
    }
}
