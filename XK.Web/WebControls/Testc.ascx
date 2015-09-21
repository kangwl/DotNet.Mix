<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Testc.ascx.cs" Inherits="XK.Web.WebControls.Testc" %>
 
    <asp:Repeater runat="server" ID="rptList">
        <ItemTemplate>
            <li class="text-danger"><%#Eval("ID") %></li>
        </ItemTemplate>
    </asp:Repeater> 
 
 
 