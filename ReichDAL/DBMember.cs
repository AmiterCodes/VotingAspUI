using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;


namespace ReichDAL
{
    public class DBMember
    {
        private static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        private static string PATH = @"..\..\..\Reichabase.accdb";

        public static DataRow GetMemberByCode(long code)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sql = $"SELECT * FROM Member WHERE [Reich Code] = {code};";

                DataTable tb = helper.GetDataTable(sql);

                if(tb.Rows.Count == 0)
                {
                    throw new ArgumentException("No Member Found");
                }
                return tb.Rows[0];
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }

        public static DataTable GetActiveMembers()
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if(helper.OpenConnection())
            {
                string sql = "SELECT * FROM Member WHERE IsMember = Yes;";

                DataTable tb = helper.GetDataTable(sql);

                return tb;
            } else
            {
                throw new Exception("Could not open connection");
            }
        }

        public static void addMember(string name, long ReichCode, bool isMember)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            string a;
            if (isMember) a = "Yes";
            else a = "No";
            if (helper.OpenConnection())
            {
                string sql = "INSERT INTO Memeber (Name, [Reich Code], IsMember) Values ('" + name  + "', " + ReichCode  +", '"+a+ "');";

                int b = helper.WriteData(sql);

                if (b == -1)
                {
                    throw new ArgumentException("wrong info");
                }

            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }

        // TODO: Add member method
    }
}
