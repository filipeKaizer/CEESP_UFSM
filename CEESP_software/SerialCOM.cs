using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CEESP_software
{

    public  class SerialCOM
    {
        public static String portSelected="";
        List<ColectedData> colectedDatas;

        public SerialCOM(List<ColectedData> DataReference)
        {
            this.colectedDatas = DataReference;
        }


       public async Task<List <string>> SearchPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            List<string> comp = new List<string>();
            Random random = new Random();

            foreach (String port in ports)
            {
                SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);

                try
                {
                    serialPort.Open();
                    //Realiza o teste de compatibilidade da comunicação
                    int A = random.Next(0, 101);
                    int B = random.Next(0, 101);

                    int resposta = (A % B) * (A + B); // O teste é feito com uma operação matemática
                    
                    serialPort.WriteLine("test"); //Pede teste

                    serialPort.WriteLine($"{A},{B}"); //Envia os valores de teste

                    if (int.Parse(serialPort.ReadLine()) == resposta)
                    {
                        comp.Add(port);
                        MessageBox.Show("Porta compatível: " + port);
                    }
                    serialPort.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return comp;
        }



        public async Task<ColectedData> readValues()
        {
            float[] Va = new float[4];
            int countVa = 0;
            float[] Ia = new float[4];
            int countIa = 0;
            float[] FP = new float[4];
            int countFP = 0;
            float[] CFP = new float[4];
            int countCFP = 0;
            float frequency = 0;
            float RPM = 0;

            String[] values = new string[13];
            MessageBox.Show("Entrou");
            

            try{
                SerialPort connection = new SerialPort("COM9", 9600, Parity.None, 8, StopBits.One);
                connection.Open();

                connection.WriteLine("snd"); //Pede envio de dados

                String response = connection.ReadLine();
                values = response.Split(';');
                MessageBox.Show(response);

                connection.Close();

            } catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

            for (int i=0; i<values.Length; i++)
            {
                if (i < 4)
                {
                   Va[countVa] = (float.Parse(values[i]));
                    countVa++;
                } else if (i>=4 && i<8) 
                {
                    Ia[countIa] = (float.Parse(values[i]));
                    countIa++;
                } else if (i>=8 && i<12)
                {
                    FP[countFP] = (float.Parse(values[i]));
                    countFP++;
                } else if (i>=12 && i<16)
                {
                    CFP[countCFP] = (float.Parse(values[i]));
                    countCFP++;
                } else
                {
                    frequency = (float.Parse(values[i]));
                }
            }

            ColectedData colected = new ColectedData(Ia, Va, FP, RPM, frequency);

            return colected;   

            
        }

    }
}
