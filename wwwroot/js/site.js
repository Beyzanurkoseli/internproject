// Modal açma/kapatma işlemleri
function toggleModal(modalId, show) {
    var modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = show ? 'block' : 'none';
    }
}

// Register Modal'ını açma
function openRegisterModal() {
    toggleModal('registerModal', true);
}

// Register Modal'ını kapatma
function closeRegisterModal() {
    toggleModal('registerModal', false);
}

// Modal dışına tıklayınca kapatma
document.addEventListener('click', function(event) {
    var registerModal = document.getElementById('registerModal');
    if (registerModal && event.target === registerModal) {
        toggleModal('registerModal', false);
    }
});

// Sayfa yüklendiğinde close tuşuna tıklama işlemi
document.addEventListener('DOMContentLoaded', function () {
    var closeModalButtons = document.querySelectorAll('.custom-close');
    closeModalButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var modal = button.closest('.custom-modal');
            if (modal) {
                modal.style.display = 'none';
            }
        });
    });
});
