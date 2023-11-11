using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEESP_software
{
    public class ConfigData
    {
        private float XsDefault;
        private float centerX;
        private float centerY;

        private int decimals;
        private int boundRate;
        private int IaMultiplier;

        private string cmdSend;
        private string cmdRele;
        private string cmdTest;
        
        public ConfigData()
        {
            // Valores
            this.XsDefault = 5.0f;
            this.decimals = 2;
            this.IaMultiplier = 10;

            // Grafico
            this.centerX = 250;
            this.centerY = 450;

            // Comunicação
            this.boundRate = 9600;
            this.cmdTest = "test";
            this.cmdSend = "snd";
            this.cmdRele = "rele";
        }

        public float getCenterX()
        {
            return this.centerX;
        }

        public float getCenterY()
        {
            return this.centerY;
        }

        public float getXs()
        {
            return this.XsDefault;
        }

        public int getDecimals() 
        {
            return this.decimals;
        }

        public int getBoundRate()
        {
            return this.boundRate;
        }

        public string getCmdSend()
        {
            return this.cmdSend;
        }

        public string getCmdRele()
        {
            return this.cmdRele;
        }

        public string getCmdTest()
        {
            return this.cmdTest;
        }

        public int getIaMultiplier()
        {
            return this.IaMultiplier;
        }

    }
}
