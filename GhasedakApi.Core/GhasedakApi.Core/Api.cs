using GhasedakApi.Core.Exceptions;
using GhasedakApi.Core.Interfaces;
using GhasedakApi.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static GhasedakApi.Core.Models.Results;

namespace GhasedakApi.Core
{
    public class Api : IAccountService, IReceiveService, IVoiceService, IContactService, ISMSService
    {
        /// <summary>
        /// The root URI for the service endpoint.
        /// </summary>
        private const string _BaseUrl = "http://178.216.249.18:8011/";

        private readonly string _apikey;

        /// <summary>
        /// The HTTP client
        /// </summary>
        private HttpClient _httpClient;


        public Api(string apikey)
        {
            _apikey = apikey;
            _httpClient = new HttpClient();
        }

        public async Task<AccountResult> AccountInfo()
        {
            var url = "api/v1/account/info";
            var param = new Dictionary<string, object>
              {
                  {"apikey", _apikey}
               };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<AccountResult>(res);
        }
        private async Task<string> PostRequest(string url, Dictionary<string, object> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
            var resp = await _httpClient.PostAsync(string.Format("{0}{1}", _BaseUrl, url), GetBodyData(parameters));
            var content = await resp.Content.ReadAsStringAsync();
            try
            {
                var result = JsonConvert.DeserializeObject<ApiResult>(content);
                if (result.Result.Code != 200)
                    throw new ApiException(result.Result.Code, result.Result.Message);
                return content;
            }
            catch (Exception ex)
            {
                throw new ConnectionException(ex.Message);
            }
        }
        private FormUrlEncodedContent GetBodyData(Dictionary<string, object> parameters)
        {
            return new FormUrlEncodedContent(parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())));
        }
        public async Task<ReceiveMessageResult> ReceiveList(string linenumber, int isRead)
        {
            var url = "api/v1/sms/receive";
            var param = new Dictionary<string, object>
                {
                 {"apikey", _apikey},
                 {"linenumber", linenumber},
                 {"isRead", isRead},
                };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<ReceiveMessageResult>(res);
        }

        public async Task<SendResult> SendVoice(string message, string[] receptor, string senddate)
        {
            var url = "api/v1/voice/send";
            var param = new Dictionary<string, object>
                {
                     {"apikey", _apikey},
                     {"message", message},
                     {"senddate", senddate},
                };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        #region Contract
        public async Task<GroupResult> AddGroup(string name, int parent)
        {
            var url = "api/v1/contact/group/add";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"name", name},
                {"parent", parent},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<GroupResult>(res);
        }

        public async Task<ApiResult> RemoveGroup(int groupid)
        {
            var url = "api/v1/contact/group/remove";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<ApiResult> EditGroup(int groupid, string name)
        {
            var url = "api/v1/contact/group/edit";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"name", name},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<ApiResult> AddNumberToGroup(int groupid, string[] number, string[] firstname, string[] lastname, string[] email)
        {
            var url = "api/v1/contact/group/number/add";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"number", string.Join(",",number)},
                {"firstname", string.Join(",",firstname)},
                {"lastname", string.Join(",",lastname)},
                {"email", string.Join(",",email)},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<GroupListResult> GroupList(int parent)
        {
            var url = "api/v1/contact/group/list";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"parent", parent},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<GroupListResult>(res);
        }

        public async Task<GroupNumbersResult> GroupNumbersList(int groupid, int page, int offset)
        {
            var url = "api/v1/contact/group/list";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"groupid", groupid},
                {"page", page},
                {"offset", offset},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<GroupNumbersResult>(res);
        }
        #endregion
        #region SMS
        public async Task<SendResult> SendSMS(string message, string linenumber, string receptor)
        {
            var url = "api/v1/sms/send/simple";
            var param = new Dictionary<string, object>
            {
                {"apikey", _apikey},
                {"linenumber", linenumber},
                {"receptor",receptor },
                {"message", System.Web.HttpUtility.UrlEncodeUnicode(message) }
            };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> SendSMS(string message, string linenumber, string receptor, DateTime senddate)
        {
            var url = "api/v1/sms/send/simple";
            var param = new Dictionary<string, object>
            {
                {"apikey", _apikey},
                {"linenumber", linenumber},
                {"receptor",receptor },
                {"senddate",Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate) },
                {"message", System.Web.HttpUtility.UrlEncodeUnicode(message) }
            };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> SendSMS(string message, string linenumber, string[] receptor)
        {
            var url = "api/v1/sms/send/bulk2";
            var param = new Dictionary<string, object>
             {
               {"apikey", _apikey},
               {"linenumber", linenumber},
               {"receptor",string.Join(",",receptor) },
               {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> SendSMS(string message, string linenumber, string[] receptor, DateTime senddate)
        {
            var url = "api/v1/sms/send/bulk2";
            var param = new Dictionary<string, object>
             {
               {"apikey", _apikey},
               {"linenumber", linenumber},
               {"receptor",string.Join(",",receptor) },
               {"message", System.Web.HttpUtility.UrlEncodeUnicode(message)},
               {"senddate",Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate) }
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> SendSMS(string[] message, string[] linenumber, string[] receptor)
        {
            var url = "api/v1/sms/send/bulk";
            var msg = new System.Text.StringBuilder();
            foreach (var item in message)
            {
                msg.Append(System.Web.HttpUtility.UrlEncodeUnicode(item)).Append(",");
            }
            var param = new Dictionary<string, object>
               {
                  {"apikey", _apikey},
                  {"linenumber", string.Join(",",linenumber)},
                  {"receptor",string.Join(",",receptor) },
                  {"message", msg},
                };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> SendSMS(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate)
        {
            var url = "api/v1/sms/send/bulk";
            var msg = new System.Text.StringBuilder();
            var date = new System.Text.StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                msg.Append(System.Web.HttpUtility.UrlEncodeUnicode(message[i])).Append(",");
                date.Append(Utilities.Date_Time.DatetimeToUnixTimeStamp(senddate[i])).Append(",");
            }
            var param = new Dictionary<string, object>
               {
                  {"apikey", _apikey},
                  {"linenumber", string.Join(",",linenumber)},
                  {"receptor",string.Join(",",receptor) },
                  {"message", msg},
                  {"senddate", date}
                };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SendResult> Verify(int type, string template, string[] receptor, string param1, string param2, string param3)
        {
            var url = "api/v1/sms/send/verify";
            var param = new Dictionary<string, object>
              {
                  {"apikey", _apikey},
                  {"type", type},
                  {"receptor",string.Join(",",receptor) },
                  {"param1", param1},
                  {"param2", param2},
                  {"param3", param3},
                  {"template", template}
              };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<StatusResult> GetStatus(long[] messageid)
        {
            var url = "api/v1/sms/status";
            var param = new Dictionary<string, object>
               {
                   {"apikey", _apikey},
                   {"messageid", string.Join(",",messageid)},
               };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<StatusResult>(res);
        }

        public async Task<SendResult> CancelSMS(long[] messageid)
        {
            var url = "api/v1/sms/cancel";
            var param = new Dictionary<string, object>
             {
                {"apikey", _apikey},
                {"messageid", string.Join(",",messageid)},
             };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }

        public async Task<SelectMessageResult> SelectSMS(long[] messageid)
        {
            var url = "api/v1/sms/select";
            var param = new Dictionary<string, object>
               {
                   {"apikey", _apikey},
                   {"messageid", string.Join(",",messageid)},
               };
            var res = await PostRequest(url, param);
            return JsonConvert.DeserializeObject<SelectMessageResult>(res);
        }
        #endregion
    }
}
