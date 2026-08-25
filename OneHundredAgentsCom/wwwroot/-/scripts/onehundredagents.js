/* Make clickable */
(function () {
    document.querySelectorAll('.make-clickable').forEach(function (element) {
        const link = element.querySelector('a');

        if (!link) {
            element.classList.remove('make-clickable');
            return;
        }

        element.classList.replace('make-clickable', 'is-clickable');

        element.addEventListener('click', function (event) {
            // Allow links themselves to retain their normal browser behaviour.
            if (event.target.closest('a')) {
                return;
            }

            window.location.href = link.href;
        });
    });
}());
