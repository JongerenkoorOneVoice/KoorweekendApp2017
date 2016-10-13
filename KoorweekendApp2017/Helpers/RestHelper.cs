using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using KoorweekendApp2017.Extensions;

namespace KoorweekendApp2017.Helpers
{
    public static class RestHelper
    {

        public async static Task<List<T>> GetRestDataFromUrl<T>(string requestUrl)
        {

            string rawData = String.Empty;
            List<T> returnValue = new List<T>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "GET";

            // Not yet implemented in the webservice
            // Authorizes the request using Basic authentication
            //string username = "";
            //string password = "";
            // request.Headers.Add("Authorization", "Basic " + String.Format("{0}:{1}", username, password));

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                rawData = reader.ReadToEnd();
                            }
                        }

                    }
                }

                returnValue = GetModelFromJson<List<T>>(rawData);
            }
            catch(Exception ex)
            {
                // do something;
            }

            return returnValue;

        }

        public static T GetModelFromJson<T>(String RawData)
        {

            JToken rawDataDynamic = JToken.Parse(RawData);
            JToken data = getJTokenData(rawDataDynamic);


            Boolean dataIsArray = data.Type == JTokenType.Array;
            String temp = data.ToJSON();
            //if (!dataIsArray) temp = String.Format("[{0}]", temp);

            try
            {
                T result = JsonConvert.DeserializeObject<T>(temp);

                return result;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        private static JToken getJTokenData(JToken token)
        {
            if (token != null && token.First != null)
            {
                if (token.First.Type == JTokenType.Array || token.First.Type == JTokenType.Object || token.First.Type == JTokenType.Property)
                {
                    JToken test = getJTokenData(token.First);
                    return test;
                }
                else
                {
                    if (token.Parent.Parent != null && token.Parent.Parent.Type == JTokenType.Array)
                        return token.Parent.Parent;
                    else
                        return token.Parent;
                }
            }
            return JToken.Parse("null");

        }
    }
}
