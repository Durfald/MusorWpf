using Newtonsoft.Json;
using System;

namespace WcfService
{
    public class User
    {
        public string NicknameSended;
        public DateTime Date;
        public string Nickname;
        public string code;
        public User()
        {

        }
        public void SetNicknameSended(string nickname)
        {
            NicknameSended = nickname;
        }
        public override string ToString()
        {
            return $"nickname sended: {NicknameSended} to {Nickname}";  
        }
    }
}
