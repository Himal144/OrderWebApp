var dataTable;
$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/admin/company/getall" },
        "columns": [
            { data: "name", "width": "15%" },
            { data: "streetAddress", "width": "25%" },
            { data: "city", "width": "15%" },
            { data: "state", "width": "15%" },
            { data: "phoneNumber", "width": "15%" },
           
            {
                data: "id", "render": function (data) {

                    return `<div class="btn-group w-75 text-center">
                                    <a href="/admin/company/upsert?id=${data}" class="btn btn-secondary mx-1"><i class="bi bi-pencil-square"></i> Edit </a>
                                            <a onclick=Delete('/admin/company/delete/${data}') class="btn btn-danger mx-1"><i class="bi bi-trash3"></i>Delete </a>
                        </div>`
                }
            }
        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

