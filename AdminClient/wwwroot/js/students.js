$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
  
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/Student/GetAll' },
        "columns": [
            { data: 'registrationNumber' },
            { data: 'name' },
            { data: 'surname' },
            { data: 'dateCreated' },
            
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/Student/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Student/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Student/Show?id=${data}"><i class="bi bi-person-bounding-box"></i> View Profile</a>
                                </div>
                              </div>`
                }
            }

        ]
    });
}