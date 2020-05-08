<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Store.aspx.cs" Inherits="WebApplication.ForzaUltra.Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../Content/ForzaUltra/Shop.css" />
    <div class="jumbotron jumbotron-fluid shopBackGround">
        <img src="../Images/ForzaUltra/Site/fu_logo.png" class="forzaUltraLogo" />
    </div>
    <%--<div class="embed-responsive embed-responsive-16by9">
        <iframe class="embed-responsive-item" src="https://www.youtube.com/embed/XxXZGDWUhQQ" allowfullscreen></iframe>
    </div>--%>
    <div id="divCategoryList">

    </div>
    <asp:HiddenField runat="server" ID="hfdMainPageProductCount" Value="0" />
</asp:Content>
