using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using ReichBL;

public partial class Votes : System.Web.UI.Page
{
    public string table = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string l = Request.QueryString["Law"];
        string m = Request.QueryString["Member"];
        if (string.IsNullOrEmpty(l) && string.IsNullOrEmpty(m))
        {
            Response.Redirect("Default.aspx");
        } else if (!string.IsNullOrEmpty(l))
        {
            Law law = new Law(int.Parse(l));
            table = GetLawTable(law);
        } else
        {
            Member member = new Member(int.Parse(m));
            table = GetMemberTable(member);
        }
        
    }

    public static string GetMemberTable(Member m)
    {

        string table = "";
        
        

        table += "<h1>" + m.Name + "'s Votes</h1>";

        table += "<table border='1'>";
        

        table += "<tr>";

        table += "<th>Law</th><th>Opinion</th><th>Reason</th>";



        table += "</tr>";
        

        foreach (Law law in Law.GetLaws())
        {
            table += "<tr>";
            Vote vote = Vote.GetVote(m.ReichCode, law.ID);
            table += "<td><a href='Votes.aspx?Law=" + law.ID + "'>" + law.Description + "</a></td>";
            if (vote != null)
            {
                table += "<td>" + vote.VoteType + "</td>";

                table += "<td>" + (string.IsNullOrEmpty(vote.VoteReason) ? "No Reason Given" : vote.VoteReason) + "</td>";
            }
            else
            {
                table += "<td>Hasn't Voted</td>";
            }


            table += "</tr>";
        }

        table += "</table>";
        return table;
    }

    public static string GetLawTable(Law law)
    {

        string table = "";

        table += "<h1>" + law.Description +"</h1>";

        table += "<table border='1'>";
        
        

        table += "<tr>";

        table += "<th>Name</th><th>Opinion</th><th>Reason</th>";



        table += "</tr>";


        foreach (Member m in Member.GetCouncilMembers())
        {
            table += "<tr>";
            
            table += "<td><a href='Votes.aspx?Member=" + m.ReichCode  +"'>" + m.Name + "</a></td>";
            Vote vote = Vote.GetVote(m.ReichCode, law.ID);
            if (vote != null)
            {
                table += "<td>" + vote.VoteType + "</td>";

                table += "<td>" + (string.IsNullOrEmpty(vote.VoteReason) ? "No Reason Given" : vote.VoteReason) + "</td>";
            } else
            {
                table += "<td>Hasn't Voted</td>";
            }

            
            table += "</tr>";
        }

        table += "</table>";
        return table;
    }
}