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

namespace GhasedakApi
{
    public class Api : IAccountService
    {
        /// <summary>
        /// The root URI for the service endpoint.
        /// </summary>
        private const string _BaseUrl = "http://ghasedakapi.com";

        private readonly string _apikey;

        /// <summary>
        /// The HTTP client
        /// </summary>
        private HttpClient _httpClient;

        public Api(string apikey)
        {
            _apikey = apikey;
        }

        public async Task<AccountResult> AccountInfo()
        {
            var url = "api/v1/account/info";
            var param = new Dictionary<string, object>
              {
                  {"apikey", _apikey}
               };
            var res = await PostRequest(url, param);
            return  JsonConvert.DeserializeObject<AccountResult>(res);
        }
        private async Task<string> PostRequest(string url, Dictionary<string, object> parameters, string method = "POST", string contentType = "application/x-www-form-urlencoded")
        {
            var resp = await _httpClient.PostAsync(string.Format("{0}{1}", _BaseUrl, url), GetBodyData(parameters));
            var content = await resp.Content.ReadAsStringAsync();
            try
            {
                var result = JsonConvert.DeserializeObject<ApiResult>(content);
                if(result.Result.Code!=200)
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

}
}
