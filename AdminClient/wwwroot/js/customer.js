$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        dom: 'Bfrtip',

        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        "ajax": {
            url: '/Customer/GetAll',
            data: function (d) {
                d.prefix = $('#prefixFilter').val();
                d.profession = $('#professionFilter').val();
                d.register = $('#registerFilter').val();
                d.portalAccount = $('#portalAccountFilter').val();
            }
        },
        "columns": [
            { data: 'titlename' },
            { data: 'registrationnumber' },
            { data: 'firstname' },
            { data: 'professionname' },
            { data: 'registername' },
            {
                data: 'portalaccount',
                "render": function (data) {
                    var badgeClass = data === "Portal Activated" ? "badge-success" : "badge-danger";
                    return `<span class="badge ${badgeClass}">${data}</span>`;
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
                                     <a class="dropdown-item" href="/Customer/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Customer/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Customer/Account?id=${data}"><i class="bi bi-person-bounding-box"></i> Activate Portal</a>
                                    <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Customer/Show?id=${data}"><i class="bi bi-person-bounding-box"></i> View Profile</a>

                                </div>
                              </div>`
                }
            }

        ]
    });
    $('#prefixFilter, #professionFilter, #registerFilter, #portalAccountFilter').change(function () {
        dataTable.ajax.reload();
    });
}