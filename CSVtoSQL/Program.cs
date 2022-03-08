using CSVtoSQL;
using System;


namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            GlobalVars glv = new GlobalVars();

            DirSelect start = new DirSelect();
            start.Output(glv.Path, glv.Conn, glv.Dtb);

        }
    }
}
