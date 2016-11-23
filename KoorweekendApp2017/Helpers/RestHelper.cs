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

namespace KoorweekendApp2017.Helpers
{
	public static class RestHelper
	{

		public async static Task<T> GetRestDataFromUrl<T>(string requestUrl)
		{

			string rawData = String.Empty;
			T returnValue = default(T);

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
			request.Method = "GET";

			// Not yet implemented in the webservice
			// Authorizes the request using Basic authentication
			//string username = "";
			//string password = "";
			// request.Headers.Add("Authorization", "Basic " + String.Format("{0}:{1}", username, password));

			try
			{
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
			
			}
		catch(Exception ex){
			var x = ex;
		}
			if (!String.IsNullOrEmpty(rawData) && rawData[0] == '<' && typeof(T) == typeof(String))
			{
				// resultJson is not JSON
				returnValue = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(rawData));
			}
			else {
				returnValue = GetModelFromJson<T>(rawData);
			}

			return returnValue;

		}


		public async static Task<T> PostDataToUrl<T>(string requestUrl, object  data)
		{
			
			T returnValue = default(T);

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
