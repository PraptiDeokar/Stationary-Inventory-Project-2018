using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;

namespace InventoryProject.Classes
{
    public class  ConnectionClass
    {

        static string connectionString = "Data Source=.\\SqlExp;Initial Catalog=Inventory;Integrated Security=true;";
        public static string ConnectionString
        {
            get
            {
                return connectionString;
            }

        }
    }
}
