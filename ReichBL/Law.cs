using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReichDAL;

namespace ReichBL
{
    public class Law
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        private List<Vote> votes;

        public List<Vote> Votes { get {
                votes = new List<Vote>();
                DataTable tb = DBVote.GetVotesByLaw(ID);

                foreach(DataRow row in tb.Rows)
                {
                    votes.Add(new Vote(row));
                }
                return votes;
            } }

        public Law(int ID)
        {
            DataRow row = DBlaw.GetLawByID(ID);

            ID = (int)row["ID"];
            Description = (string)row["Description"];
            Status = (int)row["Status"];
            
        }
        public void Vote(Vote vote)
        {
            DBVote.addVote(vote.MemberId, ID, vote.VoteType, vote.VoteReason);

        }

        public override string ToString()
        {
            return $@"ID: {ID}
  Reich Description: #{Description}
  Status: {Status}";
        }
    }
}
