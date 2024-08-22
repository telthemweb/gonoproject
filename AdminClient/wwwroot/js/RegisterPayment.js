$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/RegisterPayment/GetAll' },
        "columns": [
            {
                data: 'dateCreated',
                "render": function (data) {
                    var formattedDate = new Date(data).toLocaleDateString('en-GB', {
                        day: 'numeric',
                        month: 'long',
                        year: 'numeric'
                    });
                    return formattedDate;
                }
            },
            { data: 'fullname' },
            { data: 'item' },
            { data: 'amount' },
            { data: 'channelused' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                  <a class="btn btn-sm btn-primary" href="/RegisterPayment/Show?id=${data}">View</a>
                              </div>`
                }
            }
        ]
    });
}
