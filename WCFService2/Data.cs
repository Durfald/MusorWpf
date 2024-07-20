using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WcfService
{
    internal class Data
    {
        public string username;
        public int user_id;
        public int id;
        public string code;
        private string SecretKey;
        public string signature; 
        public string GetSecretKey()
        {
            return SecretKey;
        }
        public void SetSecretKey(string secretkey)
        {
            SecretKey = secretkey;
        }
        public override string ToString()
        {
            return $"code{code}id{id}user_id{user_id}username{username}";
        }
        public string market_api_generate_signature(string data, string key = "")
        {
            string str_to_hash = data + key;
            SHA256 shaM = new SHA256Managed();
            var sw = new UTF8Encoding().GetBytes(str_to_hash);
            var a = shaM.ComputeHash(sw);
            var aw = hexdigest(a);
            return aw;
        }

        public bool market_api_validate_signature(Data data)
        {
            var old_signature = data.signature;
            data.signature = string.Empty;
            var our_signature = market_api_generate_signature(data.ToString(), SecretKey);
            var a = our_signature;
            if (our_signature != old_signature) return false;
            this.signature = our_signature;
            return true;
        }

        static string hexdigest(byte[] sha1result)
        {
            string hexaHash = "";
            foreach (byte b in sha1result)
            {
                hexaHash += String.Format("{0:x2}", b);
            }
            return hexaHash;
        }

        public Code postRest(Data data)
        {//
            string Url = "https://neverlose.cc/api/market/give-for-free";
            if (data.signature == null) data.signature = market_api_generate_signature(data.ToString(), SecretKey);
            var client = new RestClient(Url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-type", " application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.Content.Contains("User doesn't exist")) return Code.errorUser;
            if (response.Content.Contains("This user already has this item")) return Code.has_this_item;
            if (response.Content.Contains("invalid item")) return Code.invalid_item;
            if (response.Content.Contains("Error") || response.Content.Contains("error")) return Code.error;
            return Code.succ;
        }

        //This user already has this item
        public Data(string username, int id, int user_id, string code)
        {
            this.username = username;
            this.user_id = user_id;
            this.id = id;
            this.code = code;
        }
        public Data()
        {

        }
        public enum Code
        {
            has_this_item,
            error,
            succ,
            errorUser,
            invalid_item
        }
    }
}