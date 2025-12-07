(function () {
    function autoDismissAlerts() {
        var alerts = document.querySelectorAll('.alert[data-auto-dismiss]');
        alerts.forEach(function (el) {
            var timeout = parseInt(el.getAttribute('data-auto-dismiss'), 10);
            if (!isNaN(timeout) && timeout > 0) {
                setTimeout(function () {
                    var instance = bootstrap.Alert.getOrCreateInstance(el);
                    instance.close();
                }, timeout);
            }
        });
    }

    // Inicializa ao carregar
    document.addEventListener('DOMContentLoaded', autoDismissAlerts);

    // Se sua página atualiza conteúdo via postback parcial, você pode expor:
    window.initAutoDismissAlerts = autoDismissAlerts;
})();