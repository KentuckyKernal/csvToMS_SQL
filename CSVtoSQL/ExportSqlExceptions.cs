using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVtoSQL
{
    public class ExportSqlExceptions
    {
        public void CheckFileExists(string message)
        {
            GlobalVars glbvars = new GlobalVars();
            string _path = @"" + glbvars.Path + @"\exceptions.txt";
            // shoud proabbly start this method when programme is booted
            if (File.Exists(_path))
            {
                writeExceptionLog(message, _path);
            }
            else
            {
                // create .txt
                File.Create(_path);
                writeExceptionLog(message, _path);
            }


            // check a .txt file exists for reporting exception along with their 
            // name without
            // 

        }

        public void writeExceptionLog(string message, string _path)
        {

            using (FileStream fs = new FileStream(_path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    {
                        sw.WriteLine(message);
                        sw.NewLine = "\n";
                    }
                }



            }
        }
    }
}
