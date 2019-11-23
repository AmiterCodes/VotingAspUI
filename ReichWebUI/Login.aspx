<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%=error %>
    <form action="Login.aspx" method="post">
        <input type="text" name="name" value="" placeholder="Name" />
        <input type="password" name="password" value="" placeholder="Password"/>
        <input type="submit" name="login_sub" value="Submit!" />
    </form>
</asp:Content>

