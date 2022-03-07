using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVtoSQL
{
    internal class HeadColumnTypes
    {
        List<string> rtdt = new List<string>();

        public List<string> ToDefineColumnHeads(List<string> headerList) {

            Console.WriteLine("Would you like to assign data type to column heads? Y/N");
            Console.WriteLine("\r\n");
            string headDecision = Console.ReadLine().ToLower();
            

            if (headDecision == "y")
            {
                rtdt = ReturnDataTypes(headerList);
            }
            if (headDecision == "n")
            {
                // varchar(MAX) removes the scenario of unwanted truncation
                
                Console.WriteLine("Column heads will be set to varchar(MAX) by default.");
                rtdt = ReturnDefaultDataTypes(headerList);
            }

            return rtdt;
        }


        public List<string> ReturnDataTypes(List<string> headerList) {

            List<string> columnDataType = new List<string>();

            for (var i = 0; i < headerList.Count; i++)
            {
                Console.WriteLine("Data type for: " + headerList[i] + ":");
                string headType = Console.ReadLine().ToLower();
                columnDataType.Add(headType);
            }

            return columnDataType;
        }

        public List<string> ReturnDefaultDataTypes(List<string> headerList)
        {

            List<string> columnDataType = new List<string>();

            for (var i = 0; i < headerList.Count; i++)
            {
                columnDataType.Add("varchar(MAX)");
            }
            
            return columnDataType;
        }
    }
}
