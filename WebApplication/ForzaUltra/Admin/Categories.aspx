<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebApplication.ForzaUltra.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div class="col-lg-12">
                    <h2>Administración de Categorias</h2>
                    <hr />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <br />
    </div>
    <div class="row" id="dvgrid" runat="server">
        <div class="container">
            <div class="col-lg-12">
                <div class="col-lg-12">
                    <div class="table-responsive text-nowrap">
                        <table id="tblCategories" class="table table-bordered table-striped text-center">
                            <thead class="thead thead-dark">
                                <tr>
                                    <th scope="col">#</th>
                                    <th scope="col">Categor&iacute;a</th>
                                    <th scope="col">Descripci&oacute;n</th>
                                    <th scope="col">&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <%-- <div class="col-lg-8 col-md-offset-2 text-right">
                <input type="button" id="btnAddCategory" value="Agregar nueva categoría" class="btn btn-danger" />
            </div>--%>
        </div>
    </div>

    <%--Modal de alta y edicion--%>
    <div class="modal fade" id="divModalCategory" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">
                        <span id="spnModalTitle"></span>
                    </h4>
                </div>
                <div class="modal-body" style="padding: 10px 15px;">
                    <form role="form">
                        <div class="form-group">
                            <label for="txtCategoryName">Categor&iacute;a</label>
                            <input type="text" id="txtCategoryName" class="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtCategoryDescription">Descripci&oacute;n</label>
                            <input type="text" id="txtCategoryDescription" class="form-control" />
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnSave" class="btn btn-danger">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdnCategoryIdentifier" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCurrentPage" Value="" ClientIDMode="Static" />
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/Admin/Categories.js") %>'></script>
</asp:Content>
