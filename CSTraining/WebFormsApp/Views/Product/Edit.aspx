<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="WebFormsApp.Views.Product.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit</h2>

    <div class="form-horizontal">
        <h4>Product</h4>
        <hr />       

        <div class="form-group">
            <label class = "control-label col-md-2">Name</label>
            <div class="col-md-10">
                <asp:TextBox ID="ProductName" CssClass="form-control" runat="server" AutoPostBack="true">Name</asp:TextBox>                
            </div>
        </div>

        <div class="form-group">
            <label class = "control-label col-md-2">Price</label>            
            <div class="col-md-10">
                <asp:TextBox ID="ProductPrice" CssClass="form-control" runat="server" AutoPostBack="true">Price</asp:TextBox>
            </div>
        </div>

        <div class="form-group">
            <label class = "control-label col-md-2">Category</label>
            <div class="col-md-10">
                <asp:TextBox ID="ProductCategory" CssClass="form-control" runat="server" AutoPostBack="true">Category</asp:TextBox>                
            </div>
        </div>
        <div class="form-group">
            <label class = "control-label col-md-2">Supplier</label>
            <div class="col-md-10">
                <asp:DropDownList ID="suppliers" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>                
            </div>
        </div>
        <br />
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button Text="Save" CssClass="btn btn-default" runat="server" ID="SaveButton" OnClick="SaveButton_Click" />                
            </div>
        </div>
    </div>

<div>
    <a href="Index.aspx">Back to list</a>
</div>
</asp:Content>
