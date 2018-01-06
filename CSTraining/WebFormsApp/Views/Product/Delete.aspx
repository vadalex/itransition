<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="WebFormsApp.Views.Product.Delete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
        
      <br />
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button Text="Delete" CssClass="btn btn-default" runat="server" ID="DeleteButton" OnClick="DeleteButton_Click" />                
            </div>
        </div>
    </div>

<div>
    <a href="Index.aspx">Back to list</a>
</div>
    
</div>

</asp:Content>
