let serviceUri = '';

$(document).ready(function () {

    serviceUri = $('#hdnCurrentPage').val() + '.aspx/';

    $('#btnAddCategory').click(function () {
        $('#hdnCategoryIdentifier').val('0');
        $('#txtCategoryName').val('');
        $('#txtCategoryDescription').val('');
        $('#spnModalTitle').text('Agregar nueva categoría');
        $('#divModalCategory').modal('show');
    });

    loadCategoryList();
    //alert('test');
});

$('#btnSave').click(function () {
    console.log('merge');
    mergeCategoryItem();
});

function loadCategoryList() {
    $.ajax({
        type: "POST",
        url: serviceUri + 'CategoryGetList',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var data = response.d;
            if (data.Success && data.Result.length > 0) {

                var categoryTable = $('#tblCategories tbody');
                categoryTable.empty();

                //carga tabla de categorias
                var tableContent = '';
                $.each(data.Result, function (i, item) {
                    tableContent += "<tr>";
                    tableContent += "   <th scope='row'>" + item.Identifier + "</th>";
                    tableContent += "   <td>" + item.Name + "</td>";
                    tableContent += "   <td>" + item.Description + "</td>";
                    tableContent += "   <td>";
                    tableContent += "	    <span data-placement='top' data-toggle='tooltip' title='Editar'><button id='btnEdit' class='btn btn-sm btn-default btn-xs edit' data-title='Editar' data-toggle='modal' data-target='#edit' data-category-id='" + item.Identifier + "'><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button></span>";
                    tableContent += "	    <span data-placement='top' data-toggle='tooltip' title='Eliminar'><button id='btnDelete' class='btn btn-sm btn-default btn-xs delete' data-title='Eliminar' data-target='#delete' data-category-id='" + item.Identifier + "'><span><i class='fa fa-trash-o' aria-hidden='true'></i></span></button></span>";
                    tableContent += "   </td>";
                    tableContent += "</tr>";
                });
                categoryTable.append(tableContent);

                //modal de edicion
                $('.edit').off("click").on("click", function (e) {
                    var categoryIdentifier = $(this).data('category-id');
                    loadCategoryDetail(categoryIdentifier);
                });

                $('.delete').off('click').on('click', function (e) {
                    var categoryIdentifier = $(this).data('category-id');
                    changeStatusCategoryItem(categoryIdentifier);
                });

            }
            else {
                alert("No hay categorias dadas de alta");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryList]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
};

function loadCategoryDetail(categoryIdentifier) {

    var filter = {
        'categoryIdentifier': parseInt(categoryIdentifier)
    };

    $.ajax({
        type: "POST",
        url: serviceUri + 'CategoryGetItem',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(filter),
        success: function (response) {
            var data = response.d;
            if (data.Success && data.Result) {
                console.log('categoryGetItem: ', data.Result);
                $('#hdnCategoryIdentifier').val(categoryIdentifier);
                $('#txtCategoryName').val(data.Result.Name);
                $('#txtCategoryDescription').val(data.Result.Description);
                $('#spnModalTitle').text('Editar');
                $('#divModalCategory').modal('show');
            }
            else {
                alert("No fue posible obtener el detalle de la categoria");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryItem]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
};

function mergeCategoryItem() {

    var categoryItem = new Object();
    categoryItem.Identifier = parseInt($('#hdnCategoryIdentifier').val().trim());
    categoryItem.Name = $('#txtCategoryName').val().trim();
    categoryItem.Description = $('#txtCategoryDescription').val().trim();

    $.ajax({
        type: "POST",
        url: serviceUri + 'CategoryMerge',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ 'category': categoryItem }),
        success: function (response) {
            var data = response.d;
            if (data.Success) {
                loadCategoryList();

                $('#hdnCategoryIdentifier').val('0');
                $('#txtCategoryName').val('');
                $('#txtCategoryDescription').val('');
                $('#spnModalTitle').text('Editar');
                $('#divModalCategory').modal('hide');
                alert('Informacion guardada correctamente', 'success');
            }
            else {
                alert("No fue posible obtener el detalle de la categoria");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryItem]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });

};

function changeStatusCategoryItem(categoryIdentifier) {
    var filter = {
        'categoryIdentifier': parseInt(categoryIdentifier)
    };

    $.ajax({
        type: "POST",
        url: serviceUri + 'CategoryChangeStatus',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(filter),
        success: function (response) {
            var data = response.d;
            if (data.Success) {
                alert('Se ha eliminado el registro correctamente');
                loadCategoryList();
            }
            else {
                alert("No fue posible eliminar el registro");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryChangeStatus]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
}