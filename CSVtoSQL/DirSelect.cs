using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;


namespace CSVtoSQL
{
    public class DirSelect
    {

        public void Output(string _path, string _con, string _db)
        {
            int fileTracking = 0;
            List<string> useableFiles = new ();
            
            if (Directory.Exists(@""+_path+""))
            {
                Console.WriteLine(_path);
                Console.WriteLine("Confirm Y/N");

                string opt = Console.ReadLine().ToLower();
               
                if (opt == "y")
                {
                    
                    string[] fileEntries = Directory.GetFiles(_path);
                   
                    // Selecting .csv filetype
                    foreach (var fileName in fileEntries)
                    {
                        if (Path.GetExtension(fileName).ToString() == ".csv") {
                            useableFiles.Add(fileName);
                            //Console.WriteLine(fileName);
                        }
                    }

                    ProcessFile pf = new ProcessFile();
                    pf.SelectFile(_db,_con, useableFiles, fileTracking);

                }
                else
                {
                    Console.WriteLine("Have you tried turning it on and off.");
                }
            }
            else {
                Console.WriteLine("computers are now broken");
            }
        }

    }
}
