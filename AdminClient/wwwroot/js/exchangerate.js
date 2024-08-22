$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/ExchangeRate/GetAll' },
        "columns": [
            { data: 'dateCreated' },
            { data: 'primaryCurrency.name' },
            { data: 'secondaryCurrency.name' },
            {data:'rate'},         

        ]
    });
}