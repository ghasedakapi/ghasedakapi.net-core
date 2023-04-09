using Ghasedak.Core.Exceptions;
using Ghasedak.Core.Interfaces;
using Ghasedak.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Ghasedak.Core.Models.Results;

namespace Ghasedak.Core
{
    public class Api : IAccountService, IReceiveService, IVoiceService, IContactService, ISMSService
    {
        #region Fields
        /// <summary>
        /// The root URI for the service endpoint.
        /// </summary>
        private readonly string _BaseUrl = "https://api.ghasedak.me/";


        private readonly string _apikey;

        /// <summary>
        /// The HTTP client
        /// </summary>
        private HttpClient _httpClient;

        #endregion

        #region Ctor
        public Api(string apikey, string BaseUrl= "https://api.ghasedak.me/")
        {
            _apikey = apikey;
            _BaseUrl = BaseUrl;
            _httpClient = new HttpClient();
        }
        #endregion

        #region SMS
        public async Task<SendResult> SendSMSAsync(string message, string receptor, string linenumber = null, DateTime? senddate = null, String checkid = null, string dep = null)
        {
            var url = "v2/sms/send/simple";
            var param = new Dictionary<string, object>();
            param.Add("message", message ?? "");
            param.Add("receptor", receptor ?? "");

            if (!string.IsNullOrEmpty(linenumber))
                param.Add("linenumber", linenumber);
            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));
            if (!string.IsNullOrEmpty(checkid))
                param.Add("checkid", checkid);

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        public async Task<SendResult> SendSMSAsync(string[] message, string[] linenumber, string[] receptor, DateTime[] senddate = null, string[] checkid = null, string dep = null)
        {
            var url = "v2/sms/send/bulk";

            var date = new System.Text.StringBuilder();
            var check = new System.Text.StringBuilder();
            var param = new Dictionary<string, object>();


            param.Add("linenumber", string.Join(",", linenumber ?? new string[] { }));
            param.Add("message", string.Join(",", message ?? new string[] { }));
            param.Add("receptor", string.Join(",", receptor ?? new string[] { }));
            if (senddate != null && senddate.Length > 0)
            {
                foreach (var item in senddate)
                {
                    date.Append(Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(item))).Append(",");
                }
                param.Add("senddate", date);
            }

