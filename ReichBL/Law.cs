using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReichDAL;

namespace ReichBL
{
    public enum LawStatus
    {
        Passed = 1,
        Rejected = 2,
        Pending = 3
    }

    public class Law
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public LawStatus Status { get {

                return (LawStatus)DBVote.LawStatus(ID);
            } }
        public DateTime Time { get; set; }
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

        public Law(string description)
        {
            
            Description = description;

            DataRow row = DBlaw.AddLaw(Description);

            this.ID = (int)row["ID"];
            this.Time = (DateTime)row["Time"];
        }

        public Law(DataRow row)
        {
            ID = (int)row["ID"];
            Description = (string)row["Description"];
            Time = (DateTime)row["Time"];
        }

        public Law(int ID)
        {
            DataRow row = DBlaw.GetLawByID(ID);

            this.ID = (int)row["ID"];
            Description = (string)row["Description"];
            Time = (DateTime)row["Time"];

        }
        public void Vote(Vote vote)
        {
            DBVote.AddVote(vote.MemberId, ID, vote.VoteType, vote.VoteReason);

        }

        public override string ToString()
        {
            return $@"ID: {ID}
  Reich Description: #{Description}
  Status: {Status}";
        }
    }
}
