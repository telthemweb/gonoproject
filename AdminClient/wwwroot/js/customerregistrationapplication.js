$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/CustomerRegistrationApplication/GetAll' },
        "columns": [
            { data: 'fullname' },
            { data: 'item' },
            { data: 'paymentStatus' },
            { data: 'paymentApproval' },
            { data: 'dateCreated' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                  <a class="btn btn-sm btn-primary" href="/CustomerRegistrationApplication/Show?id=${data}">View</a>
                              </div>`
                }
            }

        ]
    });
}