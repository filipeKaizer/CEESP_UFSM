using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;

namespace CEESP_software
{

    public class SerialCOM
    {
        private String portSelected = "";
        private CEESP cessp;

        private int tempoCorrente = 0;

        public SerialCOM(CEESP ceesp)
        {
            this.cessp = ceesp;
        }

        public async Task<List<string>> SearchPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            List<string> comp = new List<string>();
            Random random = new Random();
            int percent = 0;

            if (ports.Length > 0)
                percent = (98) / ports.Length;

            foreach (String port in ports)
            {
                this.cessp.setProgress((port + ": Testando..."), (int)(percent / 2), true);

                SerialPort serialPort = new SerialPort(port, 9600, Parity.None, 8, StopBits.One);

                try
                {
                    serialPort.Open();
                    //Realiza o teste de compatibilidade da comunicação
                    int A = random.Next(0, 101);
                    int B = random.Next(0, 101);

                    int resposta = (A % B) * (A + B); // O teste é feito com uma operação matemática

                    serialPort.WriteLine(ListData1.configData.getCmdTest()); //Pede teste

                    serialPort.WriteLine($"{A},{B}"); //Envia os valores de teste

                    if (int.Parse(serialPort.ReadLine()) == resposta)
                    {
                        this.cessp.setProgress((port + ": Compativel"), percent, true);
                        comp.Add(port);
                    } else
                    {
                        this.cessp.setProgress((port + ": Não compativel"), percent, true);
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
            bool connected = false;

            float[] Va = new float[4];
            float[] Ia = new float[4];
            float[] FP = new float[4];
            float[] CFP = new float[4];
            float frequency = 0;
            float RPM = 0;


            String[] values = { };

            try
            {
                SerialPort connection = new SerialPort(portSelected, ListData1.configData.getBoundRate(), Parity.None, 8, StopBits.One);
                connection.Open();

                connection.WriteLine(ListData1.configData.getCmdSend()); //Pede envio de dados

                values = await Receber(connection); //Chama de forma assincrona a função para ler dados do arduino
                String msg = "";
                foreach (String i in values)
                {
                    msg += i + " ";
                }

                connection.Close();
                connected = true;

            }
            catch (Exception e)
            {
                connected = false;
            }

            if (connected)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != "NaN")
                    {
                        string[] separados = values[i].Split('=');

                        float valor = float.Parse(separados[1]) / 100;

                        switch (separados[0])
                        {
                            case "Vm":
                                Va[0] = valor;
                                break;
                            case "Va":
                                Va[1] = valor;
                                break;
                            case "Vb":
                                Va[2] = valor;
                                break;
                            case "Vc":
                                Va[3] = valor;
                                break;
                            case "Im":
                                Ia[0] = valor * ListData1.configData.getIaMultiplier();
                                break;
                            case "Ia":
                                Ia[1] = valor * ListData1.configData.getIaMultiplier();
                                break;
                            case "Ib":
                                Ia[2] = valor * ListData1.configData.getIaMultiplier();
                                break;
                            case "Ic":
                                Ia[3] = valor * ListData1.configData.getIaMultiplier();
                                break;
                            case "FPt":
                                FP[0] = valor;
                                break;
                            case "FPa":
                                FP[1] = valor;
                                break;
                            case "FPb":
                                FP[2] = valor;
                                break;
                            case "FPc":
                                FP[3] = valor;
                                break;
                            case "CFPt":
                                CFP[0] = valor;
                                break;
                            case "CFPa":
                                CFP[1] = valor;
                                break;
                            case "CFPb":
                                CFP[2] = valor;
                                break;
                            case "CFPc":
                                CFP[3] = valor;
                                break;
                            case "F":
                                frequency = valor;
                                break;
                        }
                    }
                }
            }

            // Adicio o tempo corrente baseado no refreshTime
            tempoCorrente += cessp.getTimeRefresh();
            
            ColectedData colected = new ColectedData(Ia, Va, FP, CFP, RPM, frequency);
            colected.setTempo(tempoCorrente);            

            return colected;
        }

        private Task<string[]> Receber(SerialPort con)
        {
            String[] values = new string[13];
            return Task.Run(() =>
            {
                string message = "";

                String response = con.ReadLine();
                String[] values = response.Split(';');

                return values;
            });
        }

        public void setPort(String port)
        {
            this.portSelected = port;
        }

        public bool isValidPort()
        {
            if (this.portSelected != "")
            {
                return true;
            }
            return false;
        }
    }
}
