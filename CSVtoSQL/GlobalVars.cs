using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVtoSQL
{
    public class GlobalVars
    {
        private string path = @"path"; 
        public string Path { 
            get => path;
            set => path = value; 
        }
        public string conn = @"cons";
        public string Conn
        {
            get => conn;
            set => conn = value;
        }
        public string dtb = "db";
        public string Dtb
        {
            get => dtb;
            set => dtb = value;
        }
    }
}
