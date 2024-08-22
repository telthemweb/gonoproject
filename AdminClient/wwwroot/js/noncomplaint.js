$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/NonCompliant/GetAll' },
        "columns": [
            { data: 'title' },
            { data: 'registrationNumber' },
            { data: 'fullname' },
            { data: 'email' },
            { data: 'phone' },
            { data: 'professionName' },
            { data: 'RegisterName' },
            { data: 'accounttype' },
            { data: 'provincename' },
            { data: 'cityname' },
            { data: 'gender' },
            {
                data: 'status',
                "render": function (data) {
                    var badgeClass = data === "NON-COMPLIANT" ? "badge-danger" : "badge-success";
                    return `<span class="badge ${badgeClass} p-2">${data}</span>`;
                }
            },
            { data: 'status' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                                                Action
                                                                </button>
                                                                <div class="dropdown-menu">
                                                                <a class="dropdown-item" href="/CustomerApplication/PractitionerApplicationstList?id=${data}"><i class="bi bi-cash-coin"></i> Application History</a>
                                                                   <div class="dropdown-divider"></div>
                                                                  <a class="dropdown-item" href="/Customer/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                                                  <div class="dropdown-divider"></div>
                                                                  <a class="dropdown-item" href="/Customer/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                                                  <div class="dropdown-divider"></div>
                                                                  <a class="dropdown-item" href="/Customer/Show?id=${data}"><i class="bi bi-person-bounding-box"></i> View Profile</a>
                                                                </div>
                                                              </div>`;
                }
            }
        ]
    });
}