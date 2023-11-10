using System;

namespace CEESP_software
{
    public class ColectedData
    {
        public float[] Ia;
        public float[] Va;
        public float[] FP;
        public float[] CFP;
        public float RPM=0;
        public float frequency = 0;

        public ColectedData(float[] Ia, float[] Va, float[] FP, float[] CFP, float RPM, float frequency)
        {
            this.Ia = Ia;
            this.Va = Va;
            this.FP = FP;
            this.CFP = CFP;
            this.RPM = RPM;
            this.frequency = frequency;
        }

        public float getIa(int index)
        {
            return this.Ia[index];
        }

        public float getVa(int index)
        {
            return this.Va[index];
        }

        public float getFP(int index)
        {
            return this.FP[index];
        }

        public char getFPType(int index)
        {
            int type = (int)this.CFP[index];

            switch (type)
            {
                case 1:
                    return 'i';
                case 2:
                    return 'c';
                default:
                    return 'i';
            }
        }

        public float getRPM()
        {
            return this.RPM;
        }

        public float getFrequency()
        {
            return this.frequency;
        }

        public void setFrequency(float valor) {
            this.frequency = valor;
        }

        public void setFP (float valor, int index)
        {
            this.FP[index] = valor;
        }

        public void setRPM (float valor)
        {
            this.RPM = valor;
        }

        public void setVa (float valor, int index)
        {
            this.Va[index] = valor;
        }

        public void setIa (float valor, int index)
        {
            this.Ia[index] = valor;
        }
    }
}
