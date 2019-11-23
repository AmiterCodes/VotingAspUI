using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using ReichBL;

public partial class _Default : System.Web.UI.Page
{
    public string connString = "";
    public string table = "";

    
    public static string GetTable(Law[] t)
    {

        string table = "<h1>Laws</h1>";


        if (t.Length == 0) return table + "No Laws Added Yet";



        table += "<table border='1'>";

        Type type = t[0].GetType();

        PropertyInfo[] info = type.GetProperties();

        table += "<tr>";

        foreach (var inf in info)
        {
            if (inf.PropertyType.GetInterface("ICollection") == null)
                table += "<th>" + inf.Name + "</th>";
        }

        table += "<th> Votes </th>";


        table += "</tr>";


        foreach (var v in t)
        {
            table += "<tr>";
            foreach (var inf in info)
            {
                if (inf.PropertyType.GetInterface("ICollection") == null)
                {
                    table += "<td>" + inf.GetValue(v) + "</td>";
                }
            }

            table += "<td><a href='./Votes.aspx?Law=" + v.ID + "'>Votes</a></td>";
            table += "</tr>";
        }

        table += "</table>";
        return table;
    }

    public static string GetTable(Member[] t)
    {

        string table = "";

        if (t.Length == 0) return "No Members yet.";


        table += "<h1>Council Members</h1>";

        table += "<table border='1'>";

        Type type = t[0].GetType();


        table += "<tr>";

        table += "<th>Name</th><th>Votes</th>";



        table += "</tr>";


        foreach (Member m in Member.GetCouncilMembers())
        {
            table += "<tr>";

            table += "<td>" + m.Name + "</td>";
            table += "<td><a href='Votes.aspx?Member=" + m.ReichCode + "'>Votes</a></td>";

            table += "</tr>";
        }

        table += "</table>";
        return table;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string path = HttpContext.Current.Server.MapPath("App_Data/Reichabase.accdb");
        connString = String.Format(@"Provider={0};Data Source={1};", "Microsoft.ACE.OLEDB.12.0", path);


        IEnumerable<Law> laws = Law.GetLaws();

        if (Request.Form["search"] != null)
        {
            laws = (IEnumerable<Law>)laws.Where(law => law.Description.Contains((string)Request.Form["search"]));
        }


        table = GetTable(laws.ToArray());
        table += GetTable(Member.GetCouncilMembers().ToArray());
    }
}