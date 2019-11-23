using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ReichBL;

public partial class Login : System.Web.UI.Page
{
    public string error = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        error = "";
        var form = Request.Form;
        if(form["login_sub"] != null)
        {
            try
            {
                string name = (string)Session.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents.Contents["Name"];
                
                Member member = new Member(form["name"], form["password"]);
                Session["code"] = member.ReichCode;
                Session["Name"] = member.Name;
                Session["Member"] = member.IsMember;
            } catch(Exception ex)
            {
                error = ex.Message;
            }
        }
    }
}