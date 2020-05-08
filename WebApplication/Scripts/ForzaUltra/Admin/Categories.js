
let serviceUri = '';
$(document).ready(function () {

    serviceUri = $('#hdnCurrentPage').val() + '.aspx/';
    console.log(serviceUri);

    loadCategoryList();

});

function loadCategoryList() {
    $.ajax({
        type: "POST",
        url: serviceUri + 'LoadCategoryList',
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
                    tableContent += "	    <p data-placement='top' data-toggle='tooltip' title='Editar'><button class='btn btn-default btn-xs' data-title='Editar' data-toggle='modal' data-target='#edit' data-category-id='" + item.Identifier + "'><span class='glyphicon glyphicon-pencil'></span></button></p>";
                    tableContent += "	    <p data-placement='top' data-toggle='tooltip' title='Eliminar'><button class='btn btn-default btn-xs' data-title='Eliminar' data-target='#delete' data-category-id='" + item.Identifier + "'><span class='glyphicon glyphicon-trash'></span></button></p>";
                    tableContent += "   </td>";
                    tableContent += "</tr>";
                });

                //modal de edicion
                $('#divModalCategory').off("show.bs.modal").on("show.bs.modal", function (e) {
                    var categoryIdentifier = $(e.relatedTarget).data('category-id');
                    console.log('categoryId: ', categoryIdentifier);
                });

                categoryTable.append(tableContent);
            }
            else {
                alert("No hay categorias dadas de alta");
            }
        },
        failure: function (xhr, textStatus, errorThrown) {
            alert("Fail[LoadCategoryList]" + xhr + " " + textStatus + " " + errorThrown);
        }
    });
}