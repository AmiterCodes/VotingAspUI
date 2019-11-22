using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ReichDAL
{
    public class DBVote
    {
        private static string PROVIDER = @"Microsoft.ACE.OLEDB.12.0";
        private static string PATH = @"Reichabase.accdb";
        public static DataTable GetVotesByLaw(int lawId)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            if (helper.OpenConnection())
            {
                string sql = "SELECT * FROM Votes WHERE [Law ID] = " + lawId + ";";
                return helper.GetDataTable(sql);
            }
            else
            {
                throw new Exception("could not open connection");
            }
        }

        public static DataTable GetVotesByMember(int memberId)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);
            if (helper.OpenConnection())
            {
                string sql = "SELECT * FROM Votes WHERE [Member ID] = " + memberId + ";";
                return helper.GetDataTable(sql);
            }
            else
            {
                throw new Exception("could not open connection");
            }
        }

        public static void AddVote(long memberID, int lawID, int voteType, string voteReason)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sql = @"INSERT INTO Votes
([Member ID], [Law ID], VoteType, VoteReason)
Values (" + memberID  + ", " + lawID  +", "+voteType +", '"+voteReason + "');";

                int a = helper.WriteData(sql);

                if (a == -1)
                {
                    throw new ArgumentException("wrong info");
                }
                else
                {
                    int result = LawStatus(lawID);
                    
                    string sql1 = $"UPDATE Laws SET Status={result} WHERE ID="+lawID +";";
                    helper.WriteData(sql1);
                    
                }

            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }
        public static int LawStatus(int lawID)
        {
            DBHelper helper = new DBHelper(PROVIDER, PATH);

            if (helper.OpenConnection())
            {
                string sel = "SELECT VoteType FRON Votes WHERE [Law ID]=" + lawID + " AND VoteType=1;";
                DataTable votes = helper.GetDataTable(sel);

                int forVotes = 0;
                int againstVotes = 0;
                int neutralVotes = 0;

                foreach (DataRow row in votes.Rows)
                {
                    int type = (int)row["VoteType"];

                    if (type == 1) forVotes++;
                    if (type == 2) againstVotes++;
                    if (type == 3) neutralVotes++;
                }
                

                DataTable pairlamentMembers = DBMember.GetActiveMembers();
                int numOfMembers=pairlamentMembers.Rows.Count;
                if ((numOfMembers- neutralVotes) / 2 < forVotes)
                    return 1;
                else if ((numOfMembers - neutralVotes) / 2 <= againstVotes)
                    return 2;
                else return 3;
            }
            else
            {
                throw new Exception("Could not open connection");
            }
        }

        // TODO: Check if law passed

        // TODO: Add Vote to law method
    }
}
