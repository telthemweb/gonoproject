$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/RenewalTire/GetAll' },
        "columns": [
            { data: 'name' },
            { data: 'currency.name' },
            { data: 'amount' },
            {
                data: 'professions',
                "render": function (data) {  
                    var count = data.length
                    return `Professions(${count})`
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-secondary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/RenewalTire/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/RenewalTire/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                   <a class="dropdown-item" href="/RenewalTireProfession/Index?id=${data}"><i class="bi bi-plus"></i> Profession</a>
                            
                                  </div>
                              </div>`
                }
            }

        ]
    });
}