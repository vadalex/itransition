<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebFormsApp.Views.Supplier.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h2>Index</h2>
 <p>
    <asp:HyperLink ID="CreateSupplierLink" NavigateUrl="~/Views/Supplier/Index.aspx" runat="server">Create New</asp:HyperLink> 
</p>
<table>
    <tr>
        <th>
            Name
        </th>
        <th>
            Phone
        </th>
        <th>
            Details
        </th>        
        <th></th>
    </tr>

<% foreach (var item in Suppliers)  {  %>
    <tr>
        <td>
            <%=item.Name %>
        </td>
        <td>
            <%=item.Phone%>
        </td>
        <td>
            <%=item.Details %>
        </td>        
        <td>            
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?SupplierId=" + item.SupplierID) %>">Edit</a> | 
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?SupplierId=" + item.SupplierID) %>">Details</a> | 
            <a href="<%= ResolveUrl("~/Views/Customer/Index.aspx?SupplierId=" + item.SupplierID) %>">Delete</a>
        </td>
    </tr>
<% } %>
</table>
</asp:Content>
