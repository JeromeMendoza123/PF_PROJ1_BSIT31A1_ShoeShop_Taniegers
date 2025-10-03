const orderModalEl = document.getElementById('purchaseOrderModal'); // match Razor
const orderModal = orderModalEl ? new bootstrap.Modal(orderModalEl) : null;

// Open Add Order Modal
window.openOrderModal = function () {
    $('#purchaseOrderForm')[0].reset();
    $('#purchaseOrderForm [name="Id"]').val('');
    $('#purchaseOrderForm').data('action', '/PurchaseOrders/Create');
    if (orderModal) orderModal.show();
}

// Open Edit Order Modal
window.openEditOrderModal = function (id) {
    $.get('/PurchaseOrders/GetOrder?id=' + id, function (data) {
        if (!data.success) {
            alert(data.message);
            return;
        }

        $('#purchaseOrderForm [name="Id"]').val(data.id);
        $('#purchaseOrderForm [name="CustomerName"]').val(data.customerName);
        $('#purchaseOrderForm [name="Total"]').val(data.total);
        $('#purchaseOrderForm [name="Notes"]').val(data.notes);
        $('#purchaseOrderForm').data('action', '/PurchaseOrders/Edit');
        if (orderModal) orderModal.show();
    });
}

window.submitOrderForm = function () {
    const form = $('#purchaseOrderForm')[0];
    const formData = $(form).serialize();
    const action = $('#purchaseOrderForm').data('action');

    $.post(action, formData, function (response) {
        if (orderModal) orderModal.hide();
        alert(response.message);

        if (response.success) refreshOrdersTable();
    });
}

window.confirmDeleteOrder = function (id) {
    if (!confirm("Are you sure you want to delete this order?")) return;

    $.post('/PurchaseOrders/Delete', { id: id }, function (response) {
        alert(response.message);
        if (response.success) refreshOrdersTable();
    });
}

function refreshOrdersTable() {
    $.get('/PurchaseOrders', function (html) {
        const newTbody = $(html).find('#ordersTable tbody').html();
        $('#ordersTable tbody').html(newTbody);

        if ($.fn.DataTable.isDataTable('#ordersTable')) {
            $('#ordersTable').DataTable().destroy();
        }
        $('#ordersTable').DataTable({
            paging: true,
            searching: true,
            ordering: true,
            info: true,
            autoWidth: false,
            columnDefs: [{ orderable: false, targets: -1 }]
        });
    });
}
