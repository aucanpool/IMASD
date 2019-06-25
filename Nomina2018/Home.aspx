<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Nomina2018.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Empleados
                </h5>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                        <asp:GridView ID="employeesGridView" CssClass="table table-striped" runat="server" AllowCustomPaging="true" AllowPaging="true" 
                            OnPageIndexChanging="gridView_PageIndexChanging" PagerStyle-CssClass="pagingDiv" AutoGenerateColumns="false" 
                            OnRowCommand="gridView_RowCommand" OnRowEditing="gridView_RowEditing" OnRowDeleting="gridView_RowDeleting">
                            
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="JobNumber" HeaderText="Numero de trabajador" />
                                <asp:BoundField DataField="FirstName" HeaderText="Nombres" />
                                <asp:BoundField DataField="LastName" HeaderText="Apellidos" />
                                <asp:BoundField DataField="Address" HeaderText="Dirección" />
                                <asp:BoundField DataField="Telefone" HeaderText="Telefono" />
                                <asp:BoundField DataField="Gender" HeaderText="Genero" />
                                <asp:BoundField DataField="HireDate" HeaderText="Fecha de contratación" />
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%#Eval("Active") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Departament.Name" HeaderText="Departamento" />
                                <asp:BoundField DataField="SalaryTabulator.Key" HeaderText="Departamento" />
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <a id="editDocumentType" data-toggle="tooltip" data-placement="left" title="Editar" aria-label="Left Align"  class="buttonEditListInst btn btn-default btn-sm btn-size-table"><i class="fa fa-pencil fa-lg"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton  ID="deleteImageButton" runat="server" CommandName="delete" CommandArgument='<%#Eval("Id") %>' ImageUrl="Image/delete-icon.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    <asp:Button ID="ClientButton" runat="server" Text="Launch Modal Popup (Client)" />
    <asp:Panel ID="ModalPanel" runat="server" Width="500px">
 ASP.NET AJAX is a free framework for quickly creating a new generation of more 
 efficient, more interactive and highly-personalized Web experiences that work 
 across all the most popular browsers.<br />
 <asp:Button ID="OKButton" runat="server" Text="Close" />
</asp:Panel>
</asp:Content>
