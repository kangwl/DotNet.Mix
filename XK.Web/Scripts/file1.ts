class Doc {
    public name: String;

    start() {
        var stu = new Student();
        stu.read("Sddd");
        $("#div").html("start");
        document.write("start");
    }

    constructor(name: String) {
        this.name = name;
    }
}

window.onload = () => {
    var doc = new Doc("sdd");
   // alert(doc.name)
    doc.start();
}

interface IPerson {
    firstName: string,
    lastName: string;
}
 

function greater(iperson: IPerson) {
    return "hello " + iperson.firstName + " " + iperson.lastName;
}

var person = { firstName: "kang", lastName: "wenli" }

document.write(greater(person));
