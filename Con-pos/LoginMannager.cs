using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace Con_pos
{
    enum eTName : int { _user }
    enum eTName2 : int { _user }

    class Config
    {
        public static string Database = "ConStore";
        public static string Server = "localhost";
        public static string UserID = "root";
        public static string UserPassword = "dntls88";
        public static string[] Tables = { "Emlogin" };

        public static DataSet user_ds = null;
    }
    
    class Config2
    {
        public static string Database = "ConStore";
        public static string Server = "localhost";
        public static string UserID = "root";
        public static string UserPassword = "dntls88";
        public static string[] Tables = { "CMem" };

        public static DataSet user_ds2= null;
    }

    
}