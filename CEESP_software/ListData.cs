using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***********************************/
/* Essa classe é usada para armaze-*/
/* nar os dados coletados pela     */
/* classe SerialCOM.               */
/***********************************/

namespace CEESP_software
{
    public static class ListData1
    {
        public static List<ColectedData> colectedData { get; set; } = new List<ColectedData>();
    }
}
