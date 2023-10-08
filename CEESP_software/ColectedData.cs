using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Permissions;

namespace CEESP_software
{
    public class ColectedData
    {
        public float[] Ia;
        public float[] Va;
        public float[] FP;
        public float RPM;
        public float frequency;

        public ColectedData(float[] Ia, float[] Va, float[] FP, float RPM, float frequency)
        {
            this.Ia = Ia;
            this.Va = Va;
            this.FP = FP;
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

        public float getRPM()
        {
            return this.RPM;
        }

        public float getFrequency()
        {
            return this.frequency;
        }
    }
}
