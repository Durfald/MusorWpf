using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService;

namespace WCFService2
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IServiceData" в коде и файле конфигурации.
    [ServiceContract]
    public interface IServiceData
    {
        [OperationContract]
        SendIndfo Login(LogPass logPass);

        [OperationContract]
        SendIndfo GetCodes(LogPass logPass);

        [OperationContract]
        void DeleteUser(LogPass logPass);


        [OperationContract]
        SendIndfo SendNickname(logger user);


        [OperationContract]
        SendIndfo GetListNickname(logger getlist);


        [OperationContract]
        SendIndfo CreateHelperAcc(logger chelper);


        [OperationContract]
        SendIndfo ChangeBalance(logger AccBalance);

        [OperationContract]
        SendIndfo GetBalance(logger AccBalance);

        [OperationContract]
        SendIndfo AddCodetoHelper(logger Addhelper);


        [OperationContract]
        SendIndfo GetDate(logger Dhelper);


        [OperationContract]
        SendIndfo GetHelpers(logger helper);


        [OperationContract]
        SendIndfo DeleteHelperAcc(logger helper);


        [OperationContract]
        SendIndfo RemoveCodeHelper(logger helper);
    }
}