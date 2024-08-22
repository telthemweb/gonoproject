$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/OtherApplication/GetAll' },
        "columns": [
            { data: 'fullname' },
            { data: 'item' },
            { data: 'paymentStatus' },
            { data: 'paymentApproval' },
            { data: 'approvalStatus' },
            { data: 'dateCreated' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                  <a class="btn btn-sm btn-primary" href="/OtherApplication/Show?id=${data}">View</a>
                              </div>`
                }
            }

        ]
    });
}