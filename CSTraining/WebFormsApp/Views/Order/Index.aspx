<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebFormsApp.Views.Order.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Index</h2>

<p>
    <asp:HyperLink ID="CreateOrderLink" NavigateUrl="~/Views/Order/Index.aspx" runat="server">Create New</asp:HyperLink> 
</p>
<table>
    <tr>
        <th>
            Customer
        </th>
        <th>
            Status
        </th>
        <th>
            Created date
        </th>
        <th>
            Details
        </th>
        <th></th>
    </tr>

<% foreach (var item in Orders) {  %>
    <tr>
        <td>
            <%=item.Customer.Name %>
        </td>
        <td>
            <%=item.Status %>
        </td>
        <td>
            <%=item.CreatedDate.ToShortDateString() %>
        </td>
        <td>
            <%=item.Details %>
        </td>
        <td>            
            <a href="<%= ResolveUrl("~/Views/Order/Index.aspx?OrderId=" + item.OrderID) %>">Edit</a> | 
            <a href="<%= ResolveUrl("~/Views/Order/Index.aspx?OrderId=" + item.OrderID) %>">Details</a> | 
            <a href="<%= ResolveUrl("~/Views/Order/Index.aspx?OrderId=" + item.OrderID) %>">Delete</a>
        </td>
    </tr>
<% } %>
</table>

</asp:Content>
