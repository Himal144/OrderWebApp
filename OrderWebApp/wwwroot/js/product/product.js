
var dataTable;
$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
  
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/admin/product/getall" },
        "columns": [
            { data: "title", "width": "15%" },
            { data: "description", "width": "15%" },
            { data: "isbn", "width": "15%" },
            { data: "author", "width": "15%" },
            { data: "category.name", "width": "15%" }, // Add this
            { data: "listPrice", "width": "15%" },
            { data: "price", "width": "15%" }, // Add this
            { data: "price50", "width": "15%" }, // Add this
            { data: "price100", "width": "15%" }, 
            {
                data: "id", "render": function (data) {

                    return `<div class="btn-group w-75 text-center">
                                    <a href="/admin/product/upsert?id=${data}" class="btn btn-secondary mx-1"><i class="bi bi-pencil-square"></i> Edit </a>
                                            <a onclick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-1"><i class="bi bi-trash3"></i>Delete </a>
                        </div>`
            }}
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

