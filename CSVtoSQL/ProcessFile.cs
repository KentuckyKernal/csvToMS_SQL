using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace CSVtoSQL
{
    public class ProcessFile
    {
        public void SelectFile(string _db, string _con, List<string> useableFiles, int fileTracking) {

            
            List<string> csvToList = new();
            List<string> csvList = new();
            List<string> headerList = new();
            List<List<string>> tableContentList = new();
            string selectedFile = useableFiles[fileTracking];

            using (StreamReader sr = new StreamReader(selectedFile))
            {

                while (sr.Peek() > -1)
                {
                    csvToList.Add(sr.ReadLine());

                }

            }

            var headSplit = csvToList[0].Split(',');
            //Remove Unwanted chars
            for (var i = 0; i < csvToList.Count; i++)
            {
                csvList.Add(csvToList[i].Replace('"', '\''));
            }


            for (var i = 0; i < csvList.Count; i++)
            {
                var columnSplit = csvList[i].Split(',').ToList();
                tableContentList.Add(columnSplit);
            }

            // Error Checking
            //if (csvToList.Count == tableContentList.Count)
            //{
            //    Console.WriteLine("No Data Lost.");
            //}

            for (var i = 0; i < headSplit.Length; i++)
            {
                //will probably want to remove several chars like this
                headerList.Add(headSplit[i].Replace('"', ' ').Trim());
            }

            databaseCmds dts = new databaseCmds();
            dts.AccessBridge(useableFiles, fileTracking, _db, headerList, Path.GetFileNameWithoutExtension(selectedFile), _con, tableContentList);

        }//End Loop
    }
}
