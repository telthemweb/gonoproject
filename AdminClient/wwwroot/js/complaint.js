$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
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
            { data:'status'}
          

        ]
    });
}
