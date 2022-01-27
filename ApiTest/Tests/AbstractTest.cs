/// < summary >
/// Abstract test, Parent class for all test cases
// </summary> 
using AppGate.APi.Test.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppGate.APi.Test
{
    public abstract class AbstracTest
    {
        #region properties
        /// <summary>
        /// HttpClient to   use on the API calls
        /// </summary>
        protected HttpClient HttpClient { get; set; } = new HttpClient();

        /// <summary>
        /// Test Context Used to store information that is provided to unit tests.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Execute Request based on testData
        /// </summary>
        /// <param name="testData">Test Data with Information to Excute the request in the Endpoint</param>
        /// <returns>HttpResponseMessage  with the result of execute the request</returns>
        protected async Task<HttpResponseMessage> ExecuteRequest(string apiUrl, string methodType, string inputData = null, string inputParameters = null)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            String stringContent = !string.IsNullOrEmpty(inputData) ? inputData : string.Empty;

            switch ((RequestMethodTypes)Enum.Parse(typeof(RequestMethodTypes), methodType))
            {
                case RequestMethodTypes.POST:
                    HttpContent content = new StringContent(stringContent, Encoding.UTF8, "application/json");
                    response = await HttpClient.PostAsync($"{apiUrl}", content);
                    break;
                case RequestMethodTypes.DELETE:
                    response = await HttpClient.DeleteAsync($"{apiUrl}{inputParameters}");
                    break;
                default:
                    throw new NotImplementedException();
            }
            return response;
        }
        #endregion

    }
}
