using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReichDAL;

namespace ReichBL
{
    public enum VoteType
    {
        Favour = 1,
        Against = 2,
        Abstain = 3
    }

    public class Vote
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int LawId { get; set; }
        public VoteType VoteType { get; set; }
        public string VoteReason { get; set; }

        public Vote(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.MemberId = (int)row["Member ID"];
            this.LawId = (int)row["Law ID"];
            this.VoteType = (VoteType)row["VoteType"];
            if (row["VoteReason"] is DBNull) VoteReason = "";
            else
            this.VoteReason = (string)row["VoteReason"];
        }

        public static Vote GetVote(int memberId, int lawId)
        {
            DataRow row = DBVote.GetVote(memberId, lawId);
            if (row == null) return null;
            return new Vote(row);
        }

        public Vote(int id, int memberId, int lawId, int voteType, string voteReason)
        {
            Id = id;
            MemberId = memberId;
            LawId = lawId;
            VoteType = (VoteType)voteType;
            VoteReason = voteReason;
        }

        public override string ToString()
        {
            return $@"ID: {Id}
Member ID: {MemberId}
Law ID: {LawId}
Vote Type: {VoteType}
Vote Reason: {VoteReason}";
        }
    }
}
