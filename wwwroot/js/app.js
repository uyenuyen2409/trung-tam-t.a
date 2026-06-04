// ===== SHARED UTILITIES =====
const API_BASE = '';

// Auth guard
function checkAuth() {
    if (sessionStorage.getItem('loggedIn') !== 'true') {
        window.location.href = 'login.html';
    }
}

// Toast notification
function showToast(msg, type = 'info') {
    let container = document.getElementById('toastContainer');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toastContainer';
        container.className = 'toast-container';
        document.body.appendChild(container);
    }
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    const icons = { success: '✅', error: '❌', info: 'ℹ️' };
    toast.innerHTML = `<span>${icons[type] || 'ℹ️'}</span> ${msg}`;
    container.appendChild(toast);
    setTimeout(() => toast.remove(), 3000);
}

// Format currency
function formatVND(num) {
    return new Intl.NumberFormat('vi-VN').format(num) + ' ₫';
}

// Format date
function formatDate(dateStr) {
    if (!dateStr) return '';
    const d = new Date(dateStr);
    return d.toLocaleDateString('vi-VN');
}

// Generate sidebar HTML
function renderSidebar(activePage) {
    const links = [
        { section: 'Tổng quan', items: [
            { href: 'index.html', icon: '📊', text: 'Trang Chủ', id: 'index' },
        ]},
        { section: 'Danh mục', items: [
            { href: 'hocvien.html', icon: '👨‍🎓', text: 'Quản lý Học Viên', id: 'hocvien' },
            { href: 'khoahoc.html', icon: '📚', text: 'Quản lý Khóa Học', id: 'khoahoc' },
            { href: 'lophoc.html', icon: '🏫', text: 'Quản lý Lớp Học', id: 'lophoc' },
            { href: 'giangvien.html', icon: '👨‍🏫', text: 'Quản lý Giảng Viên', id: 'giangvien' },
        ]},
        { section: 'Nghiệp vụ', items: [
            { href: 'dangkyhoc.html', icon: '📝', text: 'Đăng Ký Học', id: 'dangkyhoc' },
            { href: 'phancong.html', icon: '📋', text: 'Phân Công GV', id: 'phancong' },
            { href: 'thanhtoan.html', icon: '💳', text: 'Thanh Toán', id: 'thanhtoan' },
        ]},
        { section: 'Báo cáo', items: [
            { href: 'thongke.html', icon: '📈', text: 'Thống Kê', id: 'thongke' },
        ]}
    ];

    let html = `
        <div class="sidebar-header">
            <div class="s-logo">🎓</div>
            <div>
                <div class="s-title">English Center</div>
                <div class="s-subtitle">Quản lý Trung tâm</div>
            </div>
        </div>
        <nav class="sidebar-nav">`;

    links.forEach(sec => {
        html += `<div class="nav-section-title">${sec.section}</div>`;
        sec.items.forEach(item => {
            const active = item.id === activePage ? ' active' : '';
            html += `<a href="${item.href}" class="nav-link${active}">
                <span class="nav-icon">${item.icon}</span> ${item.text}
            </a>`;
        });
    });

    html += `</nav>
        <div class="sidebar-footer">
            <a href="login.html" class="nav-link" onclick="sessionStorage.clear()">
                <span class="nav-icon">🚪</span> Đăng Xuất
            </a>
        </div>`;

    document.getElementById('sidebar').innerHTML = html;
}

// Set user info
function setUserInfo() {
    const el = document.getElementById('lblUser');
    if (el) el.textContent = sessionStorage.getItem('user') || 'Admin';
}
