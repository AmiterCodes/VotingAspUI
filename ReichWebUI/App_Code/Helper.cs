using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public static string GetTable(object[] t)
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
            table += "</tr>";
        }

        table += "</table>";
        return table;
    }

}