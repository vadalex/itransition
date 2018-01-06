<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebFormsApp.Views.Product.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
<h2>Index</h2>

<p>
    <asp:HyperLink ID="CreateProductLink" NavigateUrl="~/Views/Product/Create.aspx" runat="server">Create New</asp:HyperLink> 
</p>
<table>
    <tr>
        <th>
            Name
        </th>
        <th>
            Supplier
        </th>
        <th>
            Price
        </th>
        <th>
            Category
        </th>
        <th></th>
    </tr>

<% foreach (var item in Products) {  %>
    <tr>
        <td>
            <%=item.Name %>
        </td>
        <td>
            <%=item.Supplier.Name %>
        </td>
        <td>
            <%=item.Price %>
        </td>
        <td>
            <%=item.Category %>
        </td>
        <td>            
            <a href="<%= ResolveUrl("~/Views/Product/Edit.aspx?productId=" + item.ProductID) %>">Edit</a> | 
            <a href="<%= ResolveUrl("~/Views/Product/Details.aspx?productId=" + item.ProductID) %>">Details</a> | 
            <a href="<%= ResolveUrl("~/Views/Product/Delete.aspx?productId=" + item.ProductID) %>">Delete</a>
        </td>
    </tr>
<% } %>
</table>

</asp:Content>
