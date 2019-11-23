using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReichDAL;

namespace ReichBL
{
    public class Member
    {


        public string Name { get; set; }
        public int ReichCode { get; set; }
        public bool IsMember { get; set; }
        public string Password { get; set; }

        public Member(int ReichCode)
        {
            DataRow row = DBMember.GetMemberByCode(ReichCode);

            Name = (string)row["Name"];
            this.ReichCode = ReichCode;
            Password = (string)row["Password"];
            IsMember = (bool)row["IsMember"];
        }

        public Member(DataRow row)
        {
            Name = (string)row["Name"];
            Password = (string)row["Password"];
            ReichCode = (int)row["Reich Code"];
            IsMember = (bool)row["IsMember"];
        }

        public Member(string Name, string Password)
        {
            DataRow row = DBMember.GetMember(Name, Password);
            if (row == null) throw new ArgumentException("Authentication Failed");

            Name = (string)row["Name"];
            Password = (string)row["Password"];
            ReichCode = (int)row["Reich Code"];
            IsMember = (bool)row["IsMember"];
        }

        public List<Law> GetUnvotedLaws()
        {
            DataTable memeberVotes = DBVote.GetVotesByMember(this.ReichCode);
            DataTable pending = DBlaw.GetPendingLaws();
            List<Law> a = new List<Law>();
            for (int i = 0; i < pending.Rows.Count; i++)
            {
                bool didntVote = true;
                for (int j = 0; j < memeberVotes.Rows.Count; j++)
                {
                    if (pending.Rows[i]["ID"] == memeberVotes.Rows[j]["ID"])
                        didntVote = false;
                }
                if (!didntVote)
                {
                    Law b = new Law(pending.Rows[i]);
                    a.Add(b);
                }
            }
            return a;


        }

        public static void AddMember(Member member)
        {   
            DBMember.AddMember(member.Name, member.ReichCode, member.IsMember);
        }

        public void RemoveFromCouncil()
        {
            DBMember.RemoveMemberFromCouncil(ReichCode);
        }
        

        public static List<Member> GetCouncilMembers()
        {
            List<Member> list = new List<Member>();
            DataTable tb = DBMember.GetActiveMembers();

            foreach(DataRow row in tb.Rows)
            {
                list.Add(new Member(row));
            }

            return list;
        }

        public List<Vote> GetVotesByMember()
        {
            List<Vote> votesByMember = new List<Vote>();
            DataTable a = DBVote.GetVotesByMember(this.ReichCode);
            for (int i = 0; i < a.Rows.Count; i++)
            {
                Vote vote = new Vote(a.Rows[i]);
                votesByMember.Add(vote);
            }
            return votesByMember;
        }

        public override string ToString()
        {
            return $@"Name: {Name}
  Reich Code: #{ReichCode}
  Is Member of the Parliment?: {IsMember}";
        }
    }
}
