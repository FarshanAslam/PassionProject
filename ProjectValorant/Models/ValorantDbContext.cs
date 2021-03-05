using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectValorant.Models
{
    public class ValorantDbContext
    {

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "valorantproject"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

       
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }
        }
        
        public MySqlConnection AccessDatabase()
        {
            //We are instantiating the MySqlConnection Class to create an object
            
            return new MySqlConnection(ConnectionString);
        }
    }
}