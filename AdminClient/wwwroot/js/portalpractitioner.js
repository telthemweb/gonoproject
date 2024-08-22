$(document).ready(function () {
    LoadDataTable();
});

function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/Portal/GetAllPractitioners' },
        "columns": [
            { data: 'id' },
            { data: 'name' },
            { data: 'surname' },
            { data: 'email' },
            {
                data: 'enabled',
                "render": function (data) {
                    var datac = data === true ? "ACTIVE" : "BLOCKED";
                    var badgeClass = data === true ? "badge-success p-2" : "badge-danger p-2";
                    return `<span class="badge ${badgeClass}">${datac}</span>`;
                }
            }
            
        ]
    });
}
