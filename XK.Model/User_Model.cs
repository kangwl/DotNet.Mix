using System;

namespace XK.Model {
    public class User_Model  {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
    }
}
