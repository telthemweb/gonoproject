$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable =$('#profTable').DataTable({
        "ajax": {url:'/Profession/GetAll'},
        "columns": [
            { data: 'name' },
            { data: 'description' },
            { data: 'product.name' },
            { data: 'product.lastreg' },
            { data: 'points' },
            {
                data: 'placement',
                "render": function (data) {
                    return data=="0" ? "Not Required" :"Required"
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `
                           <div class="btn-group" role="group">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/Profession/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/Profession/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                </div>
                              </div>
                    `
                }
            }

    ]});
}