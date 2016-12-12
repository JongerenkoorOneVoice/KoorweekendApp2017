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
using System.Text;
using XLabs.Platform.Services;
using XLabs.Ioc;

namespace KoorweekendApp2017.Helpers
{
	public static class RestHelper
	{

		public async static Task<T> GetRestDataFromUrl<T>(string requestUrl, BasicAuthentication authObj = null)
		{
            T returnValue = Default<T>.Value;
            var network = Resolver.Resolve<INetwork>();
            if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
            {


                requestUrl = AuthenticationHelper.CreateSecureAuthenticatedUrl(requestUrl);
                string rawData = String.Empty;



                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "GET";

                // Not yet implemented in the webservice
                // Authorizes the request using Basic authentication
                //string username = "";
                //string password = "";
                // request.Headers.Add("Authorization", "Basic " + String.Format("{0}:{1}", username, password));

                try
                {
                    var response = (HttpWebResponse)Task.Run(request.GetResponseAsync).Result;


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
                    response.Dispose();


                }
                catch (Exception ex)
                {
                    if (ex.Message == "The remote server returned an error: (403) Forbidden.")
                    {
                        // Should probably log somthing here
                    }
                    else
                    {
                        // And here
                    }
                }

                if (!String.IsNullOrEmpty(rawData) && rawData[0] == '<' && typeof(T) == typeof(String))
                {
                    // resultJson is not JSON
                    returnValue = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(rawData));
                }
                else if (String.IsNullOrEmpty(rawData))
                {
                    return returnValue;
                }
                else
                {
                    returnValue = GetModelFromJson<T>(rawData);
                }
            }
			return returnValue;

		}


		public async static Task<T> PostDataToUrl<T>(string requestUrl, object  data)
		{
            T returnValue = default(T);
            var network = Resolver.Resolve<INetwork>();
            if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
            {
                requestUrl = AuthenticationHelper.CreateSecureAuthenticatedUrl(requestUrl);


                String json = JsonConvert.SerializeObject(data);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json.ToCharArray());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.ContentType = "application/json; charset=utf-8"; //set the content type to JSON
                request.Method = "POST"; //make an HTTP POST


                var client = new HttpClient();
                var postData = new ByteArrayContent(jsonBytes);
                var resultStream = await client.PostAsync(requestUrl, postData);
                var resultBytes = await resultStream.Content.ReadAsByteArrayAsync();
                var resultJson = Encoding.UTF8.GetString(resultBytes, 0, resultBytes.Length);


                returnValue = GetModelFromJson<T>(resultJson);
            }
			return returnValue;



			// Not yet implemented in the webservice
			// Authorizes the request using Basic authentication
			//string username = "";
			//string password = "";
			// request.Headers.Add("Authorization", "Basic " + String.Format("{0}:{1}", username, password));

		

			/*
			using (HttpWebResponse response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null).ConfigureAwait(false)))
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
*/


		}

		public async static Task<T> PutDataToUrl<T>(string requestUrl, object data)
		{
            T returnValue = default(T);
            var network = Resolver.Resolve<INetwork>();
            if (network.InternetConnectionStatus() != NetworkStatus.NotReachable)
            {
                requestUrl = AuthenticationHelper.CreateSecureAuthenticatedUrl(requestUrl);


                String json = JsonConvert.SerializeObject(data);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(json.ToCharArray());

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.ContentType = "application/json; charset=utf-8"; //set the content type to JSON
                request.Method = "PUT"; //make an HTTP PUT


                var client = new HttpClient();
                var postData = new ByteArrayContent(jsonBytes);
                var resultStream = await client.PutAsync(requestUrl, postData);
                var resultBytes = await resultStream.Content.ReadAsByteArrayAsync();
                var resultJson = Encoding.UTF8.GetString(resultBytes, 0, resultBytes.Length);


                returnValue = GetModelFromJson<T>(resultJson);
            }
			return returnValue;
		}

        public static T GetModelFromJson<T>(String RawData)
        {
			if (RawData == "null") return default(T);

            JToken rawDataDynamic = JToken.Parse(RawData);
            JToken data = getJTokenData(rawDataDynamic);


            //Boolean dataIsArray = data.Type == JTokenType.Array;
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

			if (token == null)
			{
				return JToken.Parse("null");
			}

			if (token.Type == JTokenType.String
			|| token.Type == JTokenType.Integer
			|| token.Type == JTokenType.Boolean
			|| token.Type == JTokenType.Date
			|| token.Type == JTokenType.Float
			|| token.Type == JTokenType.Guid)
			{
				return token;
			}


            if (token.First != null)
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

			// If somehow token is something different return null;
			return JToken.Parse("null");

        }
    }
}
