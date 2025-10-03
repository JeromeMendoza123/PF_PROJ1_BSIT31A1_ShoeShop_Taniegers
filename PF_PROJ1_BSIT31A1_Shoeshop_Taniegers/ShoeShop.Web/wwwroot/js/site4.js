// =======================
// ORDERS
// =======================
const orderModalEl = document.getElementById('purchaseOrderModal');
const orderModal = orderModalEl ? new bootstrap.Modal(orderModalEl) : null;

window.openOrderModal = function () {
    $('#purchaseOrderForm')[0].reset();
    $('#purchaseOrderForm [name="Id"]').val('');
    $('#purchaseOrderForm').data('action', '/PurchaseOrders/Create');
    if (orderModal) orderModal.show();
}

window.openEditOrder = function (id) {
    $.get('/PurchaseOrders/GetOrder?id=' + id, function (data) {
        if (!data.success) { alert(data.message); return; }
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
        if ($.fn.DataTable.isDataTable('#ordersTable')) $('#ordersTable').DataTable().destroy();
        $('#ordersTable').DataTable({ paging: true, searching: true, ordering: true, info: true, autoWidth: false, columnDefs: [{ orderable: false, targets: -1 }] });
    });
}

// =======================
// PULL OUTS
// =======================
const pullOutModalEl = document.getElementById('pullOutModal');
const pullOutModal = pullOutModalEl ? new bootstrap.Modal(pullOutModalEl) : null;

window.openPullOutModal = function () {
    $('#pullOutForm')[0].reset();
    $('#pullOutForm').data('action', '/PullOuts/Create');
    if (pullOutModal) pullOutModal.show();
}

window.openEditPullOut = function (id) {
    $.get('/PullOuts/Get?id=' + id, function (data) {
        if (!data) { alert("Pull out not found!"); return; }
        $('#pullOutForm [name="Id"]').val(data.id);
        $('#pullOutForm [name="Name"]').val(data.name);
        $('#pullOutForm [name="Brand"]').val(data.brand);
        $('#pullOutForm [name="Cost"]').val(data.cost);
        $('#pullOutForm [name="Price"]').val(data.price);
        $('#pullOutForm [name="Description"]').val(data.description);
        $('#pullOutForm [name="ImageUrl"]').val(data.imageUrl);
        $('#pullOutForm').data('action', '/PullOuts/Edit');
        if (pullOutModal) pullOutModal.show();
    });
}

window.submitPullOutForm = function () {
    const form = $('#pullOutForm')[0];
    const data = $(form).serialize();
    const action = $('#pullOutForm').data('action');
    $.post(action, data, function (response) {
        if (pullOutModal) pullOutModal.hide();
        alert("Pull out saved successfully!");
        setTimeout(() => location.reload(), 500);
    });
}

window.confirmDeletePullOut = function (id) {
    if (confirm("Are you sure you want to delete this pull out?")) {
        $.post('/PullOuts/Delete', { id: id }, function (response) {
            alert("Deleted successfully!");
            setTimeout(() => location.reload(), 500);
        });
    }
}

// =======================
// REPORTS
// =======================
const reportModalEl = document.getElementById('reportModal');
const reportModal = reportModalEl ? new bootstrap.Modal(reportModalEl) : null;

window.openReportModal = function () {
    $('#reportForm')[0].reset();
    $('#reportForm').data('action', '/Reports/Create');
    if (reportModal) reportModal.show();
}

window.openEditReport = function (id) {
    $.get('/Reports/Get?id=' + id, function (data) {
        if (!data) { alert("Report not found!"); return; }
        $('#reportForm [name="Id"]').val(data.id);
        $('#reportForm [name="Name"]').val(data.name);
        $('#reportForm [name="Brand"]').val(data.brand);
        $('#reportForm [name="Cost"]').val(data.cost);
        $('#reportForm [name="Price"]').val(data.price);
        $('#reportForm [name="Description"]').val(data.description);
        $('#reportForm [name="ImageUrl"]').val(data.imageUrl);
        $('#reportForm').data('action', '/Reports/Edit');
        if (reportModal) reportModal.show();
    });
}

window.submitReportForm = function () {
    const form = $('#reportForm')[0];
    const data = $(form).serialize();
    const action = $('#reportForm').data('action');
    $.post(action, data, function (response) {
        if (reportModal) reportModal.hide();
        alert(response.message);
        if (response.success) setTimeout(() => location.reload(), 500);
    });
}

window.confirmDeleteReport = function (id) {
    if (confirm("Are you sure you want to delete this report?")) {
        $.post('/Reports/Delete', { id: id }, function (response) {
            alert(response.message);
            if (response.success) setTimeout(() => location.reload(), 500);
        });
    }
}

// =======================
// Initialize DataTables
// =======================
$(document).ready(function () {
    if ($('#ordersTable').length) $('#ordersTable').DataTable({ paging: true, searching: true, ordering: true, info: true, autoWidth: false, columnDefs: [{ orderable: false, targets: -1 }] });
    if ($('#pullOutTable').length) $('#pullOutTable').DataTable({ paging: true, searching: true, ordering: true, info: true, autoWidth: false, columnDefs: [{ orderable: false, targets: -1 }] });
    if ($('#reportsTable').length) $('#reportsTable').DataTable({ paging: true, searching: true, ordering: true, info: true, autoWidth: false, columnDefs: [{ orderable: false, targets: -1 }] });
});
