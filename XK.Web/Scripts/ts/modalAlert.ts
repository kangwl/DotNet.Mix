class ModalAlert {
    public name: string;
    public ModalAlert(name) {
        this.name = name;
    }
    private create(title: string, content: string): void {
        var head = document.createElement("div");
        head.innerText = title;
        var container = document.createElement("div");
        container.innerHTML = content;

        var main = document.createElement("div");
        var obj = main.appendChild(head).appendChild(container);
         
    }
}