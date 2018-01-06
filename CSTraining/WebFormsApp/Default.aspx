<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsApp._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>ASP.NET Web Forms.</h1>              
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>We suggest the following:</h3>
    <ol class="round">
        <li class="one">
            <h4><a runat="server" href="~/Views/Supplier/Index.aspx">Suppliers</a></h4>
        </li>
        <li class="two">
            <h4><a runat="server" href="~/Views/Product/Index.aspx">Products</a></h4>
        </li>
        <li class="three">
            <h4><a runat="server" href="~/Views/Order/Index.aspx">Orders</a></h4>
        </li>
        <li class="four">
            <h4><a runat="server" href="~/Views/Customer/Index.aspx">Customers</a></h4>
        </li>
    </ol>
</asp:Content>
