namespace MatakDBConnector
{
    public class User
    {
        private int userId;
        private string password;
        private int phoneId;
        private string lastName;
        private string firstName;
        private int permissionId;
        private int orgId;
        private string email;
        private string nickname;

        public User()
        {
            userId = 0;
            password = "0";
            phoneId = 0;
            lastName = "0";
            firstName = "0";
            permissionId = 0;
            orgId = 0;
            email = "0";
            nickname = "0";
        }

        public User(int userId, string password, int phoneId, string lastName, string firstName, int permissionId, int orgId, string email, string nickname)
        {
            this.userId = userId;
            this.password = password;
            this.phoneId = phoneId;
            this.lastName = lastName;
            this.firstName = firstName;
            this.permissionId = permissionId;
            this.orgId = orgId;
            this.email = email;
            this.nickname = nickname;
        }

        public int UsedId
        {
            get => userId;
            set => userId = value;
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public int PhoneId
        {
            get => phoneId;
            set => phoneId = value;
        }

        public string LastName
        {
            get => lastName;
            set => lastName = value;
        }

        public string FirstName
        {
            get => firstName;
            set => firstName = value;
        }

        public int PermissionId
        {
            get => permissionId;
            set => permissionId = value;
        }

        public int OrgId
        {
            get => orgId;
            set => orgId = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Nickname
        {
            get => nickname;
            set => nickname = value;
        }
    }
}