<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="XK.Web.xk.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .search_key{color: red}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                搜索
            </div>
        </div>
        <div class="panel-body">
            <div class="well form-inline">
                <asp:TextBox runat="server" ID="txt_KeyWord" CssClass="form-control"></asp:TextBox>
                <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="搜索" OnClick="btnSearch_Click"/>
            </div>
            <asp:Repeater runat="server" ID="rpt_SearchResult">
                <ItemTemplate>
                    <div class="well">
                        <p>
                            <%#Eval("ID") %>. <%#Eval("Title") %>
                        </p>
                        <p>
                            <%#Eval("Content") %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
