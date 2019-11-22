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


        if (t.Length == 0) return "";

        string table = "";
        table += "<table>";

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

            table += "<td><a src='./Votes.aspx?Law=" + v.ID + "'>Votes</a></td>";
            table += "</tr>";
        }

        table += "</table>";
        return table;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string path = HttpContext.Current.Server.MapPath("App_Data/Reichabase.accdb");
        connString = String.Format(@"Provider={0};Data Source={1};", "Microsoft.ACE.OLEDB.12.0", path);


        IEnumerable<Law> laws = Law.GetPassedLaws();

        if (Request.Form["search"] != null)
        {
            laws = (IEnumerable<Law>)laws.Where(law => law.Description.Contains((string)Request.Form["search"]));
        }


        table = GetTable(laws.ToArray());
    }
}