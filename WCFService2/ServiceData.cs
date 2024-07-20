using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfService;

namespace WCFService2
{
    public class SendIndfo
    {
        public int Score;
        public bool Seeker;
        public string Info;
        public List<User> Users;
        public List<LogPass> passes;
        public DateTime DateSubEnd;
    }
    [ServiceBehavior]
    public class ServiceData : IServiceData
    {
        public static List<LogPass> loggeds = new List<LogPass>();
        public static List<LogPass> LogPassUsers = new List<LogPass>();
        static Random r = new Random();

        public SendIndfo AddCodetoHelper(logger helper)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (helper.logPass.Login == ii.Login && helper.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == helper.logPass.Login && i.Password == helper.logPass.Password)
                        {
                            logger.logPass = ii;
                            logger.Helper = helper.Helper;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            foreach (var i in LogPassUsers)
            {
                if (i.Login == logger.Helper.Login && i.Access == Access.Helper && i.SecretKey == logger.logPass.SecretKey)
                {
                    foreach (var ii in i._Codes)
                    {
                        if (ii == logger.Helper.Code)
                        {
                            return new SendIndfo() { Seeker = false, Info = " This code is already added" };
                        }
                    }
                    i._Codes.Add(logger.Helper.Code);
                    Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} add code to helper account {helper.Helper.Login}");
                    File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                    return new SendIndfo() { Seeker = true, Info = " Successfully added code" };
                }
            }
            return new SendIndfo() { Seeker = false, Info = " Not found user" };
        }

