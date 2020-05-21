<%@ Page Title="Categorias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebApplication.ForzaUltra.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <div class="row">
        <div class="container">
            <div class="col-lg-12">
                <div class="row">
                    <div class="col-lg-6">
                        <h2>Administración de Categorias</h2>                        
                    </div>
                    <div class="col-lg-6">
                        <a class="btn btn-sm btn-danger pull-right" id="btnAddCategory" data-toggle="modal" data-target="#basicExampleModal"><i class="fa fa-plus-circle"></i>&nbsp;Nuevo</a>
                    </div>
                </div>
                <hr />
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
                    <button type="button" class="btn btn-sm btn-outline-primary" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnSave" class="btn btn-sm btn-outline-danger">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdnCategoryIdentifier" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCurrentPage" Value="" ClientIDMode="Static" />
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/Admin/Categories.js") %>'></script>
</asp:Content>
