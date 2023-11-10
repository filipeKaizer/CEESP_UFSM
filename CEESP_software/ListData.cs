using System.Collections.Generic;

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