        public SendIndfo CreateHelperAcc(logger helper)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (helper.logPass.Login == ii.Login && helper.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == helper.logPass.Login && i.Password == helper.logPass.Password)
                        {
                            logger.logPass = ii;
                            logger.Helper = helper.Helper;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            if (!File.Exists($"{helper.Helper.Login}Users.txt"))
            {
                File.Create($"{helper.Helper.Login}Users.txt").Close();
                File.WriteAllText($"{helper.Helper.Login}Users.txt", "[]");
            }
            else
            {
                return new SendIndfo() { Seeker = true, Info = "Login is already used" };
            }
            var LogPassHelper = new LogPass() { Login = logger.Helper.Login, Password = logger.Helper.Password, Access = Access.Helper, SecretKey = logger.logPass.SecretKey, UserID = logger.logPass.UserID, DateSubEnd = logger.logPass.DateSubEnd };
            LogPassUsers.Add(LogPassHelper);
            Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} create helper account {helper.Helper.Login}");
            File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
            return new SendIndfo() { Seeker = true, Info = "Successfully created account" };
        }

        public SendIndfo DeleteHelperAcc(logger helper)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (helper.logPass.Login == ii.Login && helper.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == helper.logPass.Login && i.Password == helper.logPass.Password)
                        {
                            logger.logPass = ii;
                            logger.Helper = helper.Helper;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            foreach (var i in LogPassUsers)
            {
                if (i.Login == logger.Helper.Login && i.Access == Access.Helper&&i.SecretKey==logger.logPass.SecretKey)
                {
                    LogPassUsers.Remove(i);
                    Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} delete account helper {helper.Helper.Login}");
                    File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                    break;
                }
            }
            foreach (var i in LogPassUsers)
            {
                if (i.Login == logger.logPass.Login && i.Access == Access.Admin && i.SecretKey == logger.logPass.SecretKey)
                {
                    try
                    {
                        i.AddUserstoUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(helper.Helper.Login + "Users.txt")));
                        File.Delete(helper.Helper.Login + "Users.txt");
                        return new SendIndfo() { Seeker = true, Info = " Successfully deleted account" };
                    }
                    catch
                    {
                        return new SendIndfo() { Seeker = true, Info = " Not found user" };
                    }
                }
            }
            File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers,Formatting.Indented));
            return new SendIndfo() { Seeker = false, Info = " Not found user" };
        }

        public SendIndfo GetDate(logger loggers)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (loggers.logPass.Login == ii.Login && loggers.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == loggers.logPass.Login && i.Password == loggers.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            return new SendIndfo() { DateSubEnd = logger.logPass.DateSubEnd, Seeker = true };
        }

        public SendIndfo GetHelpers(logger helper)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (helper.logPass.Login == ii.Login && helper.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == helper.logPass.Login && i.Password == helper.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            List<LogPass> logpasers = new List<LogPass>();
            foreach (var i in LogPassUsers)
            {
                if (i.SecretKey == logger.logPass.SecretKey && i.Access == Access.Helper)
                {
                    logpasers.Add(i);
                }
            }
            foreach (var i in logpasers)
            {
                i.SecretKey = null;
                i.UserID = 0;
                i.HWID = null;
            }
            return new SendIndfo() { Seeker = true, passes = logpasers };
        }

        public SendIndfo GetListNickname(logger loggers)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (loggers.logPass.Login == ii.Login && loggers.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == loggers.logPass.Login && i.Password == loggers.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper)
            {
                if (!File.Exists($"{logger.logPass.Login}Users.txt"))
                {
                    File.Create($"{logger.logPass.Login}Users.txt").Close();
                    File.WriteAllText($"{logger.logPass.Login}Users.txt", "[]");
                }
                logger.logPass.SetUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($"{logger.logPass.Login}Users.txt")));
                return new SendIndfo() { Seeker = true, Users = logger.logPass.GetUsers() };
            }
            else
            {
                if (!File.Exists($"{logger.logPass.Login}Users.txt"))
                {
                    File.Create($"{logger.logPass.Login}Users.txt").Close();
                    File.WriteAllText($"{logger.logPass.Login}Users.txt", "[]");
                }
                logger.logPass.SetUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($"{logger.logPass.Login}Users.txt")));
                foreach (var i in LogPassUsers)
                {
                    if (logger.logPass.SecretKey == i.SecretKey && i.Access == Access.Helper)
                    {
                        if (!File.Exists($"{i.Login}Users.txt"))
                        {
                            File.Create($"{i.Login}Users.txt").Close();
                            File.WriteAllText($"{i.Login}Users.txt", "[]");
                        }
                        logger.logPass.AddUserstoUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText($"{i.Login}Users.txt")));
                    }
                }
                return new SendIndfo() { Users = logger.logPass.GetUsers(), Seeker = true };
            }
        }

        public SendIndfo Login(LogPass logPass)
        {
            try
            {
                var a = (File.ReadAllText("LogPassUsers.txt"));
                LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(a);
                foreach (var i in LogPassUsers)
                {
                    if (i.Login == logPass.Login && i.Password == logPass.Password)
                    {
                        foreach (var ii in loggeds)
                        {
                            if (logPass.Login == ii.Login && ii.Password == logPass.Password)
                            {
                                loggeds.Remove(ii);
                                Console.WriteLine($"Date {DateTime.UtcNow} :User {ii.Login} Reconnect");
                                break;
                            }
                        }
                        if (i.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Info = "No subscribtion", Seeker = false };
                        if (i.checkhwid(logPass.HWID))
                        {
                            loggeds.Add(i);
                            File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                            Console.WriteLine($"Date {DateTime.UtcNow} :User {logPass.Login} Logged");
                            return new SendIndfo() { Seeker = true };
                        }
                        else
                        {
                            return new SendIndfo() { Info = "Invalid HWID", Seeker = false };
                        }
                    }
                }
                return new SendIndfo() { Info = "Not found user", Seeker = false };
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new SendIndfo() { Info = "Problem", Seeker = false };
            }
          
        }

        public void DeleteUser(LogPass logPass)
        {
            foreach (var i in loggeds)
            {
                if(logPass.Login==i.Login&& logPass.Password==i.Password)
                {
                    loggeds.Remove(i);
                    Console.WriteLine($"Date {DateTime.UtcNow} :User {logPass.Login} disconected");
                    break;
                }
            }
        }

        public SendIndfo RemoveCodeHelper(logger helper)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (helper.logPass.Login == ii.Login && helper.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == helper.logPass.Login && i.Password == helper.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            foreach (var i in LogPassUsers)
            {
                if (i.Login == helper.Helper.Login && i.Access == Access.Helper&&i.SecretKey==logger.logPass.SecretKey)
                {
                    foreach (var ii in i._Codes)
                    {
                        if (ii == helper.Helper.Code)
                        {
                            Console.WriteLine($"Date {DateTime.UtcNow} :User {i.Login} deleted code {helper.Helper.Code} from the {helper.Helper.Login}");
                            i._Codes.Remove(helper.Helper.Code);
                            File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                            return new SendIndfo() { Seeker = true, Info = " Successfully removed code" };
                        }
                    }
                    return new SendIndfo() { Seeker = false, Info = " Not found code" };
                }
            }
            return new SendIndfo() { Seeker = false, Info = " Not found user" };
        }

        public SendIndfo GetCodes(LogPass logPass)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (logPass.Login == ii.Login && logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == logPass.Login && i.Password == logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Admin) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            logger.logPass.Login = null;
            logger.logPass.Password = null;
            logger.logPass.HWID = null;
            logger.logPass.SecretKey = null;
            logger.logPass.UserID = 0;
            return new SendIndfo() { Seeker = true, passes=new List<LogPass>() { logger.logPass} };
        }

        public SendIndfo SendNickname(logger user)
        {
            logger logger=new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if(user.logPass.Login==ii.Login&&user.logPass.Password==ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == user.logPass.Login && i.Password == user.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
           
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };

            if (logger.logPass.Access == Access.Admin)
            {

                if (!File.Exists($"{logger.logPass.Login}Users.txt"))
                {
                    File.Create($"{logger.logPass.Login}Users.txt").Close();
                    File.WriteAllText($"{logger.logPass.Login}Users.txt", "[]");
                }
                user.User.NicknameSended = logger.logPass.Login;
                Data data = new Data(user.User.Nickname, r.Next(0, int.MaxValue - 10), logger.logPass.UserID, user.User.code);
                data.SetSecretKey(logger.logPass.SecretKey);
                data.signature = data.market_api_generate_signature(data.ToString(), data.GetSecretKey());

                if (!data.market_api_validate_signature(data)) return new SendIndfo() { Info = " Signature not valid", Seeker = false };

                var code = data.postRest(data);

                if (code == Data.Code.has_this_item) return new SendIndfo() { Info = " This user already has this item", Seeker = false };
                else if (code == Data.Code.error) return new SendIndfo() { Info = " Neverlose.cc server error", Seeker = false };
                else if (code == Data.Code.invalid_item) return new SendIndfo() { Info = " Invalid code", Seeker = false };
                else if (code == Data.Code.errorUser) return new SendIndfo() { Info = " Invalid nickname", Seeker = false };

                logger.logPass.AddUserstoUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(logger.logPass.Login+"Users.txt")));
                logger.logPass.AddUsertoUsers(user.User);
                Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} send code {user.User.code} to {user.User.Nickname}");
                File.WriteAllText($"{logger.logPass.Login}Users.txt", JsonConvert.SerializeObject(logger.logPass.GetUsers(), Formatting.Indented));
                return new SendIndfo() { Info = $" Item successfully gifted", Seeker = true };

            }
            else
            {
                foreach (var ii in LogPassUsers)
                {
                    if (ii.Login == logger.logPass.Login)
                    {
                        foreach (var i in ii._Codes)
                        {
                            if (user.User.code == i)
                            {
                                if (!File.Exists($"{logger.logPass.Login}Users.txt"))
                                {
                                    File.Create($"{logger.logPass.Login}Users.txt").Close();
                                    File.WriteAllText($"{logger.logPass.Login}Users.txt", "[]");
                                }
                                if (ii.Score <= 0) return new SendIndfo() { Info = " You don't have enough points", Seeker = false };
                                user.User.NicknameSended = logger.logPass.Login;
                                Data data = new Data(user.User.Nickname, r.Next(0, int.MaxValue - 10), logger.logPass.UserID, user.User.code);
                                data.SetSecretKey(logger.logPass.SecretKey);
                                data.signature = data.market_api_generate_signature(data.ToString(), data.GetSecretKey());
                                if (!data.market_api_validate_signature(data)) return new SendIndfo() { Info = " Signature not valid", Seeker = false };
                                var code = data.postRest(data);
                                if (code == Data.Code.has_this_item) return new SendIndfo() { Info = " This user already has this item", Seeker = false };
                                else if (code == Data.Code.error) return new SendIndfo() { Info = " Neverlose.cc server error", Seeker = false };
                                logger.logPass.AddUserstoUsers(JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(logger.logPass.Login + "Users.txt")));
                                logger.logPass.AddUsertoUsers(user.User);
                                ii.Score--;
                                File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                                Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} send code {user.User.code} to {user.User.Nickname}");
                                File.WriteAllText($"{logger.logPass.Login}Users.txt", JsonConvert.SerializeObject(logger.logPass.GetUsers(), Formatting.Indented));
                                return new SendIndfo() { Info = $" Item successfully gifted", Seeker = true };
                            }
                        }
                        return new SendIndfo() { Info = $" Access denied", Seeker = false };
                    }
                }
                return new SendIndfo() { Info = $" Not found user", Seeker = false };
            }
        }

        public SendIndfo ChangeBalance(logger AccBalance)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (AccBalance.logPass.Login == ii.Login && AccBalance.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == AccBalance.logPass.Login && i.Password == AccBalance.logPass.Password)
                        {
                            logger.logPass = ii;
                            logger.Helper = AccBalance.Helper;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            if (logger.logPass.DateSubEnd < DateTime.UtcNow) return new SendIndfo() { Seeker = false, Info = " No subscribtion" };
            if (logger.logPass.Access == Access.Helper) return new SendIndfo() { Seeker = false, Info = " Access denied" };
            foreach (var i in LogPassUsers)
            {
                if (i.Login == logger.Helper.Login && i.Access == Access.Helper && i.SecretKey == logger.logPass.SecretKey)
                {
                    Console.WriteLine($"Date {DateTime.UtcNow} :User {logger.logPass.Login} change balance {AccBalance.Helper.Login} for {i.Score} to {logger.Helper.Score}");
                    i.Score = logger.Helper.Score;
                    File.WriteAllText("LogPassUsers.txt", JsonConvert.SerializeObject(LogPassUsers, Formatting.Indented));
                    return new SendIndfo() { Seeker = true, Info = " Successfully change balance" };
                }
            }
            return new SendIndfo() { Seeker = false, Info = " Not found user" };
        }

        public SendIndfo GetBalance(logger AccBalance)
        {
            logger logger = new logger();
            logger.logPass = null;
            LogPassUsers = JsonConvert.DeserializeObject<List<LogPass>>(File.ReadAllText("LogPassUsers.txt"));
            foreach (var ii in LogPassUsers)
            {
                if (AccBalance.logPass.Login == ii.Login && AccBalance.logPass.Password == ii.Password)
                {
                    foreach (var i in loggeds)
                    {
                        if (i.Login == AccBalance.logPass.Login && i.Password == AccBalance.logPass.Password)
                        {
                            logger.logPass = ii;
                            break;
                        }
                    }
                    break;
                }
            }
            if (logger.logPass == null) return new SendIndfo() { Seeker = false, Info = " Unknown user" };
            return new SendIndfo() { Score = logger.logPass.Score, Seeker = true };
        }
    }
}