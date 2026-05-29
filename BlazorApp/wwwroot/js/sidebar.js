// sidebar.js — Staff Sidebar interactions

function toggleSidebar() {
    var sidebar = document.getElementById('staffSidebar');
    var overlay = document.getElementById('sidebarOverlay');
    if (sidebar) {
        sidebar.classList.toggle('open');
        if (overlay) overlay.classList.toggle('active');
    }
}

function closeSidebar() {
    var sidebar = document.getElementById('staffSidebar');
    var overlay = document.getElementById('sidebarOverlay');
    if (sidebar) sidebar.classList.remove('open');
    if (overlay) overlay.classList.remove('active');
}

function highlightActiveLink() {
    var links = document.querySelectorAll('.sidebar-link');
    var path = window.location.pathname.toLowerCase();
    links.forEach(function (link) {
        link.classList.remove('active-link');
        var href = link.getAttribute('href');
        if (href && path === href.toLowerCase()) {
            link.classList.add('active-link');
        }
    });
}

// Run on page load
document.addEventListener('DOMContentLoaded', function () {
    highlightActiveLink();
});

// Close sidebar and re-highlight when a nav link is clicked (Blazor SPA navigation)
document.addEventListener('click', function (e) {
    var link = e.target.closest('.sidebar-link, .sidebar-logout');
    if (link) {
        setTimeout(function () {
            closeSidebar();
            highlightActiveLink();
        }, 50);
    }
});
