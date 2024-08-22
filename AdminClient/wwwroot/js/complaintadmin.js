$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        dom: 'Bfrtip',
        serverSide: true,
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "ajax": { url: '/Welcome/GetAll' },
        "columns": [
            { data: 'title' },
            { data: 'registrationNumber' },
            { data: 'fullname' },
            { data: 'gender' },
            { data: 'professionName' },
            { data: 'registerName' },
            { data: 'accounttype' },
            { data: 'provincename' },
            { data: 'cityname' },
            { data: 'id' },
            {
                data: 'id',
                "render": function (data) {
                    console.log(data); 
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/Customer/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Customer/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Customer/Show?id=${data}"><i class="bi bi-person-bounding-box"></i> View Profile</a>
                                </div>
                              </div>`
                }
            }
          

        ]
    });
}
