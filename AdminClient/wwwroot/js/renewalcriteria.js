$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/RenewalCriteria/GetAll' },
        "columns": [
            { data: 'renewalCategory.name' },
            { data: 'employmentStatus.name' },
            { data: 'employmentLocation.name' },
            {
                data: 'certificateRequest',
                "render": function (data) {
                    return data>0 ?"Requires Certificate" : "Does not require certificate"
                }
            },
            { data: 'percentage' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/RenewalCriteria/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/RenewalCriteria/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                </div>
                              </div>`
                }
            }

        ]
    });
}