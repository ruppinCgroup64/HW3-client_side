using HW2_cgroup64.BL;

namespace HW3_cgroup64.BL
{
    public class User
    {
        string firstName;
        string familyName;
        string email;
        string password;
        static List<User> UsersList = new List<User>();

        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public User(string firstName, string familyName, string email, string password)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            this.Email = email;
            this.Password = password;
        }
        public User() { }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);
        }
        public User LoginUser()
        {
            DBservices dbs = new DBservices();
            return dbs.Login(this);
        }
        public List<User> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }
        public User Update() { 
            DBservices dbs = new DBservices();
            return dbs.UpdateUser(this);
        }

    }
}