            if (checkid != null && checkid.Length > 0)
                param.Add("checkid", string.Join(",", checkid));

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        public async Task<SendResult> SendSMSAsync(string message, string[] receptor, string linenumber = null, DateTime? senddate = null, string[] checkid = null, string dep = null)
        {
            var url = "v2/sms/send/pair";
            var param = new Dictionary<string, object>();

            param.Add("message", message ?? "");
            param.Add("receptor", string.Join(",", receptor ?? new string[] { }));

            if (!string.IsNullOrEmpty(linenumber))
                param.Add("linenumber", linenumber);

            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));
            if (checkid != null && checkid.Length > 0)
                param.Add("checkid", string.Join(",", checkid));


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        public async Task<SendResult> VerifyAsync(int type, string template, string[] receptor, string param1, string param2 = null, string param3 = null, string param4 = null, string param5 = null, string param6 = null, string param7 = null, string param8 = null, string param9 = null, string param10 = null, string dep = null)
        {
            var url = "v2/verification/send/simple";
            var param = new Dictionary<string, object>
        {
            {"type", type},
            {"receptor",string.Join(",",receptor??new string[]{ }) },
            {"template", template??""},
            {"param1", param1??""},
            {"param2", param2??""},
            {"param3", param3??""},
            {"param4", param4??""},
            {"param5", param5??""},
            {"param6", param6??""},
            {"param7", param7??""},
            {"param8", param8??""},
            {"param9", param9??""},
            {"param10", param10??""},
        };


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        public async Task<StatusResult> GetStatusAsync(int type, long[] id, string dep = null)
        {
            var url = "v2/sms/status";
            var param = new Dictionary<string, object>
               {
                   {"type", type},
                   {"id", string.Join(",",id??new long[]{ })},
               };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param, method: "GET");
            return JsonConvert.DeserializeObject<StatusResult>(res);
        }
        public async Task<StatusResult> CancelSMSAsync(long[] messageid, string dep = null)
        {
            var url = "v2/sms/cancel";
            var param = new Dictionary<string, object>
             {

                {"messageid", string.Join(",",messageid??new long[]{ })},
             };


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<StatusResult>(res);
        }

        #endregion

        #region Account
        public async Task<AccountResult> AccountInfoAsync(string dep = null)
        {
            var url = "v2/account/info";

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, new Dictionary<string, object>(), method: "GET");
            return JsonConvert.DeserializeObject<AccountResult>(res);
        }
        #endregion

        #region Receive
        public async Task<ReceiveMessageResult> ReceiveListAsync(string linenumber, int isRead, string dep = null)
        {
            var url = "v2/sms/receive/last";
            var param = new Dictionary<string, object>
                {
                 {"linenumber", linenumber??""},
                 {"isRead", isRead},
                };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<ReceiveMessageResult>(res);
        }
        public async Task<ReceiveMessageResult> ReceiveListPagingAsync(string linenumber, int isRead, DateTime fromdate, DateTime todate, int page = 0, int offset = 200, string dep = null)
        {

            if (page < 0) page = 0;
            if (offset < 0 || offset > 200) offset = 200;

            var url = "v2/sms/receive/paging";
            var param = new Dictionary<string, object>
                {
                 {"linenumber", linenumber??""},
                 {"isRead", isRead},
                 {"fromdate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(fromdate))},
                 {"todate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(todate))},
                 {"page", page},
                 {"offset", offset},
                };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<ReceiveMessageResult>(res);
        }
        #endregion

        #region Voice
        public async Task<SendResult> SendVoiceAsync(string message, string[] receptor, DateTime? senddate = null, string dep = null)
        {
            var url = "v2/voice/send/simple";
            var param = new Dictionary<string, object>();

            param.Add("message", message ?? "");
            param.Add("receptor", string.Join(",", receptor ?? new string[] { }));
            if (senddate.HasValue)
                param.Add("senddate", Utilities.Date_Time.DatetimeToUnixTimeStamp(Convert.ToDateTime(senddate)));


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<SendResult>(res);
        }
        #endregion

        #region Contract
        public async Task<GroupResult> AddGroupAsync(string name, int parent, string dep = null)
        {
            var url = "v2/contact/group/new";
            var param = new Dictionary<string, object>
             {
                {"name", name??""},
                {"parent", parent},
             };


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<GroupResult>(res);
        }

        public async Task<ApiResult> RemoveGroupAsync(int groupid, string dep = null)
        {
            var url = "v2/contact/group/remove";
            var param = new Dictionary<string, object>
             {
               {"groupid", groupid},
             };


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<ApiResult> EditGroupAsync(int groupid, string name, string dep = null)
        {
            var url = "v2/contact/group/edit";
            var param = new Dictionary<string, object>
             {
                {"groupid", groupid},
                {"name", name??""},
             };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<ApiResult> AddNumberToGroupAsync(int groupid, string[] number, string[] firstname = null, string[] lastname = null, string[] email = null, string dep = null)
        {
            var url = "v2/contact/group/addnumber";
            var param = new Dictionary<string, object>
             {
                {"groupid", groupid},
                {"number", string.Join(",",number??new string[]{})},
                {"firstname", string.Join(",",firstname ??new string[]{})},
                {"lastname", string.Join(",",lastname??new string[]{})},
                {"email", string.Join(",",email??new string[]{})},
             };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param);
            return JsonConvert.DeserializeObject<ApiResult>(res);
        }

        public async Task<GroupListResult> GroupListAsync(int parent = 0, string dep = null)
        {
            var url = "v2/contact/group/list";
            var param = new Dictionary<string, object>
             {
                {"parent", parent},
             };

            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param, method: "GET");
            return JsonConvert.DeserializeObject<GroupListResult>(res);
        }

        public async Task<GroupNumbersResult> GroupNumbersListAsync(int groupid, int page = 1, int offset = 100, string dep = null)
        {
            page = page - 1;
            if (page < 0) page = 0;

            if (offset < 0 || offset > 100) offset = 100;

            var url = "v2/contact/group/listnumber";
            var param = new Dictionary<string, object>
             {
                {"groupid", groupid},
                {"page", page},
                {"offset", offset},
             };


            if (!string.IsNullOrEmpty(dep))
                url += "?dep=" + dep.Trim();

            var res = await PostRequestAsync(url, param, method: "GET");
            return JsonConvert.DeserializeObject<GroupNumbersResult>(res);
        }
        #endregion


        #region Utility
        private async Task<string> PostRequestAsync(string url, Dictionary<string, object> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
            _httpClient.DefaultRequestHeaders.Add("apikey", _apikey);
            var resp = new HttpResponseMessage();

            if (url.Contains("dep"))
                url += "&agent=core";
            else
                url += "?agent=core";

            if (method == "POST")
                resp = await _httpClient.PostAsync(string.Format("{0}{1}", _BaseUrl, url), GetBodyData(parameters));
            else if (method == "GET")
            {
                resp = await _httpClient.GetAsync(string.Format("{0}{1}{2}", _BaseUrl, url, GetQueryStringData(parameters)));
            }


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




        private String GetQueryStringData(Dictionary<string, object> parameters)
        {
            String queryString = String.Empty;
            if (parameters != null && parameters.Count > 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if (i == 0)
                        queryString += ("?" + parameters.Keys.ElementAt(i) + "=" + parameters.Values.ElementAt(i));
                    else
                        queryString += ("&" + parameters.Keys.ElementAt(i) + "=" + parameters.Values.ElementAt(i));
                }
            }
            return queryString;

        }

        #endregion
    }
}
