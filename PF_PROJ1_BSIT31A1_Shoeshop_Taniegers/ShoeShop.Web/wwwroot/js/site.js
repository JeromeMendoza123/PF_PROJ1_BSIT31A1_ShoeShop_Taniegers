document.addEventListener("DOMContentLoaded", function () {
    // ===== Sidebar Toggle =====
    const toggleButton = document.getElementById("menu-toggle");
    const sidebar = document.getElementById("sidebar-wrapper");
    const pageContent = document.getElementById("page-content-wrapper");
    const overlay = document.getElementById("overlay");

    function toggleSidebar() {
        sidebar.classList.toggle("collapsed");
        if (window.innerWidth >= 769) {
            pageContent.classList.toggle("shifted");
        } else {
            overlay.classList.toggle("active");
        }
    }

    if (toggleButton) toggleButton.addEventListener("click", toggleSidebar);
    if (overlay) {
        overlay.addEventListener("click", function () {
            sidebar.classList.add("collapsed");
            overlay.classList.remove("active");
        });
    }

    // ===== Toast Notifications =====
    function showToast(message, type = "SuccessMessage") {
        const bgClass = type === "SuccessMessage" ? "bg-success" :
            type === "ErrorMessage" ? "bg-danger" : "bg-info";

        const toastId = `toast-${Date.now()}`;
        const toastHtml = `
            <div id="${toastId}" class="toast align-items-center text-white ${bgClass} border-0 mb-2" role="alert">
                <div class="d-flex">
                    <div class="toast-body">${message}</div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>
            </div>`;
        const container = document.getElementById("toastContainer");
        if (container) container.insertAdjacentHTML("beforeend", toastHtml);
        const toastElement = document.getElementById(toastId);
        if (toastElement) new bootstrap.Toast(toastElement, { delay: 3000 }).show();
    }

    if (window.toastMessages) {
        for (const [type, messages] of Object.entries(window.toastMessages)) {
            messages.forEach(msg => showToast(msg, type));
        }
    }

    // ===== Responsive DataTables =====
    $.fn.dataTable.ext.errMode = 'none'; // remove alert popups
    const tables = document.querySelectorAll("table.dataTable, table[id$='Table']");
    tables.forEach(table => {
        if (!$.fn.DataTable.isDataTable(table)) {
            $(table).DataTable({
                paging: true,
                searching: true,
                ordering: true,
                info: true,
                autoWidth: false,
                responsive: true,  // <-- mobile friendly
                columnDefs: [
                    { orderable: false, targets: table.querySelectorAll("th").length - 1 }
                ]
            });
        }
    });

    // ===== Shoe Modal Handling =====
    const modalElement = document.getElementById('shoesModal');
    const shoesModal = modalElement ? new bootstrap.Modal(modalElement) : null;

    window.openCreateModal = function () {
        $('#shoesForm')[0].reset();
        $('#ShoeId').val('');
        $('#shoesForm').data('action', '/Shoes/Create');
        if (shoesModal) shoesModal.show();
    };

    window.openEditModal = function (id) {
        $.get('/Shoes/GetShoe?id=' + id, function (data) {
            $('#ShoeId').val(data.id);
            $('#shoesForm [name="Name"]').val(data.name);
            $('#shoesForm [name="Brand"]').val(data.brand);
            $('#shoesForm [name="Cost"]').val(data.cost);
            $('#shoesForm [name="Price"]').val(data.price);
            $('#shoesForm [name="Description"]').val(data.description);
            $('#shoesForm [name="ImageUrl"]').val(data.imageUrl);
            $('#shoesForm').data('action', '/Shoes/Edit');
            if (shoesModal) shoesModal.show();
        });
    };

    window.submitForm = function () {
        var form = $('#shoesForm')[0];
        var formData = $(form).serialize();
        var action = $('#shoesForm').data('action');

        $.post(action, formData, function (response) {
            if (shoesModal) shoesModal.hide();
            showToast(response.message, response.success ? "SuccessMessage" : "ErrorMessage");
            setTimeout(() => location.reload(), 1000);
        });
    };

    window.confirmDelete = function (id) {
        if (confirm("Are you sure you want to delete this shoe?")) {
            $.post('/Shoes/Delete', { id: id }, function (response) {
                showToast(response.message, response.success ? "SuccessMessage" : "ErrorMessage");
                setTimeout(() => location.reload(), 1000);
            });
        }
    };
});
