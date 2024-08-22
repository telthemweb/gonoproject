$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#catTabled').DataTable({
        "ajax": { url: '/AwaitingApplication/GetAll' },
        "columns": [
            {
                data: 'DateCreated',
                "render": function (data) {
                    var formattedDate = new Date(data).toLocaleDateString('en-GB', {
                        day: 'numeric',
                        month: 'long',
                        year: 'numeric'
                    });
                    return formattedDate;
                }
            },
            { data: 'Fullname' },
            { data: 'ApplicationType' },
            { data: 'Profession' },
            {
                data: 'Id',
                "render": function (data) {
                    return `<div class="text-right">
                                  <a class="btn btn-sm btn-primary" href="/AwaitingApplication/Show?id=${data}">View</a>
                              </div>`;
                }
            }
        ]
    });
}
