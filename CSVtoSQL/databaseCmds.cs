using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace CSVtoSQL
{
    internal class databaseCmds
    {

        
        public void AccessBridge(List<string> useableFiles, int fileTracking, string targetdb, List<string> headerList, string _fileName, string con, List<List<string>> tableContentList)
        { 
            checkTableExists(useableFiles, fileTracking, targetdb, headerList, _fileName, con, tableContentList);
        }

        private void checkTableExists(List<string> useableFiles, int fileTracking, string targetdb, List<string> headerList, string _fileName,string con, List<List<string>> tableContentList)
        {

            Console.WriteLine("Checking if table name [ " + _fileName + " ] exists in current database.");

            string tableName_ = _fileName;

            string connectionString = @"" + con + "";
            string queryString = @"BEGIN TRAN T1;
                                   SELECT * " +
                                   "FROM [" + targetdb + "].[dbo]." + tableName_ +
                                   " COMMIT TRAN T1;";

            string delString = @"BEGIN TRAN T1;
                                 DROP TABLE [" + targetdb + "].[dbo]." + tableName_ + ";" +
                                 " COMMIT TRAN T1;";

            // Check Table Exists
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    command.ExecuteReader();
                }

                Console.WriteLine("Table Exists. Overite (Y/N)");

                string del = Console.ReadLine().ToLower();
                if (del == "y")
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        SqlCommand command = new SqlCommand(delString, connection);
                        connection.Open();

                        command.ExecuteReader();
                    }

                    Console.WriteLine("Table Droped..");
                    Console.WriteLine("Initiating Transaction ...");
                    HeaderListToSQL(useableFiles, fileTracking, targetdb, headerList, tableName_, con, tableContentList);


                }
                else
                {
                    Console.WriteLine("File Was Not Loaded.");
                }
                
            }
            catch
            {
                HeaderListToSQL(useableFiles, fileTracking, targetdb, headerList, tableName_, con, tableContentList);
            }
        }


        private void HeaderListToSQL(List<string> useableFiles, int fileTracking, string targetdb, List<string> headerList, string tableName_, string con, List<List<string>> tableContentList)
        {
            
            HeadColumnTypes hct = new HeadColumnTypes();
            List<string> dataTypeList = hct.ToDefineColumnHeads(headerList);

            
            Console.WriteLine("Initiating Transaction for [ " + tableName_ + " ].");

            // Use @ character to continue string over multiple lines
            string queryString = @"BEGIN TRAN T1;
                                      CREATE TABLE [" + targetdb + "].[dbo].[" + tableName_ +
                                      "] (";

                // Building the Column Headers for table creation

                for (var i = 0; i < headerList.Count; i++)
                {

                    queryString += ("" + headerList[i] + " " + dataTypeList[i]);

                    if (i < headerList.Count - 1)
                    {
                        queryString += ", ";
                    }
                }
            
            queryString += @"); " +
                            "COMMIT TRAN T1;";

            var connectionString = @"" + con + "";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);
                try {
                    connection.Open();

                    command.ExecuteReader();

                    DataInsertRowsList(useableFiles, fileTracking, targetdb, headerList, tableName_, con, tableContentList);

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Query: " + queryString);
                    DisplaySqlErrors(ex);
                }

            }
           
        }
        private static void DataInsertRowsList(List<string> useableFiles, int fileTracking, string targetdb, List<string> headerList, string tableName_, string con, List<List<string>> tableContentList)
        {
            
            //insert rows
            string connectionString = @"" + con + "";
            
            string tableContentString1 = @" BEGIN TRAN TINSERT;
                                            INSERT INTO [" + targetdb + "].[dbo].[" + tableName_ + "] (";
            string tableContentString2 = "";

            for (int i = 0; i < headerList.Count; i++)
            {
                tableContentString1 += " [" + headerList[i] + "]";

                if (i < headerList.Count - 1)
                {
                    tableContentString1 += ", ";
                }
            }

            // This is the finished part of string 1
            tableContentString1 += " ) VALUES (";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                for (int j = 1; j < tableContentList.Count; j++) 
                {
                    for(int k = 0; k < tableContentList[j].Count; k++) 
                    {
                        tableContentString2 += " " + tableContentList[j][k] + "";

                        if (k < headerList.Count - 1)
                        {
                            tableContentString2 += ",";
                        }
                    }


                    string fullQuery = tableContentString1 + tableContentString2 + ");" +
                        "COMMIT TRAN TINSERT;";

                    SqlCommand command = new SqlCommand(fullQuery, connection);
                    try{
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        reader.Read();
                        reader.Close();
                        connection.Close();
                        tableContentString2 = "";

                        
                    }
                    catch(SqlException ex)
                    {
                        Console.WriteLine("Query: " + fullQuery);
                        DisplaySqlErrors(ex);
                    }
                    
                }
                
                Console.WriteLine("File [ " + tableName_ + " ] Upload Complete.");
                Console.WriteLine("\r\n");
                Console.WriteLine("-----------------------------------------------");
                // Call Next File After Previous has completed

                if (useableFiles.Count > fileTracking - 1 ) {
                    ProcessFile pf = new ProcessFile();
                    pf.SelectFile(targetdb, connectionString, useableFiles, fileTracking + 1);

                }

            }
        }

        private static void DisplaySqlErrors(SqlException exception)
        {
            for (int i = 0; i < exception.Errors.Count; i++)
            {
                Console.WriteLine("Index #" + i + "\n" +
                    "Error: " + exception.Errors[i].ToString() + "\n");
                // Create a drop table query, output file and reason for drop in a SQL DB 
            }
        }
    }
}
