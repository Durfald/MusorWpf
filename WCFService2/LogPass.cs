using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WCFService2;

namespace WcfService
{
    public class LogPass
    {
        public string Login;
        public string Password;
        public int UserID;
        public string HWID;
        public int Score;
        public string SecretKey;
        public Access Access;
        private List<User> Users = new List<User>();
        public List<string> _Codes = new List<string>();
        public DateTime DateSubEnd;
        public string Codes;

        public bool checkhwid(string Hwid)
        {
            if (string.IsNullOrEmpty(Hwid)) return false;
            if (string.IsNullOrEmpty(HWID))
            {
                HWID = Hwid;
                return true;
            }
            if (Hwid == HWID) return true;
            else return false;
        }

        public void SetUsers(List<User> users)
        {
            Users = users;
        }
        public void ClearUsers()
        {
            Users.Clear();
        }
        public void AddUsertoUsers(User user)
        {
            Users.Add(user);
        }
        public void AddUserstoUsers(List<User> users)
        {
            Users.AddRange(users);
        }

        public List<User> GetUsers()
        {
            return Users;
        }
    }
    public enum Access
    {
        Admin = 0,
        Helper = 1,
    }
}