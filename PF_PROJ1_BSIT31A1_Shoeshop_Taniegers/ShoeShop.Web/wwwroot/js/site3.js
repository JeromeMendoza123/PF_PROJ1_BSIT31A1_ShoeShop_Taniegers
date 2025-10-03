// Pull Out Modal
const pullOutModalEl = document.getElementById('pullOutModal');
const pullOutModal = pullOutModalEl ? new bootstrap.Modal(pullOutModalEl) : null;

window.openPullOutModal = function () {
    $('#pullOutForm')[0].reset();
    $('#pullOutForm').data('action', '/PullOuts/Create');
    if (pullOutModal) pullOutModal.show();
}

window.openEditPullOutModal = function (id) {
    $.get('/PullOuts/Get?id=' + id, function (data) {
        if (!data) {
            alert("Pull out not found!");
            return;
        }
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

$(document).ready(function () {
    $('#pullOutsTable').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        columnDefs: [
            { orderable: false, targets: -1 }
        ]
    });
});
