var Doc = (function () {
    function Doc(name) {
        this.name = name;
    }
    Doc.prototype.start = function () {
        var stu = new Student();
        stu.read("Sddd");
        $("#div").html("start");
        document.write("start");
    };
    return Doc;
})();
window.onload = function () {
    var doc = new Doc("sdd");
    // alert(doc.name)
    doc.start();
};
function greater(iperson) {
    return "hello " + iperson.firstName + " " + iperson.lastName;
}
var person = { firstName: "kang", lastName: "wenli" };
document.write(greater(person));
//# sourceMappingURL=file1.js.map