using System;

namespace MyTestShop.Model {
    public class User {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string UserPass { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public DateTime Birthday { get; set; }
    }
}
