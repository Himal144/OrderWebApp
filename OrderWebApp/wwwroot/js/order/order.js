
var dataTable;
$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/admin/order/getall" },
        "columns": [
            { data: "id", "width": "15%" },
            { data: "name", "width": "15%" },
            { data: "applicationUser.email", "width": "15%" },
            { data: "phoneNumber", "width": "15%" },
            { data: "orderStatus", "width": "15%" }, // Add this
            { data: "orderTotal", "width": "15%" },
            
            {
                data: "id", "render": function (data) {

                    return `<div class="btn-group  text-center">
                                    <a href="/admin/order/details?orderId=${data}" class="btn btn-secondary mx-1"><i class="bi bi-pencil-square"></i> </a>
                                          
                        </div>`
                }
            }
        ]
    })
}




