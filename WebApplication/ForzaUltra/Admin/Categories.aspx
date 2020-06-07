<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebApplication.ForzaUltra.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="container">
                <div class="col-lg-12">
                    <h4><strong>Administración de Categorias</strong></h4>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-lg-4">
                <div class="input-group form-sm">
                    <input class="form-control my-0 py-1 red-border" type="search" id="wordFilter" placeholder="Producto / Categoría" aria-label="Search">
                    <div class="input-group-append">
                        <a class="input-group-text  lighten-3" id="btnSearch"><i class="fa fa-search"
                            aria-hidden="true"></i></a>
                    </div>
                </div>
            </div>
            <div class="col-lg-8">
                <a class="btn btn-sm btn-danger pull-right" id="btnAddCategory" data-toggle="modal" data-target="#basicExampleModal"><i class="fa fa-plus-circle"></i>&nbsp;Nuevo</a>
            </div>
        </div>
        <div class="row" id="dvgrid" runat="server">
            <div class="col-lg-12" style="min-height:55%!important">
                <div class="table-responsive text-nowrap" >
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
        <div class="row">
            <div class="container">
                <div class="col-lg-12 text-center">
                    <div class="pagination">
                        <a href="#" class="first" data-action="first">&laquo;</a>
                        <a href="#" class="previous" data-action="previous">&lsaquo;</a>
                        <input type="text" readonly="readonly" data-max-page="40" />
                        <a href="#" class="next" data-action="next">&rsaquo;</a>
                        <a href="#" class="last" data-action="last">&raquo;</a>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <%--Modal de alta y edicion--%>
        <div class="modal fade" id="divModalCategory" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">
                            <span id="spnModalTitle"></span>
                        </h4>
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    </div>
                    <div class="modal-body" style="padding: 10px 15px;">
                        <div class="form-group">
                            <label for="txtCategoryName">Categor&iacute;a</label>
                            <input type="text" id="txtCategoryName" class="form-control" />
                        </div>
                        <div class="form-group">
                            <label for="txtCategoryDescription">Descripci&oacute;n</label>
                            <input type="text" id="txtCategoryDescription" class="form-control" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-sm btn-outline-primary" data-dismiss="modal">Cancelar</button>
                        <button type="button" id="btnSave" class="btn btn-sm btn-outline-danger">Guardar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnCategoryIdentifier" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCurrentPage" Value="" ClientIDMode="Static" />
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/Admin/Categories.js") %>'></script>
</asp:Content>
