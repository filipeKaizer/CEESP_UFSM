﻿using System;
using System.Numerics;
using System.Windows;

namespace CEESP_software
{
    public class ColectedData
    {
        private float[] Ia;
        private float[] Va;
        private float[] Ea;
        private float[] FP;
        private float[] CFP;
        private float RPM=0;
        private float frequency = 0;
        private int tempo=0;

        private float Xs = 5;

        public ColectedData(float[] Ia, float[] Va, float[] FP, float[] CFP, float RPM, float frequency)
        {
            this.Ia = Ia;
            this.Va = Va;
            this.FP = FP;
            this.CFP = CFP;
            this.RPM = RPM;
            this.frequency = frequency;

            float[] EaValues = {0, 0, 0, 0};

            for (int i = 0; i < 4; i++)
            {
                float angle = (float)Math.Acos(FP[i]);
                //                                   R                     Aj
                //Complex complexo = new Complex((Va[i] * FP[i]), (Va[i] * Math.Sin(angle)));
                //float Ea = 


                if (CFP[i] == 1)
                    EaValues[i] = (this.Xs * Ia[i]) + Va[i];
                if (CFP[i] == 2)
                    EaValues[i] =  Va[i] - (this.Xs * Ia[i]);
            }

            this.Ea = EaValues;

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

            this.Ea[index] = calculaEa(index, this.getVa(index), valor);

        }

        public void setRPM (float valor)
        {
            this.RPM = valor;
        }

        public void setVa (float valor, int index)
        {
            this.Va[index] = valor;

            this.Ea[index] = calculaEa(index, valor, this.getFP(index));
        }

        public void setIa (float valor, int index)
        {
            this.Ia[index] = valor;
        }

        public int getTempo()
        {
            return this.tempo;
        }

        public float getEa(int index)
        {
            return this.Ea[index];
        }

        public void setTempo(int tempo)
        {
            this.tempo = tempo;
        }

        private float calculaEa(int i, float Va, float FP)
        {
            float Ea = 0;

            float angle = (float)Math.Acos(FP);
            //                                   R                     Aj
            Complex complexo = new Complex((Va * FP), (Va * Math.Sin(angle)));

            Ea = (float)complexo.Real;

            return Ea;
        }

        public void setXs(float Xs)
        {
            this.Xs = Xs;
        }
    }
}
