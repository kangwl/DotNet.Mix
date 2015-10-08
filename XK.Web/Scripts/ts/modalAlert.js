var ModalAlert = (function () {
    function ModalAlert() {
    }
    ModalAlert.prototype.ModalAlert = function (name) {
        this.name = name;
    };
    ModalAlert.prototype.create = function (title, content) {
        var head = document.createElement("div");
        head.innerText = title;
        var container = document.createElement("div");
        container.innerHTML = content;
        var main = document.createElement("div");
        var obj = main.appendChild(head).appendChild(container);
    };
    return ModalAlert;
})();
