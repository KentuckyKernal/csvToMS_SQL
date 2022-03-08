using CSVtoSQL;
using System;


namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GlobalVars glv = new GlobalVars();

            //Console.WriteLine("Enter Directory Where Files are stored: ");
            //glv.Path = @"" + Console.ReadLine() + "";

            //Console.WriteLine("\r\n");

            //Console.WriteLine("Enter connection string: ");
            //glv.Conn = @"" + Console.ReadLine() + "";
            //Console.WriteLine("\r\n");

            //Console.WriteLine("Please Type Destination Database: ");
            //glv.Dtb = @"" + Console.ReadLine() + "";

            DirSelect start = new DirSelect();
            start.Output(glv.Path, glv.Conn, glv.Dtb);



        }
    }
}