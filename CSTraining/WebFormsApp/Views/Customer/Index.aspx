<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebFormsApp.Views.Customer.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
       <h2>Index</h2>

<p>
    <asp:HyperLink ID="CreateCustomerLink" NavigateUrl="~/Views/Customer/Index.aspx" runat="server">Create New</asp:HyperLink> 
</p>
<table>
    <tr>
        <th>
            Name
        </th>
        <th>
            Address
        </th>
        <th>
            Phone
        </th>        
        <th></th>
    </tr>

<% foreach (var item in Customers) {  %>
    <tr>
        <td>
            <%=item.Name %>
        </td>
        <td>
            <%=item.Address %>
        </td>
        <td>
            <%=item.Phone %>
        </td>        
        <td>            
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?CustomerId=" + item.CustomerID) %>">Edit</a> | 
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?CustomerId=" + item.CustomerID) %>">Details</a> | 
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?CustomerId=" + item.CustomerID) %>">Delete</a>
        </td>
    </tr>
<% } %>
</table>

</asp:Content>
