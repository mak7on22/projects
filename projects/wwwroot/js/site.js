// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Simple confetti effect that follows the mouse cursor
document.addEventListener('mousemove', function (e) {
    const confetti = document.createElement('div');
    confetti.className = 'confetti';
    confetti.style.left = e.pageX + 'px';
    confetti.style.top = e.pageY + 'px';
    confetti.style.backgroundColor = `hsl(${Math.random() * 360},100%,50%)`;
    document.body.appendChild(confetti);

    // remove element after animation ends
    setTimeout(() => confetti.remove(), 1000);
});
