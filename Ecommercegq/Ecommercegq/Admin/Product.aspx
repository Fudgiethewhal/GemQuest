<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Ecommercegq.Admin.Product" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-12">
    <div class="card">
        <div class="card-body">
            <h4 class="card-title">Product</h4>
            <hr />

            <div class="form-body">
                
                <div class="row">
                    <div class="col-md-6">
                        <label>Product Name</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" placeholder="Enter Product Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtProductName"
                                ErrorMessage="Product Name is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                        <div class="col-md-3">
                            <label>Category</label>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true">
                                    <asp:ListItem Value="0">Select Category</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="frvCategory" runat="server" ForeColor="Red" Font-Size="Small"
                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlCategory" InitialValue="0"
                                    ErrorMessage="Category is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    <div class="col-md-3">
                        <label>Category</label>
                        <div class="form-group">
                            <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvSubCategory" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="ddlSubCategory"  
                                ErrorMessage="SubCategory is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label>Price</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter Product Price"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtPrice"
                                ErrorMessage="Product Price is required"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPrice" runat="server" ControlToValidate="txtPrice"
                                ValidationExpression="\d+(?:.\d{1,2})?" ErrorMessage="Product Price is invalid" ForeColor="Red"
                                Display="Dynamic" SetFocusOnError="true" Font-Size="Smaller">                                
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Color</label>
                        <div class="form-group">
                            <asp:ListBox ID="lboxColor" runat="server" CssClass="form-control" SelectionMode="Multiple"
                                ToolTip="Use CTRL key to select multiple items">
                                <asp:ListItem Value="1">Blue</asp:ListItem>
                                <asp:ListItem Value="2">Red</asp:ListItem>
                                <asp:ListItem Value="3">Pink</asp:ListItem>
                                <asp:ListItem Value="4">Purple</asp:ListItem>
                                <asp:ListItem Value="5">Brown</asp:ListItem>
                                <asp:ListItem Value="6">Gray</asp:ListItem>
                                <asp:ListItem Value="7">Green</asp:ListItem>
                                <asp:ListItem Value="8">Yellow</asp:ListItem>
                                <asp:ListItem Value="9">White</asp:ListItem>
                                <asp:ListItem Value="10">Black</asp:ListItem>
                            </asp:ListBox>
                            
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label>Size</label>
                        <div class="form-group">
                            <asp:ListBox ID="lboxSize" runat="server" CssClass="form-control" SelectionMode="Multiple"
                                ToolTip="Use CTRL key to select multiple items">
                                <asp:ListItem Value="1">XS</asp:ListItem>
                                <asp:ListItem Value="2">SM</asp:ListItem>
                                <asp:ListItem Value="3">M</asp:ListItem>
                                <asp:ListItem Value="4">L</asp:ListItem>
                                <asp:ListItem Value="5">XL</asp:ListItem>
                                <asp:ListItem Value="6">XXL</asp:ListItem>                                
                            </asp:ListBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label>Quantity</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" placeholder="Enter Product Quantity"
                                TextMode="Number"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtQuantity"
                                ErrorMessage="Product Quantity is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Company Name</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Enter Product's Company Name">                                
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtCompanyName"
                                ErrorMessage="Company Name is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <label>Short Description</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtShortDescription" runat="server" CssClass="form-control" placeholder="Enter Short Description"
                                TextMode="MultiLine">                                
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvShortDescription" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtShortDescription"
                                ErrorMessage="Short Description is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Long Description</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtLongDescription" runat="server" CssClass="form-control" placeholder="Enter Long Description"
                                TextMode="MultiLine"> 
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLongDescription" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtLongDescription"
                                ErrorMessage="Long Description is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Additional Description</label>
                        <div class="form-group">
                            <asp:TextBox ID="txtAdditionalDescription" runat="server" CssClass="form-control" placeholder="Enter Additional Description"
                                TextMode="MultiLine"> 
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvAdditionalDescription" runat="server" ForeColor="Red" Font-Size="Small"
                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtAdditionalDescription"
                                ErrorMessage="Additional Description is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

               <%-- <label>Category Image</label>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:FileUpload ID="fuCategoryImage" runat="server" CssClass="form-control"
                                onchange="ImagePreview(this);" />
                            <asp:HiddenField ID="hfCategoryId" runat="server" Value="0" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <asp:CheckBox ID="cbIsActive" runat="server" Text="&nbsp; IsActive" />
                        </div>
                    </div>
                </div>--%>

            <%--<div class="form-action pb-5">
                <div class="text-left">
                    <asp:Button ID="btnAddOrUpdate" runat="server" CssClass="btn btn-info" Text="Add" OnClick="btnAddOrUpdate_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-dark" Text="Reset" OnClick="btnClear_Click" />
                </div>
            </div>--%>

            <%--<div>
                <asp:Image ID="imagePreview" runat="server" CssClass="img-thumbnail" AlternateText="" />
            </div>--%>

        </div>
    </div>
</div>

    </div>

</asp:Content>
