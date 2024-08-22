$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#catTable').DataTable({
        "ajax": { url: '/PaymentItem/GetAll' },
        "columns": [
            { data:'paymentItemCategory.name'},
            { data: 'name' },
            { data: 'requiredApproval' },
            { data: 'expiryType' },
            { data:'generateCertificate'},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-right">
                                <button type="button" class="btn btn-sm btn-primary dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                Action
                                </button>
                                <div class="dropdown-menu">
                                  <a class="dropdown-item" href="/PaymentItem/Modify?id=${data}"><i class="bi bi-pencil"></i> Edit</a>
                                  <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/PaymentItem/Delete?id=${data}"><i class="bi bi-trash-fill"></i> Delete</a>
                                   <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/PaymentItemFee/Index?id=${data}"><i class="bi bi-coin"></i> Fees</a>
                                   <div class="dropdown-divider"></div>
                                  <a class="dropdown-item" href="/PaymentItemRequirement/Index?id=${data}"><i class="bi bi-card-checklist"></i> Requirements</a>
                         
                                </div>
                              </div>`
                }
            }

        ]
    });
}