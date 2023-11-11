using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using OfficeOpenXml;

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
                    Tempo = i.getTempo()+"s",
                    Va = i.getVa(0),
                    Ia = i.getIa(0),
                    FP = i.getFP(0),
                    RPM = i.getRPM(),
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
                    if (fp <= 0)
                    {
                        TBAngle.Text = "90º";
                    } else if (fp >= 1)
                    {
                        TBAngle.Text = "0º";
                    } else {
                        TBAngle.Text = Math.Round(Math.Acos((int)fp)).ToString() + "º";
                    }

                } catch
                {

                }

            }
        }

        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListData.SelectedIndex != -1)
            {
                ListData1.colectedData.Remove(ListData1.colectedData[ListData.SelectedIndex]);
                LegendaDefault();
                atualizaDados();

            }
        }

        private void LegendaDefault()
        {
            TBIa.Text = "0";
            TBVa.Text = "0";
            TBFP.Text = "0";
            TBRPM.Text = "0";
            TBF.Text = "0";
            TBAngle.Text = "0";
        }


        private void SalvarArquivo ()
        {
            if (ListData1.colectedData.Count != 0) { 

            SaveFileDialog SaveWindow = new SaveFileDialog();
            SaveWindow.Filter = "Arquivo Excel (*.xlsx)|*.xlsx";
            SaveWindow.Title = "Escolher caminho do arquivo de dados";

                if (SaveWindow.ShowDialog() == true)
                {
                    string caminhoArquivo = SaveWindow.FileName;

                    FileInfo fileInfo = new FileInfo(caminhoArquivo);

                    using (ExcelPackage excelPackage = new ExcelPackage(fileInfo))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Dados");

                        // Adiciona os cabeçalhos
                        worksheet.Cells[1, 1].Value = "Tempo";
                        worksheet.Cells[1, 2].Value = "Va";
                        worksheet.Cells[1, 3].Value = "Ia";
                        worksheet.Cells[1, 4].Value = "FP";
                        worksheet.Cells[1, 5].Value = "RPM";
                        worksheet.Cells[1, 6].Value = "Freq.";
                        worksheet.Cells[1, 7].Value = "Tipo";

                        // Adiciona os dados
                        int i = 0;
                        foreach (ColectedData data in ListData1.colectedData)
                        {
                            worksheet.Cells[i + 2, 1].Value = data.getTempo();
                            worksheet.Cells[i + 2, 2].Value = Math.Round(data.getVa(0), 2);
                            worksheet.Cells[i + 2, 3].Value = Math.Round(data.getIa(0), 2);
                            worksheet.Cells[i + 2, 4].Value = Math.Round(data.getFP(0), 2);
                            worksheet.Cells[i + 2, 5].Value = Math.Round(data.getRPM(), 2);
                            worksheet.Cells[i + 2, 6].Value = Math.Round(data.getFrequency(), 2);

                            if (data.getFPType(0) == 'i')
                                worksheet.Cells[i + 2, 6].Value = "Indutiva";
                            if (data.getFPType(0) == 'c')
                                worksheet.Cells[i + 2, 6].Value = "Capacitiva";
                            if (data.getFPType(0) != 'i' && data.getFPType(0) != 'c')
                                worksheet.Cells[i + 2, 6].Value = "Resistiva";

                            i++;
                        }

                        excelPackage.Save();
                    }
                }
            }



        }

        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            SalvarArquivo();
        }
    }
}
