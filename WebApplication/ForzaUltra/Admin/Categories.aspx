﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="WebApplication.ForzaUltra.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <br />
    <h2>Administración de Categorias</h2>
    <hr />
    <br />
    <div class="row" id="dvgrid" runat="server">
        <div class="col-lg-8 col-md-offset-2">
            <table id="tblCategories" class="table table-bordered table-responsive-md table-striped text-center">
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
        <div class="col-lg-8 col-md-offset-2 text-right">
            <input type="button" ID="btnAddCategory" value="Agregar nueva categoría" class="btn btn-danger" />
        </div>
    </div>

    <%--Modal de alta y edicion--%>
    <div class="modal" id="divModalCategory">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title">
                        <span id="spnModalTitle"></span>
                    </h4>
                </div>
                <div class="modal-body">
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnSave" class="btn btn-danger"></button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdnCategoryIdentifier" Value="0" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnCurrentPage" Value="" ClientIDMode="Static" />
    <script type="text/javascript" src='<%:ResolveUrl("~/Scripts/ForzaUltra/Admin/Categories.js") %>'></script>
</asp:Content>