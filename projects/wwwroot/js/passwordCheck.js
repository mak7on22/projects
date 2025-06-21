document.addEventListener('DOMContentLoaded', function () {
    const password = document.getElementById('Password');
    const confirm = document.getElementById('ConfirmPassword');
    if (!password || !confirm) return;

    function validate() {
        if (password.value !== confirm.value) {
            confirm.setCustomValidity('Passwords do not match');
        } else {
            confirm.setCustomValidity('');
        }
    }

    password.addEventListener('input', validate);
    confirm.addEventListener('input', validate);
});
