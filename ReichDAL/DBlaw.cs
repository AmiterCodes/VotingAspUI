using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ReichDAL
{
    public class DBlaw
    {
        private static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        private static string PATH = @"Reichabase.accdb";
        public static DataTable GetPassedLaws()
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            if (helper.OpenConnection())
            {
                string sql = "SELECT * FROM Laws;";

                DataTable tb = helper.GetDataTable(sql);

                return tb;
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
        public static DataTable GetPendingLaws()
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            if (helper.OpenConnection())
            {
                string sql = "SELECT * FROM Laws WHERE Status = 3;";

                DataTable tb = helper.GetDataTable(sql);

                return tb;
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
        

        public static DataRow GetLawByID(int ID)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sql = $"SELECT * FROM Laws WHERE [ID] = {ID};";

                DataTable tb = helper.GetDataTable(sql);

                if (tb.Rows.Count == 0)
                {
                    throw new ArgumentException("No Law Found");
                }
                return tb.Rows[0];
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
        public static DataRow AddLaw(string Description)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sql = "INSERT INTO Laws (Description, Status) Values ('"+Description +"');";

                int a=helper.InsertWithAutoNumKey(sql);


                if (a == -1)
                {
                    throw new ArgumentException("wrong info");
                }
                return helper.GetDataTable("SELECT FROM Laws WHERE ID = " + a + ";").Rows[0];

            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
        
        public static void UpdateLawStatus(int id, int status)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sql = "UPDATE Laws SET Status =" + status + "WHERE ID=" + id + ";";

                int a = helper.WriteData(sql);
                
                if (a == -1)
                {
                    throw new ArgumentException("wrong info");
                }
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
    }
}
