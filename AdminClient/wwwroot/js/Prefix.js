$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    $('#prefixcount').DataTable({
        "ajax": { url: '/Prefix/GetAll' },
        "columns": [
            { data: 'name' },
            { data: 'lastreg' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-secondary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/Prefix/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                   <div class="dropdown-divider"></div>
                                   <a class="dropdown-item" href="/Prefix/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                   <div class="dropdown-divider"></div>
                                   <a class="dropdown-item" href="/Prefix/professionrequirements?id=${data}"><i class="bi bi-file-fill"></i> Requirements</a>
                                </div>
                              </div>`
                }
            }

        ]
    });
}