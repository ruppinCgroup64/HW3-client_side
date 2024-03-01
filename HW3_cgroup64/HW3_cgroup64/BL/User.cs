using HW2_cgroup64.BL;

namespace HW3_cgroup64.BL
{
    public class User
    {
        string firstName;
        string familyName;
        string email;
        string password;
        bool isActive;
        bool isAdmin;
        //static List<User> UsersList = new List<User>();

        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        public User(string firstName, string familyName, string email, string password, bool isActive, bool isAdmin)
        {
            this.FirstName = firstName;
            this.FamilyName = familyName;
            this.Email = email;
            this.Password = password;
            this.IsActive = isActive;
            this.IsAdmin = isAdmin;
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
        public int UpdateIsActive()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUserIsActive(this);
        }

    }
}
