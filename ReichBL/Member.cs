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
        public long ReichCode { get; set; }
        public bool IsRaciallyDisabled { get; set; }
        public bool IsMember { get; set; }

        public Member(long ReichCode)
        {
            DataRow row = DBMember.GetMemberByCode(ReichCode);

            Name = (string)row["Name"];
            ReichCode = (long)row["Reich Code"];
            IsRaciallyDisabled = (bool)row["IsRaciallyDisabled"];
            IsMember = (bool)row["IsMember"];
        }

        public static void addMember(Member member)
        {
            
            DBMember.addMember(member.Name, member.ReichCode, member.IsMember);
        }

        public override string ToString()
        {
            return $@"Name: {Name}
  Reich Code: #{ReichCode}
  Is Racially Disabled?: {IsRaciallyDisabled}
  Is Member of the Parliment?: {IsMember}";
        }
    }
}
