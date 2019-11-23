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
                return this.GetLawStatus();
            } }
        public DateTime Time { get; set; }
        private List<Vote> votes;

        public static List<Law> GetLaws()
        {
            DataTable a = DBlaw.GetLaws();
            List<Law> b = new List<Law>();
            for (int i = 0; i < a.Rows.Count; i++)
            {
                b.Add(new Law(a.Rows[i]));
            }
            return b;
        }

        public LawStatus GetLawStatus()
        {
            DataTable votes = DBVote.GetVotesByLaw(this.ID);
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
            int numOfMembers = pairlamentMembers.Rows.Count;
            if ((numOfMembers - neutralVotes) / 2 < forVotes)
                return LawStatus.Passed;
            else if ((numOfMembers - neutralVotes) / 2 <= againstVotes)
                return LawStatus.Rejected;

            else
            {
                if (Time.Hour >= 8 && Time.Hour <= 20)
                {
                    if (DateTime.Now.Subtract(Time) > TimeSpan.FromHours(5))
                    {
                        if (forVotes > againstVotes)
                            return LawStatus.Passed;
                        else return LawStatus.Rejected;
                    }
                    else
                        return LawStatus.Pending;

                }
                else
                {
                    if (DateTime.Now.Subtract(Time) > TimeSpan.FromHours(15))
                    {
                        if (forVotes > againstVotes)
                            return LawStatus.Passed;
                        else return LawStatus.Rejected;
                    }
                    else
                        return LawStatus.Pending;
                }

            }
        }

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
            DBVote.AddVote(vote.MemberId, ID, (int)vote.VoteType, vote.VoteReason);

        }

        public override string ToString()
        {
            return $@"ID: {ID}
  Description: {Description}
  Status: {Status}";
        }
    }
}
