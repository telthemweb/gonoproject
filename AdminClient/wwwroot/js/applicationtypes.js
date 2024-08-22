$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/ApplicationType/GetAll' },
        "columns": [
            { data: 'name' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/ApplicationType/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/ApplicationType/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                </div>
                              </div>`
                }
            }

        ]
    });
}