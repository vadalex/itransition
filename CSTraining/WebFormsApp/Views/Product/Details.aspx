<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="WebFormsApp.Views.Product.Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Details</h2>

<div>
    <h4>Product</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            Name
        </dt>

        <dd>
            <%= Item.Supplier.Name %>
        </dd>

        <dt>
            Name
        </dt>

        <dd>
            <%= Item.Name%>
        </dd>

        <dt>
            Price
        </dt>

        <dd>
            <%= Item.Price%>
        </dd>

        <dt>
            Category
        </dt>

        <dd>
            <%= Item.Category%>
        </dd>

        <dt>
            Orders
        </dt>

        <dd>
            <table>
                <tbody>
                    <%foreach (var order in Item.Orders) { %>                                        
                        <tr><td><%=order.OrderID + " " + order.Details %></td></tr>
                    <% } %>
                </tbody>
            </table>            
        </dd>

    </dl>
</div>
<p>
    <a href="<%= this.ResolveUrl("~/Views/Product/Edit.aspx?productId=" + Item.ProductID.ToString()) %>">Edit</a>
    <a href="Index.aspx">Back to list</a>
</p>
</asp:Content>
