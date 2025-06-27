<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Ecommercegq.Admin.ProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
    window.onload = function () {
        var seconds = 5;
        setTimeout(function () {
            document.getElementById("<%=lblMsg.ClientID %>").style.display = "none";
        }, seconds * 1000);
    };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="mb-4">
        <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>

     <div class="col-sm-12 col-md-12">
     <div class="card">
         <div class="card-body">
             <%--<h4 class="card-title">Product List</h4>--%>
             <a href="Product.aspx" class="card-title text-primary m-0 d-flex alert-items-end justify-content-end">
                 <i class="fas fa-plus-circle">Add New</i>
             </a>
             <hr />
             <div>
                 <asp:Repeater ID="rProductList" runat="server" OnItemCommand="rProductList_ItemCommand">
                     <HeaderTemplate>
                         <table class="table data-table-export table-hover nowrap">
                             <thead>
                                 <tr>
                                     <th class="table-plus">Name</th>
                                     <th>Image</th>
                                     <th>Price</th>
                                     <th>Qty</th>
                                     <th>Sold</th>
                                     <th>Category</th>
                                     <th>SCategory</th>
                                     <th>IsActive</th>                                     
                                     <th>CreatedDate</th>
                                     <th class="datatable-nosort">Action</th>
                                 </tr>
                             </thead>
                             <tbody>
                     </HeaderTemplate>
                     <ItemTemplate>
                         <tr>
                             <td class="table-plus"><%# Eval("ProductName") %> </td>
                             <td>
                                 <img width="40" src="<%# Ecommercegq.Utils.getImageUrl( Eval("ImageUrl")) %>" alt="image" />
                             </td>
                             <td><%# Eval("Price") %></td>
                             <td>
                                 <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                             </td>
                             <td><%# Eval("Sold") %></td>
                             <td><%# Eval("CategoryName") %></td>
                             <td><%# Eval("SubCategoryName") %></td>
                             <td>
                                 <asp:Label ID="lblIsActive" runat="server"
                                     Text='<%# Convert.ToBoolean(Eval("IsActive")) ? "Active" : "In-Active" %>'
                                     CssClass='<%# Convert.ToBoolean(Eval("IsActive")) ? "badge badge-success"
                                         : "badge badge-danger" %>'>
                                 </asp:Label>
                             </td>
                             <td><%# Eval("CreatedDate") %></td>
                             <td>
                                 <asp:LinkButton ID="lblEdit" Text="Edit" runat="server" CssClass="badge badge-primary"
                                     CommandArgument='<%# Eval("ProductId") %>' CommandName="edit" CausesValidation="false">
                                     <i class="fas fa-edit"></i>
                                 </asp:LinkButton>
                                 <asp:LinkButton ID="lblDelete" Text="Delete" runat="server" CssClass="badge badge-danger">
                                   CommandArgument='<%# Eval("ProductId") %>' CommandName="delete" CausesValidation="false"
                                     OnClientClick="return confirm('Do you want to delete this product?');">

                                     <i class="fas fa-trash-alt"></i>
                                 </asp:LinkButton>
                             </td>
                         </tr>
                     </ItemTemplate>
                     <FooterTemplate>
                         </tbody>
                         </table>
                     </FooterTemplate>
                 </asp:Repeater>
             </div>
         </div>
     </div>
 </div>
</asp:Content>
