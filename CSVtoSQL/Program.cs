using CSVtoSQL;
using System;


namespace MyApp 
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Enter Directory Where Files are stored: ");
            string _path = @"" + Console.ReadLine() + "";

            Console.WriteLine("\r\n");

            Console.WriteLine("Enter connection string: ");
            string _con = @"" + Console.ReadLine() + "";
            Console.WriteLine("\r\n");

            Console.WriteLine("Please Type Destination Database: ");
            string _db = @"" + Console.ReadLine() + "";

            DirSelect start = new DirSelect();
            start.Output(_path, _con, _db);

        }
    }
}