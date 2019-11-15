using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ReichBL
{
    public class Vote
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int LawId { get; set; }
        public int VoteType { get; set; }
        public string VoteReason { get; set; }

        public Vote(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.MemberId = (int)row["Member ID"];
            this.LawId = (int)row["Law ID"];
            this.VoteType = (int)row["VoteType"];
            this.VoteReason = (string)row[VoteReason];
        }



        public Vote(int id, int memberId, int lawId, int voteType, string voteReason)
        {
            Id = id;
            MemberId = memberId;
            LawId = lawId;
            VoteType = voteType;
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
