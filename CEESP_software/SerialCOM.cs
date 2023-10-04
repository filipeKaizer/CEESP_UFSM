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
        String portSelected="";
        public SerialCOM()
        {

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

                    serialPort.WriteLine($"{A},{B}");
                    

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



        public async Task readValues(List<ColectedData> Data)
        {
            float[] Va = new float[4]; //0-Media, 1-A, 2-B, 3-C
            float[] Ia = new float[4];
            float[] FP = new float[4];
            float frequency = 0;
            float RPM = 0;

            String[] values = new string[14];

            SerialPort connection = new SerialPort(portSelected, 9600, Parity.None, 8, StopBits.One);

            try{
                connection.Open();

             //   connection.WriteLine("snd");

                values = connection.ReadLine().Split(',');

                connection.Close();
            } catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }

            for (int i=0; i<values.Length; i++)
            {
                if (i<3)
                {
                    Va.Append(float.Parse(values[i]));
                } else if (i < 8)
                {
                    Ia.Append(float.Parse(values[i]));
                } else if (i<12)
                {
                    FP.Append(float.Parse(values[i]));
                } else if (i==12)
                {
                    frequency = float.Parse(values[i]);
                } else
                {
                    RPM = float.Parse(values[i]);
                }
            }


            ColectedData colected = new ColectedData(Ia, Va, FP, RPM, frequency);

            Data.Add(colected); //Atualiza a lista de dados passada por parametro.


        }

    }
}
