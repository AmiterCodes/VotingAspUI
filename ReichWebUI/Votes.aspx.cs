using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ReichBL;

public partial class Votes : System.Web.UI.Page
{
    public string table = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Law law = new Law(int.Parse(Request.QueryString["Law"]));
        table =Helper.GetTable(law.Votes.ToArray());   
    }
}