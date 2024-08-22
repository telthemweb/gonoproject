$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/RegistrationReport/GetAll' },
        "columns": [
            { data: 'registrationnumber' },
            { data: 'lastname' },
            { data: 'firstname' },
            { data: 'professionname' },
            { data: 'registername' },
            { data: 'dateregistration' },
            

        ]
    });
}